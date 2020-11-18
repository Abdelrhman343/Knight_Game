using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollison3 : MonoBehaviour
{

    public GameObject PickupEffect;
    public float duration;
    public int HealthAdd;
    public Enemy enemy;


    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           StartCoroutine( PickUp());
        }

    }


    IEnumerator PickUp()
    {

        Instantiate(PickupEffect, transform.position, transform.rotation);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        enemy.transform.localScale = new Vector3 (8,8,1);
        yield return new WaitForSeconds(duration);
        enemy.transform.localScale = new Vector3(2.4f, 2.4f, 1);



    }



}