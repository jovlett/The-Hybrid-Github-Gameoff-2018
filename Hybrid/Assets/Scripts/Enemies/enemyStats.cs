using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemyStat", menuName = "Stats/New Enemy Stat", order = 1)]
public class enemyStats : ScriptableObject
{
    public float Speed;   //Speed of enemy
    public float Agility;   //Turn speed of enemy
}
