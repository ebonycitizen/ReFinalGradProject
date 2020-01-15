using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCaribrator : MonoBehaviour
{
    [SerializeField]
    private Transform cameraRig;
    [SerializeField]
    private Transform eyePos;
    [SerializeField]
    private Transform offsetEyePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Callibrate();
        }
    }

    public void Callibrate()
    {
        var gapPos = offsetEyePos.position - eyePos.position;
        cameraRig.position += gapPos;
    }
}
