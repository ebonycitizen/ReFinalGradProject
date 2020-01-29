using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HandAction : MonoBehaviour
{
    [SerializeField]
    private VelocityEstimator VE;
    [SerializeField]
    private float threshold;
    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;
    [SerializeField]
    private Material waveMat;

    public bool HasWave { get; private set; }

    private Material originMat;

    // Start is called before the first frame update
    void Start()
    {
        originMat = meshRenderer.material;    
    }

    // Update is called once per frame
    void Update()
    {
        var acceleration = Mathf.Abs(VE.GetVelocityEstimateX());
        if (threshold < acceleration)
        {
            //Debug.Log(acceleration);
            //meshRenderer.material = waveMat;
            HasWave = true;
        }
        else
        {
           // meshRenderer.material = originMat;
            HasWave = false;
        }

    }


}
