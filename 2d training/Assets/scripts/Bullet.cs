using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float ShootSpeed;
    public Rigidbody2D rb;
    private Animator anim;
    public GameObject Impact;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = transform.right * ShootSpeed;
        StartCoroutine(DestroyFireBall());
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            Destroy(gameObject);
           var Ins= Instantiate(Impact, transform.position, transform.rotation);
            Destroy(Ins,3);
            enemy.anime.SetTrigger("hurt");
            enemy.TakeDamage(enemy.attackdamage);

        }



    }

    IEnumerator DestroyFireBall()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

}
