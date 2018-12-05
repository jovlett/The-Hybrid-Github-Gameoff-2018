using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchModes : MonoBehaviour {

    public playerStats plStat;   //The player's stats like speed, agility, etc.

    private Animator anim;   //The player's animator

    public string curConfig;

    private AudioSource aud;

	void Start () {
        anim = GetComponent<Animator>();

        aud = GetComponentInChildren<AudioSource>();

        InvokeRepeating("Switch", plStat.SwitchDuration, plStat.SwitchDuration);
	}
	
	void Update () {
		
	}

    void Switch ()
    {
        if(curConfig == "Gun")
        {
            aud.volume = 0.2f;

            GetComponent<CharacterGun>().StopGun();

            curConfig = "Spikes";

            anim.SetBool("Switch", true);
        }
        else if (curConfig == "Spikes")
        {
            aud.volume = 0.732f;

            GetComponent<CharacterSpikes>().StopSpikes();

            curConfig = "Gun";

            anim.SetBool("Switch", false);
        }
    }
}
