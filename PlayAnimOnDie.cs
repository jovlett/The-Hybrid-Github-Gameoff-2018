using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimOnDie : MonoBehaviour {
    public Animator anim;
    public string animationName;

    public EnemyHealth entity;
	
	void Update () {
        if (entity.dead)
            anim.Play(animationName);
	}
}
