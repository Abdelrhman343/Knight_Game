using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public player PLAYER;
    
    public Transform FirePoint;
    public GameObject FireBallPrefab;
    public float shootrate = 2;
    float nexttimetoshoot;
    public float ShootForce;
    public float WaitForRunReset;
    public GameObject ImpactEffect;
    public float backShootForce;
    public PlayerMovement plm;
    public Bullet bullet;


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nexttimetoshoot)
        {
            if (Input.GetButtonDown("Shoot"))
            {
                Instantiate(FireBallPrefab, FirePoint.position, FirePoint.rotation);
                var Instance = Instantiate(ImpactEffect, FirePoint.position, FirePoint.rotation);
                playerMovement.runSpeed = 0;
                playerMovement.animator.SetTrigger("Shoot");
                playerMovement.rb.AddForce(new Vector3 (Time.time* ShootForce,0,0));
                if (PLAYER.m_FacingRight == false)
                {
                    playerMovement.rb.AddForce(new Vector3(Time.time * backShootForce, 0, 0));
                }
                nexttimetoshoot = Time.time + 1f / shootrate;
                StartCoroutine(WaitForRun());
                Destroy(Instance, 3);

            if (Instance.gameObject.CompareTag("potion"))
            {
                plm.TakeDamage1(20);
                Destroy(gameObject);
                var Ins = Instantiate(bullet.Impact, transform.position, transform.rotation);
                Destroy(Ins, 3);


            }
            }



        }

        IEnumerator WaitForRun()
        {
            yield return new WaitForSeconds(WaitForRunReset);
            playerMovement.runSpeed = playerMovement.AfterRunSpeed;
            

           
        }





    }





}
