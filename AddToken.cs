using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddToken : MonoBehaviour {
    public playerStats plStat;
    private Text Counter;
    private Text highCounter;

    void Start()
    {
        Counter = GameObject.Find("Counter").GetComponent<Text>();
        highCounter = GameObject.Find("HighCounter").GetComponent<Text>();

        plStat.TokenAmount += 1;

        highCounter.text = "" + PlayerPrefs.GetInt("Highscore");

        GameObject.Find("Token Amount").GetComponent<Text>().text = ": " + plStat.TokenAmount;
        Counter.text = GameObject.Find("Token Amount").GetComponent<Text>().text;

        if(plStat.TokenAmount > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore"  , plStat.TokenAmount);
            highCounter.text = "" + GameObject.Find("Token Amount").GetComponent<Text>().text;
        }
    }
}
