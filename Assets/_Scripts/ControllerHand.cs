using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerHand : MonoBehaviour
{
    public bool FirstContact { get; set; }
    public SteamVR_Action_Vibration hapicAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Hapic");

    // Start is called before the first frame update
    void Start()
    {
        FirstContact = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControllerHaptic()
    {
        hapicAction.Execute(0, 0.5f, 1f, 100f, SteamVR_Input_Sources.RightHand);
    }
}
