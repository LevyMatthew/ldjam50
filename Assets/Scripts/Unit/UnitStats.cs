using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Stats")]
public class UnitStats : ScriptableObject
{
    public int walkSpeed;
    public int runSpeed;
    public int maxSpeed;
    public float turnSpeed;
    public int maxHealth;
    public int armor;
    public int attackDamage;
    public int attackSpeed;
    public int mass;
    public string description;
}
