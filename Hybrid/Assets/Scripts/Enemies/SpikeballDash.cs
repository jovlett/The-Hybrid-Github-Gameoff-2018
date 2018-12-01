using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeballDash : MonoBehaviour {

    public bool isRotating;

    public GameObject dashTarget;

    public float cooldown;

    public enemyStats enStat;

    public bool dashing;

    public float dashTargetDistance;

    private Transform player;

    private Animator anim;

    public float i;

    void Start () {
        player = GameObject.Find("Player").transform;

        //InvokeRepeating("Dash", i + Random.Range(0, 1.2f), Mathf.Abs(i) + Random.Range(cooldown - 1.2f, cooldown + 1.2f));
        InvokeRepeating("Dash", i + Random.Range(0.1f, 1.2f), cooldown + i + Random.Range(0.1f, 1.2f));

        anim = GetComponent<Animator>();
	}
	
	void Update () {
            i = Random.Range(-2.2f, 2.2f);

            Vector3 diff = player.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            if (isRotating)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z), Time.deltaTime * 3 * enStat.Agility);
            }

            if (dashing)
            {
                transform.position = Vector3.Lerp(transform.position, dashTarget.transform.position, Time.deltaTime * enStat.Speed);
            }

            if (Vector3.Distance(transform.position, dashTarget.transform.position) < 0.1f)
            {
                dashing = false;

                dashTarget.transform.parent = transform;
                dashTarget.transform.localPosition = new Vector3(4.387f, 0);
                isRotating = true;
            }

    }

    void Dash ()
    {
        if (!GetComponent<EnemyHealth>().dead)
        {
            isRotating = false;

            anim.Play("SpikeballEnemy_Target");
        }
    }

    void dash ()
    {
        dashTarget.transform.parent = null;

        dashing = true;
    }
}
