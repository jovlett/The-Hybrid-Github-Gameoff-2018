using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database/New enemyDB", order = 1)]
public class enemyDB : ScriptableObject
{
    public GameObject[] enemies;
    public GameObject healer;
}
