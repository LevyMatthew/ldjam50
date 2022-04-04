using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Difficulty/DifficultySettings")]
public class DifficultySettings : ScriptableObject
{
    [Header("Spawn Rates")]
    [Range(0f,360f),Tooltip("What angle should the spawn range start at (in degrees)")]
    public float angleOffset;
    [Range(0f,360f),Tooltip("How big is the spawn arc (in degrees)")]
    public float angleRange;
    [Range(0f,60f), Tooltip("Time between spawns (in seconds)")]
    public float interval;
    [Range(0f,300f), Tooltip("Distance to spawn units from center of world")]
    public float radius;

}