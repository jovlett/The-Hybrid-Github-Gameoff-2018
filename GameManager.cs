using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public playerStats plStat;

    public enemyDB enDB;

    public int curWave = 0;
    public int enCount = 0;
    public int difficulty = 0;

    public GameObject waveGraphic;
    public PickRandomText waveTitle;
    public Text waveText;

    private GameObject player;

	void Start () {
        plStat.TokenAmount = 0;

        player = GameObject.Find("Player");
    }

	void Update () {
        enCount = FindObjectsOfType<EnemyHealth>().Length;

		if(enCount == 0)
        {
            curWave += 1;

            difficulty += Random.Range(1, 3);

            if (!player.GetComponent<CharacterHealth>().dead)
            {
                waveText.text = "Wave " + curWave;

                if (curWave != 1)
                {
                    waveTitle.Randomize();

                    waveGraphic.SetActive(false);
                    waveGraphic.SetActive(true);
                }
            }

            /*
            for (int i = 0; i < difficulty; i++)
            {
                Instantiate(enDB.enemies[Random.Range(0, enDB.enemies.Length)], player.transform.position + new Vector3(Random.Range(-difficulty, difficulty), Random.Range(-difficulty, difficulty)), Quaternion.identity);
            }*/

            Vector3 center = transform.position;
            for (int i = 0; i < difficulty; i++)
            {
                Vector3 pos = RandomCircle(center, difficulty);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                Instantiate(enDB.enemies[Random.Range(0, enDB.enemies.Length)], player.transform.position + new Vector3(Random.Range(-difficulty, difficulty), Random.Range(-difficulty, difficulty)), Quaternion.identity);
            }

            if (Random.Range(0, 5) == 1)
            {
                Instantiate(enDB.healer, player.transform.position + new Vector3(Random.Range(-difficulty, difficulty), Random.Range(-difficulty, difficulty)), Quaternion.identity);
            }
        }
	}

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

}
