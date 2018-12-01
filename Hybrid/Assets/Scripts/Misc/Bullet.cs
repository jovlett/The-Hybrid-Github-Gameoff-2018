using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public GameObject hitParticle;
    public ParticleSystem trailParticle;
    public GameObject trail;
    public float damage;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update () {
        move(speed);
	}

    public void move(float speed)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.up, Time.deltaTime * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            collision.gameObject.GetComponent<EnemyHealth>().Hurt(damage);
        }

        hitParticle.transform.parent = null;
        hitParticle.SetActive(true);

        trailParticle.Stop();
        trailParticle.transform.parent = null;

        trail.transform.parent = null;

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Color color;
        color = Color.green;
        // local up
        DrawHelperAtCenter(transform.up, color, 2f);
    }

    private void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }

}
