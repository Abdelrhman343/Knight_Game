using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhit : MonoBehaviour
{
    public int maxhealth = 100;
    int currenthealth;
    public Animator animator;





    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        animator.SetTrigger("hurt");
        if (currenthealth <= 0)
        {
            Die();
            //GetComponent<Collider2D>().enabled = false;
            this.enabled = false;

        }

    }
    void Die()
    {
        animator.SetBool("isDead", true);
    }

























}
