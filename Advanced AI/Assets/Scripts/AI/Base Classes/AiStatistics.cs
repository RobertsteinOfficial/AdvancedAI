using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIJob
{
    Worker,
    Soldier,
    Colonel
}

[CreateAssetMenu(menuName = "Scriptable AI/Stats", fileName = "AI Stat")]
public class AiStatistics : ScriptableObject
{
    [Header("General Movement")]
    public float walkSpeed;
    public float runSpeed;
    [Header("Working")]
    public AIJob myJob;
    public GameObject workingStation;
    [Header("Survival")]
    [Range(0,100)]
    public float hungerThreshold;
    [Range(0,100)]
    public float tiredThreshold;
    [Header("Senses")]
    public AnimationCurve sightDissipation;
    public AnimationCurve sightDissipation2;
    public AnimationCurve sightDissipation3;
    [Min(0)]
    public float hearRange;
    [Range(0,360)]
    public float sightAngle;
    [Min(0)]
    public float sightRange;

}
