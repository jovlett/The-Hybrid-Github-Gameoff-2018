using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerStat", menuName = "Stats/New Player Stat", order = 1)]
public class playerStats : ScriptableObject
{
    public float Speed;   //Speed of player
    public float Agility;   //Turn speed of player

    public float SwitchDuration;   //Amount of time that the player is in 1 mode

    public float SpikeCooldown;   //Amount of time the player has to wait after dashing to be able to dash again
    public float SpikeTargetDistance;   //How far the dash will shoot
    public float SpikeDamage;   //How much damage dashing will do to an enemy

    public float GunCooldown;   //Amount of time the player has to wait before shooting another bullet
    public float GunBulletSpeed;   //The speed a bullet travels once shot
    public float GunBulletDamage;   //The damage the bullet does to enemies

    public int TokenAmount = 0;
}
