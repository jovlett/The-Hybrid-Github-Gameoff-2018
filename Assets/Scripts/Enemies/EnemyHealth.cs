using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public bool dead;   //Whether the player has died or not

    public float maxHealth;   //The maximum amount of health the player has
    public float curHealth;   //The current amount of health the player has

    public float healthTarget = 5;

    public bool hurt;

    public Image healthDisplay;

    public GameObject TokenFX;

    public Collider2D[] colliders;

    public AudioClip hurtSFX;
    public AudioClip deadSFX;

    public GameObject healthPickup;

    private GameManager gm;

    private Animator anim;

    private AudioSource aud;

    private bool once = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        curHealth = maxHealth;

        anim = GetComponent<Animator>();

        aud = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        healthDisplay.fillAmount = curHealth / maxHealth;

        if (hurt && healthTarget != 0)
            curHealth = Mathf.Lerp(curHealth, healthTarget, Time.deltaTime * 5);

        if (Mathf.Abs(curHealth - healthTarget) <= 0.05f)
            hurt = false;

        if (curHealth <= 0 || healthTarget <= 0)
        {
            dead = true;
            curHealth = 0;
            GetComponent<CircleCollider2D>().enabled = false;
        }

        healthDisplay.rectTransform.rotation = Quaternion.Euler(0, 0, -45);

        if (dead)
        {
            anim.SetBool("Dead", true);

            if(!once)
            {
                aud.PlayOneShot(deadSFX);
                once = true;
            }
            foreach(Collider2D c in colliders)
            {
                c.enabled = false;
            }
        }

        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
    }

    public void Hurt (float dmg)
    {
        aud.PlayOneShot(hurtSFX);
        aud.pitch = Random.Range(0.75f, 1.5f);

        hurt = true;

        if (GetComponent<ChaserMovement>())
            anim.Play("Chaser_Hurt");
        else if (GetComponent<SpikeballDash>())
            anim.Play("SpikeballEnemy_Hurt");
        else if (GetComponent<HealerEnemy>())
            anim.Play("Healer_Hurt");

        if(curHealth > 0  && !dead)
            healthTarget = curHealth - dmg;
    }

    public void Heal(float amnt)
    {
        aud.PlayOneShot(hurtSFX);

        if (amnt != 0)
        {
            hurt = true;

            if (GetComponent<ChaserMovement>())
                anim.Play("Chaser_Hurt");
            else if (GetComponent<SpikeballDash>())
                anim.Play("SpikeballEnemy_Hurt");
            else if (GetComponent<HealerEnemy>())
                anim.Play("Healer_Hurt");

            if (curHealth > 0 && !dead)
                healthTarget = curHealth + amnt;
        }
    }

    private void destroy()
    {
        Destroy(gameObject);
        Destroy(GetComponent<TargetIndicator>().m_icon.gameObject);

        Instantiate(TokenFX, transform.position, Quaternion.identity);

        if (Random.Range(0, gm.difficulty/2) == 1)
            Instantiate(healthPickup, transform.position, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Color color;
        color = Color.red;
        // local up
        DrawHelperAtCenter(transform.up, color, 3f);
    }

    private void DrawHelperAtCenter(
                        Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }
}
