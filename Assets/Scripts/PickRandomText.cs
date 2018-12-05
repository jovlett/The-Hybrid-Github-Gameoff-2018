using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickRandomText : MonoBehaviour {
    public string[] quotes;
    public Text text;

	void Start () {
        Randomize();
    }

    public void Randomize ()
    {
        text.text = quotes[Random.Range(0, quotes.Length)];
    }
}
