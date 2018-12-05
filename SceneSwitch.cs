using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {
	IEnumerator Switch (string name) {
        if (name == "Game")
            yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(name);
	}
}
