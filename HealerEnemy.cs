using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealerEnemy : MonoBehaviour {
    public float walkSpeed;   //Speed of the enemy
    public float curSpeed;   //Control variable
    public float maxSpeed;   //The maximum speed the enemy can go at normal speed

    public List<EnemyHealth> enemiesInRadius =  new List<EnemyHealth>();
    public LayerMask enemyLayer;
    public Collider2D[] hitColliders;

    public enemyStats enStat;

    private GameObject player;

    void Start () {
        InvokeRepeating("Heal", 1, 1);

        LoadStats();

        player = GameObject.Find("Player");
    }

    void LoadStats()
    {
        walkSpeed = (enStat.Speed + (enStat.Agility / 3));
    }

    void Update () {
        GameObject[] gos = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var b = new Bounds(transform.position, Vector3.zero);
        var layeredObjs = gos.Where(x => x.layer == 10);

        foreach(var lo in layeredObjs)
        {
            b.Encapsulate(lo.transform.position);
        }

        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        if (GetComponent<EnemyHealth>().dead != true || GetComponent<EnemyHealth>().healthTarget > 0 && layeredObjs.Count() - 1 <= 1)
            transform.position = Vector2.MoveTowards(transform.position, b.center, Time.deltaTime * curSpeed);
        else if(Vector3.Distance(transform.position, player.transform.position) >= 2)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * curSpeed);
    }

    void Heal ()
    {
        if (GetComponent<EnemyHealth>().dead != true || GetComponent<EnemyHealth>().healthTarget > 0)
        {
            enemiesInRadius.Clear();

            hitColliders = Physics2D.OverlapCircleAll(transform.position, 3, enemyLayer);

            foreach (Collider2D e in hitColliders)
            {
                if (!e.GetComponent<HealerEnemy>())
                    enemiesInRadius.Add(e.GetComponent<EnemyHealth>());
            }

            enemiesInRadius.RemoveAll(item => item == null);

            enemiesInRadius = enemiesInRadius.Distinct().ToList();

            foreach (EnemyHealth e in enemiesInRadius)
            {
                
                if (!e.dead && e.maxHealth - e.curHealth > 0.05f)
                {
                    e.Heal(Random.Range(0.5f, 2.5f));
                }
            }
        }
    }
}
