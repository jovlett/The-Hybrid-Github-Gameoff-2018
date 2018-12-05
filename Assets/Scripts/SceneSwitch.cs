using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    void Switch (string name) {
        if (name == "Game")
            StartCoroutine(delay());

        SceneManager.LoadScene(name);
	}

    IEnumerator delay ()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
