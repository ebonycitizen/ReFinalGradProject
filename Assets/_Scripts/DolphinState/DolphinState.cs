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
        Idle,
        Come,
        Swim,
        Jump,
    }

    private enum JumpType
    {
        Jump1,
        Jump2,
        Jump3,
    }

    [SerializeField] //for debug
    private GameObject rayObject;

    private GameObject sendObj;

    [SerializeField]
    private DolphinBoid boid;
    [SerializeField]
    private DolphinSimulation simulation;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject bigSplash;
    [SerializeField]
    private GameObject waterSplash;

    [SerializeField]
    private JumpType jumpType;

    [SerializeField]
    private Speaker speaker;

    private ImtStateMachine<DolphinState> stateMachine;

    // Start is called before the first frame update
    void Awake()
    {
        stateMachine = new ImtStateMachine<DolphinState>(this);

        stateMachine.AddAnyTransition<D_IdleState>((int)StateEventId.Idle);

        stateMachine.AddTransition<D_IdleState, D_ComeState>((int)StateEventId.Come);

        stateMachine.AddTransition<D_IdleState, D_SwimState>((int)StateEventId.Swim);

        stateMachine.AddTransition<D_ComeState, D_SwimState>((int)StateEventId.Swim);

        stateMachine.AddTransition<D_JumpState, D_SwimState>((int)StateEventId.Swim);

        stateMachine.AddTransition<D_SwimState, D_JumpState>((int)StateEventId.Jump);

        stateMachine.SetStartState<D_IdleState>();
    }
    private void Start()
    {
        boid.enabled = false;
        simulation.InitBoid(boid);

        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.Update();
        //Debug.Log(stateMachine.CurrentStateName);
    }
    private void ChangeParentNull()
    {
        transform.parent = null;
    }
    private void ChangeParentSendObj()
    {
        if (sendObj == null)
            return;

        transform.parent = sendObj.transform;
    }
    public bool ChangeState(string tag, GameObject obj)
    {
        sendObj = obj;

        if (tag == "D_Idle")
        {
            stateMachine.SendEvent((int)StateEventId.Idle);
            return true;
        }
        if (tag == "D_Come")
        {
            stateMachine.SendEvent((int)StateEventId.Come);
            return true;
        }
        if (tag == "D_Swim")
        {
            stateMachine.SendEvent((int)StateEventId.Swim);
            return true;
        }
        if (tag == "D_Jump")
        {
            stateMachine.SendEvent((int)StateEventId.Jump);
            return true;
        }


        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jump")
        {
            Debug.Log(stateMachine.CurrentStateName);
            if (stateMachine.CurrentStateName != "D_SwimState")
                return;

            switch(jumpType)
            {
                case JumpType.Jump1:
                    animator.SetTrigger("Jump1");
                    break;
                case JumpType.Jump2:
                    animator.SetTrigger("Jump2");
                    break;
                case JumpType.Jump3:
                    animator.SetTrigger("Jump3");
                    break;
            }
            
            stateMachine.SendEvent((int)StateEventId.Jump);
            Instantiate(bigSplash, other.ClosestPoint(transform.position), bigSplash.transform.rotation);
            SoundManager.Instance.PlayOneShotSe(ESeTable.JumpOutWater, 0.1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag== "Jump")
        {
            stateMachine.SendEvent((int)StateEventId.Swim);
            simulation.ChangeExit();
            Instantiate(bigSplash, other.ClosestPoint(transform.position), bigSplash.transform.rotation);
            Instantiate(waterSplash, other.ClosestPoint(transform.position), waterSplash.transform.rotation);
            SoundManager.Instance.PlayOneShotSe(ESeTable.JumpIntoWater, 0.1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}