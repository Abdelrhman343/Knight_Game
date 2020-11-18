using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollison2 : MonoBehaviour
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
        yield return new WaitForSeconds(duration);



    }



}