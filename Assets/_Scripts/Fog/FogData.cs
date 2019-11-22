using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/Create FogData")]
public class FogData : ScriptableObject
{
    [Tooltip("Apply distance-based fog?")]
    public bool distanceFog = true;
    [Tooltip("Distance fog is based on radial distance from camera when checked")]
    public bool useRadialDistance = false;
    [Tooltip("Apply height-based fog?")]
    public bool heightFog = true;
    [Tooltip("Fog top Y coordinate")]
    public float height = 1.0f;
    [Range(0.001f, 10.0f)]
    public float heightDensity = 2.0f;
    [Tooltip("Push fog away from the camera by this amount")]
    public float startDistance = 0.0f;

    [ColorUsage(true, true)]
    public Color SkyColor;
    [ColorUsage(true, true)]
    public Color EquatorColor;
    [ColorUsage(true, true)]
    public Color GroundColor;

    public Color fogColor;
    public Material skybox;
}
