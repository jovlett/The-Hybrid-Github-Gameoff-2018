using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpikes : MonoBehaviour {
    public bool dashing = false;   //If the player is dashing or not

    private Rigidbody2D rb2D;   //The player's rigidbody 2D

    public playerStats plStat;   //The player's stats like speed, agility, etc.

    public GameObject target;   //The target the player will go to when dashing

    public bool cooldown;   //Controls whether player still has to wait for a cooldown or not

    public GameObject dashParticle;

    public GameObject[] spikes;

    public float dmg;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (!dashing)
        {
            target.transform.localPosition = new Vector3(0, plStat.SpikeTargetDistance, 0);
        }

        if (Input.GetKeyDown("space") && !cooldown && !dashing)
        {
            dashing = true;
            target.transform.parent = null;

            StartCoroutine(doCooldown());

            GetComponent<CharacterMovement>().canMove = false;

            var newDashParticle = Instantiate(dashParticle, transform.Find("Spike Config"));

            newDashParticle.transform.localPosition = new Vector3(0, -0.5f, 0);

            StartCoroutine(spikeKillSwitch());
        }

        if (dashing)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * plStat.Speed);
            dmg = Vector3.Distance(transform.position, target.transform.position);
        }

        if (Vector3.Distance(transform.position, target.transform.position) < 1)
        {
            dashing = false;
            target.transform.parent = transform;
            dmg = 0;
            GetComponent<CharacterMovement>().canMove = true;
        }
    }

    public void StopSpikes()
    {
        dashing = false;
        target.transform.parent = transform;
        target.transform.localPosition = new Vector3(0, plStat.SpikeTargetDistance, 0);

        GetComponent<CharacterSpikes>().enabled = false;

        GetComponent<CharacterGun>().enabled = true;

        GetComponent<CharacterMovement>().canMove = true;

        GameObject.Find("Blue Trail").GetComponent<TrailRenderer>().emitting = true;
        GameObject.Find("Orange Trail").GetComponent<TrailRenderer>().emitting = false;
    }

    IEnumerator doCooldown()
    {
        cooldown = true;

        yield return new WaitForSeconds(plStat.SpikeCooldown);

        cooldown = false;
    }

    IEnumerator spikeKillSwitch ()
    {
        yield return new WaitForSeconds(0.3f);

        dashing = false;
        target.transform.parent = transform;
        dmg = 0;
        GetComponent<CharacterMovement>().canMove = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyHealth>() && dashing)
        {
            collision.gameObject.GetComponent<EnemyHealth>().Hurt(dmg);
        }
    }
}

