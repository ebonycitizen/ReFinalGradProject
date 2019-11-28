using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using BehaviorDesigner.Runtime;

public partial class DolphinState : MonoBehaviour
{
    private enum StateEventId
    {
        None,
    }

    private ImtStateMachine<DolphinState> stateMachine;



    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new ImtStateMachine<DolphinState>(this);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
