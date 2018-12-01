using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchModes : MonoBehaviour {

    public playerStats plStat;   //The player's stats like speed, agility, etc.

    private Animator anim;   //The player's animator

    public string curConfig;

	void Start () {
        anim = GetComponent<Animator>();

        InvokeRepeating("Switch", plStat.SwitchDuration, plStat.SwitchDuration);
	}
	
	void Update () {
		
	}

    void Switch ()
    {
        if(curConfig == "Gun")
        {
            GetComponent<CharacterGun>().StopGun();

            curConfig = "Spikes";

            anim.SetBool("Switch", true);
        }
        else if (curConfig == "Spikes")
        {
            GetComponent<CharacterSpikes>().StopSpikes();

            curConfig = "Gun";

            anim.SetBool("Switch", false);
        }
    }
}
