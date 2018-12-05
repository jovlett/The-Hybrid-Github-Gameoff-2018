using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CharacterHealth : MonoBehaviour {

    public bool dead;

    public float maxHealth;   
    public float curHealth;

    float healthTarget;

    public bool hurt;

    public Image healthDisplay;

    public Animator deadMenu;

    public AudioClip dieSFX;

    public string lowpassParam;
    public string volParam;
    public AudioMixer mixer;

    private AudioSource aud;

    private Animator anim;

    private bool once = false;

	void Start () {
        curHealth = maxHealth;

        anim = GetComponent<Animator>();

        aud = GetComponentInChildren<AudioSource>();

        mixer.SetFloat(lowpassParam, 5000);
        mixer.SetFloat(volParam, 0);
    }
	
	void Update () {

        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);

        if(hurt)
            curHealth = Mathf.Lerp(curHealth, healthTarget, Time.deltaTime * 5);

        if (curHealth - healthTarget <= 0.05f)
            hurt = false;

        if (curHealth <= 0)
        {
            dead = true;
            curHealth = 0;
        }

        healthDisplay.rectTransform.rotation = Quaternion.Euler(0, 0, -45);
        healthDisplay.fillAmount = curHealth / maxHealth;

        if (dead && !once)
        {
            Die();
            once = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LowerHealth" && !dead && !GetComponent<CharacterSpikes>().dashing)
        {
            hurt = true;
            healthTarget = curHealth - 1;
        }

        if (collision.gameObject.tag == "HealthPickup" && !dead)
        {
            hurt = true;
            healthTarget = curHealth + 30;
        }
    }

    void Die ()
    {
        aud.PlayOneShot(dieSFX);
        aud.transform.parent = null;

        if(GetComponent<CharacterGun>().enabled)
        {
            anim.Play("Player_GunDie");

            var trail = GameObject.Find("Blue Trail").GetComponent<TrailRenderer>();
            trail.autodestruct = true;
            trail.transform.parent = null;
        }
        else if (GetComponent<CharacterSpikes>().enabled)
        {
            anim.Play("Player_SpikeDie");

            var trail = GameObject.Find("Orange Trail").GetComponent<TrailRenderer>();
            trail.autodestruct = true;
            trail.transform.parent = null;
        }
    }

    private void destroy()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        deadMenu.Play("YouDied_Flyin");

        mixer.SetFloat(lowpassParam, 1000);
        mixer.SetFloat(volParam, -10);
    }

}
