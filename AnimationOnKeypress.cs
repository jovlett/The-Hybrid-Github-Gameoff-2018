using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnKeypress : MonoBehaviour {
    public string triggerName;
    public KeyCode key;
    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(key))
            anim.SetTrigger(triggerName);

    }
}
