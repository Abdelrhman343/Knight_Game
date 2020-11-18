
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


	public player controller;
	public Animator animator;
	public Rigidbody2D rb;
	public int PotionHealth;
	 Vector2 move;
	public GameObject DashEffect;
	public Transform dashpoint;
	public CameraController cm;
	public float runSpeed = 40f;
	public float AfterRunSpeed;
	float horizontalMove = 0f;
	bool crouch = false;
	public float JumpForce;
	public bool isJumping;
	public float WaitForRunReset;





	public HealthBar healthBar;

	public CamerZoom camerazoom;

	public float AttackRnge = 0.5f;
	public LayerMask enemyLayers;
	public Transform AttackPoint;
	public int attackdamage = 20;
	public float attackrate = 2f;
	float nexttimetoattack = 0f;
	public int SlideSpeed;

	public int maxhealth1= 100;
	public int currenthealth1;
	public BoxCollider2D boxcollider2d;
	Vector2 offset;
	Vector2 size;
	Vector2 Orginaloffset;
	Vector2 Orginalsize;
	public LayerMask WhatIsGround;
	public float CheckRadius;
	public bool isgrounded=true;
	public Transform FeetRadius;
	public int Random;








	//offset -0.6692261 size 1.08819
	// Update is called once per frame
	//transform.eulerAngles = new Vector3(0, 0, 0);


	private void Start()
	{


		Orginaloffset= new Vector2(boxcollider2d.offset.x, boxcollider2d.offset.y );
		Orginalsize= new Vector2(boxcollider2d.size.x, boxcollider2d.size.y );
		offset = new Vector2(boxcollider2d.offset.x, boxcollider2d.offset.y - 0.0892525f);
		size = new Vector2(boxcollider2d.size.x, boxcollider2d.size.y- 0.178505f);
		isgrounded = true;
		healthBar.SetMaxHealth(maxhealth1);
		currenthealth1 = maxhealth1;
		AfterRunSpeed = runSpeed;
		rb = GetComponent<Rigidbody2D>();
	}
	void Update()
	{
		isgrounded = Physics2D.OverlapCircle(FeetRadius.position, CheckRadius, WhatIsGround);

		Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
		transform.Translate(m, Space.World);

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		if (horizontalMove > 0 || horizontalMove < 0)
		{
			camerazoom.ZoomActive = true;
		}
		else
		{
			camerazoom.ZoomActive = false;
		}


        if (isgrounded == true)
        {
			rb.gravityScale = 4;
		}
        
		if ( isgrounded==true&&Input.GetButtonDown("Jump"))
		{
			isJumping = false;
			rb.velocity = Vector2.up * JumpForce;
		}
        else if(isgrounded==false)
        {
			animator.SetBool("IsJumping", true);
			rb.gravityScale = 12;
			
			
        }

		


		if (Input.GetButtonDown("Crouch"))
		{
			boxcollider2d.offset = offset;
			boxcollider2d.size = size;
			crouch = true;
			runSpeed = 0;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			boxcollider2d.offset = Orginaloffset;
			boxcollider2d.size = Orginalsize;
			crouch = false;
			runSpeed = AfterRunSpeed;
		}

		if (Time.time >= nexttimetoattack)
		{
			if (Input.GetButtonDown("Slide"))
			{

            if (horizontalMove != 0)
            {
				nexttimetoattack = Time.time + 1 / attackrate;
				runSpeed = SlideSpeed;
				var ins=Instantiate(DashEffect, dashpoint.position,transform.rotation);
				animator.SetTrigger("Slide");
				Destroy(ins, 2);

            }
				StartCoroutine(WaitForRun());

			}
			
			if (Input.GetButtonDown("shieldattack"))
			{
				ShieldAttack();

			}
			if (Input.GetButtonUp("shieldattack"))
			{
				animator.SetBool("IsBlocking", false);
				runSpeed = AfterRunSpeed;

			}


			if (Input.GetButtonDown("Attack"))
			{
				nexttimetoattack = Time.time+1/ attackrate;
				Attck();
			}

		}

	}


	void ShieldAttack()
	{
		animator.SetBool("IsBlocking",true);

		runSpeed = 0;
		
	}
	void Attck()
	{
		healthBar.SetHealth(currenthealth1);
		animator.SetTrigger("Attacking");
		Collider2D[] hitenemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRnge, enemyLayers);
		foreach (Collider2D enemy in hitenemies)
		{
			enemy.GetComponent<Enemy>().TakeDamage(attackdamage);
			
		}
	}

    public void TakeDamage1(int damage )
	{

		currenthealth1 -= damage;
		healthBar.SetHealth(currenthealth1);
		healthBar.healthPercentage.text = healthBar.slider.value + "%";
		animator.SetTrigger("hurt");
		if (currenthealth1 <= 0)
		{
			Die();

		}

	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("potion"))
        {
			currenthealth1 += PotionHealth;
			Destroy(gameObject);
        }
    }

    public void Die()
	{
		animator.SetBool("isDead", true);
		StartCoroutine(waittodie());
		StartCoroutine(waittospawn());
		
	}
	IEnumerator waittodie()
    {
		yield return new WaitForSeconds(0.7f);
		this.enabled = false;
		cm.enabled =false;
		boxcollider2d.enabled = false;
		rb.gravityScale = 0;
		transform.position=new Vector3(841,514,65);
		animator.SetBool("Respawn", true);

    }
	IEnumerator waittospawn()
    {
		yield return new WaitForSeconds(2);
		animator.SetBool("isDead", false);
		transform.position = new Vector3(0, 7, 0);
		cm.enabled = true;
		boxcollider2d.enabled = true;
		currenthealth1 = maxhealth1;
		healthBar.SetHealth(currenthealth1);
		rb.gravityScale = 4;
		this.enabled = true;
	}
	

	IEnumerator WaitForRun()
	{
		yield return new WaitForSeconds(WaitForRunReset);
		runSpeed = AfterRunSpeed;


	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("ground"))
		{
			isJumping = false;
			animator.SetBool("IsJumping", false);

		}
	}


	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	private void OnDrawGizmosSelected()
	{
		if (AttackPoint == null)
		{
			return;
		}

		Gizmos.DrawWireSphere(AttackPoint.position, AttackRnge);
	}


	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch);

	}

	}