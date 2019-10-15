using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Boid/Param")]
public class Param : ScriptableObject
{
    public float initSpeed = 2f;
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float neighborDistance = 1f;
    public float neighborFov = 90f;
    public float separationWeight = 5f;
    public Vector3 wallScale = Vector3.one * 5f;
    public float wallDistance = 3f;
    public float wallWeight = 1f;
    public float alignmentWeight = 2f;
    public float cohesionWeight = 3f;
}
