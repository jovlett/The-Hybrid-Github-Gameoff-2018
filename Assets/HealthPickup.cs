using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(DelayedDestroy());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            anim.SetTrigger("Die");
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(Random.Range(25, 35));

        anim.SetTrigger("Die");
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
