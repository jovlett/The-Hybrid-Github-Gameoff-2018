using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMovement : MonoBehaviour {

    public float walkSpeed;   //Speed of the enemy
    public float curSpeed;   //Control variable
    public float maxSpeed;   //The maximum speed the enemy can go at normal speed

    public enemyStats enStat;   //The enemy's stats like speed, agility, etc.

    private Transform player;

    void LoadStats()
    {
        walkSpeed = (enStat.Speed + (enStat.Agility / 3));
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;

        LoadStats();
    }

    void Update()
    {
        Vector3 diff = player.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), Time.deltaTime * 3 * enStat.Agility);

        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        if(GetComponent<EnemyHealth>().dead != true || GetComponent<EnemyHealth>().healthTarget > 0)
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * curSpeed / 1.5f);
    }

    void OnDrawGizmos()
    {
        Color color;
        color = Color.green;
        // local up
        DrawHelperAtCenter(transform.up, color, 2f);
    }

    private void DrawHelperAtCenter(
                        Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }
}
