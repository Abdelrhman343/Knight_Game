using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anime;
    private Transform player;
    public float LineOfSite;
    public bool facingRight = true;
    public float Range;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float DistanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (DistanceFromPlayer <= LineOfSite )
        {
            FlipEnemy();
            Debug.Log("yes");
            anime.SetBool("Flip1",true);
        }

        if(DistanceFromPlayer <= Range  )
        {
            
            anime.SetBool("IsBanha", true);
        }
    }

    // Update is called once per frame
    void FlipEnemy()
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
    facingRight =! facingRight;

    transform.Rotate(0f, -180, 0f);
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
