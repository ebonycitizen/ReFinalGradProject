using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/DolphinParam")]
public class DolphinParam : ScriptableObject
{
    public float initSpeed = 2f;
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float wallDistance = 3f;
    public float wallWeight = 1f;
    public float neighborDistance = 1f;
    public float separationWeight = 5f;
    public float alignmentWeight = 2f;
    public float cohesionWeight = 3f;
}
