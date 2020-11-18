using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxhealth = 100;
    public int currenthealth;

    public EnemyHealthBar healthBarEnemy;

    private Rigidbody2D rb;
    public float attackrate;
    public float nexttimetoattack;
    private BoxCollider2D boxcollider2d;
    public float speed;
    public float LineOfSite;
    private Transform player;
    public float AttackRange;
    public Animator anime;
    public bool facingRight = true;
    public PlayerMovement plm;

    public float AttackRnge = 0.5f;
    public LayerMask enemyLayers;
    public Transform AttackPoint;
    public int attackdamage = 20;






    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        healthBarEnemy.SetMaxHealth(maxhealth);
        boxcollider2d = GetComponent<BoxCollider2D>();
        rb= GetComponent<Rigidbody2D>();
        anime.SetBool("isDead", false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public void TakeDamage(int damage)
    {
        
        currenthealth -= damage;
        healthBarEnemy.SetHealth(currenthealth);
        anime.SetTrigger("hurt");
        if (currenthealth <= 0)
        {
            Die();
            this.enabled = false;      
        } 
    }



    void Die()
    {
        rb.constraints=RigidbodyConstraints2D.FreezeAll;
        boxcollider2d.enabled = false;
        anime.SetBool("isDead", true);
    }
    public void Update()
    {
        float DistanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (DistanceFromPlayer < LineOfSite && DistanceFromPlayer >= AttackRange )
        {

            anime.SetBool("speed", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            FlipEnemy();
        }
        else if(DistanceFromPlayer > LineOfSite )
        {
            anime.SetBool("speed", false);
        }

        else if (DistanceFromPlayer <= AttackRange&&plm.animator.GetBool("IsBlocking")==false )
        {
            if (Time.time >= nexttimetoattack )
            {
                anime.SetBool("speed", false);
                anime.SetTrigger("EnemyAttack");
            nexttimetoattack = Time.time +1/ attackrate;
                Attck();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        Enemy enemy = hitinfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(enemy.attackdamage);
        }
        
    }

    void Attck()
    {
        

        anime.SetTrigger("Attacking");
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRnge, enemyLayers);
        foreach (Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<PlayerMovement>().TakeDamage1(attackdamage);

        }
    }

    public void FlipEnemy()
    {
        float playerdirection = player.position.x - transform.position.x;
        if (playerdirection > 0 && facingRight)
        {
            Flip();
        }
        else if (playerdirection < 0 && !facingRight)
        {
            Flip();
        }
    }
    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        transform.Rotate(0f, -180, 0f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        if (AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position,AttackRnge);
    }



























}
