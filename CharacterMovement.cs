using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    
    public float walkSpeed;   //Speed of the player
    public float curSpeed;   //Control variable
    public float maxSpeed;   //The maximum speed the player can go at normal speed

    private Rigidbody2D rb2D;   //The player's rigidbody 2D

    public playerStats plStat;   //The player's stats like speed, agility, etc.

    public bool canMove = true;

    void LoadStats()
    {
        walkSpeed = (plStat.Speed + (plStat.Agility / 3));
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        LoadStats();
    }

    void FixedUpdate()
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        if(canMove && !Input.GetKeyDown("space") && !GetComponent<CharacterHealth>().dead)
            rb2D.velocity = new Vector2
            (Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f), Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
    }

    void Update()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), Time.deltaTime * 3 * plStat.Agility);
    }

}

