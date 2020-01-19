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
        //Callibrate();
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
        var gapPosY = offsetEyePos.position.y - eyePos.position.y;
        cameraRig.position += new Vector3(0, gapPosY);
    }
}
