using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGun : MonoBehaviour {

    public GameObject bullet;
    public Transform gunEnd;
    private Transform gunEnd_Axis;

    private bool cooldown;

    public playerStats plStat;

    public GameObject shootParticle;

    public AudioClip shootSFX;

    private Rigidbody2D rb2D;

    private Animator anim;

    private AudioSource aud;

    void Start()
    {
        aud = GetComponentInChildren<AudioSource>();

        anim = GetComponent<Animator>();

        rb2D = GetComponent<Rigidbody2D>();

        gunEnd_Axis = gunEnd.parent;
    }

    void Update()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        gunEnd_Axis.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), Time.deltaTime * 10);

        if (Input.GetKeyDown("space") && !cooldown)
        {
            anim.SetTrigger("Shoot");

            StartCoroutine(Shoot());
        }
    }

    public void StopGun()
    {
        GetComponent<CharacterGun>().enabled = false;


        GetComponent<CharacterSpikes>().enabled = true;

        GetComponent<CharacterMovement>().canMove = true;

        GameObject.Find("Blue Trail").GetComponent<TrailRenderer>().emitting = false;
        GameObject.Find("Orange Trail").GetComponent<TrailRenderer>().emitting = true;
    }

    IEnumerator Shoot ()
    {
        aud.PlayOneShot(shootSFX);
        aud.pitch = Random.Range(0.75f, 1.5f);

        var newBullet = Instantiate(bullet, gunEnd.position, transform.rotation);

        newBullet.GetComponent<Bullet>().speed = plStat.GunBulletSpeed;

        newBullet.GetComponent<Bullet>().damage = plStat.GunBulletDamage + Random.Range(-1, 1);

        Instantiate(shootParticle, gunEnd.position, transform.rotation);

        cooldown = true;

        yield return new WaitForSeconds(plStat.GunCooldown);

        cooldown = false;
    }
}
