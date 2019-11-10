using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    private class ElectricShock : ImtStateMachine<OrcaState>.State
    {
        private float distOffset = 10f;

        private Transform orca;
        private Transform rayObject;
        private Animator animator;
        private ElectricJellyfish jellyfish;

        private bool canMove;

        private void MoveForward()
        {
            if (Vector3.Distance(orca.position, rayObject.position) <= distOffset * 2f)
                canMove = false;

            orca.position = Vector3.Lerp(orca.position, rayObject.position - Vector3.up * distOffset, Time.fixedDeltaTime * 2f);
        }

        private void EnableEmission()
        {
            var renderer = orca.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var r in renderer)
                r.material.EnableKeyword("_EMISSION");
        }
        private void DisableEmission()
        {
            var renderer = orca.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var r in renderer)
                r.material.DisableKeyword("_EMISSION");
        }

        private void SetMovement()
        {
            Sequence s = DOTween.Sequence();
            s.AppendInterval(0.6f)
                .AppendCallback(()=> Instantiate(jellyfish.GetBoomEffect(),rayObject))

                .AppendInterval(0.1f)
                .AppendCallback(()=> Context.ChangeParentRayObject())
                .AppendCallback(()=>SoundManager.Instance.PlayOneShot3DSe(ESeTable.Call, orca.GetComponentInChildren<Speaker>()))
                .AppendCallback(() => animator.speed = 0f)
                .AppendCallback(() => EnableEmission())
                .AppendCallback(()=>Instantiate(jellyfish.GetShockEffect(),orca))
                .Join(orca.DOShakePosition(2f,0.2f))
                .Join(orca.DORotate( new Vector3(-29,12,-134), 0.3f))

                .AppendCallback(() => DisableEmission())
                .AppendCallback(() => animator.speed = 1f)
                .AppendCallback(() => Context.ChangeParentCameraRig())
                .AppendCallback(()=> stateMachine.SendEvent((int)StateEventId.Idle));
                
            s.Play();
        }

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            animator = orca.GetComponentInChildren<Animator>();
            jellyfish = rayObject.GetComponent<ElectricJellyfish>();
            
            canMove = true;

            SetMovement();
        }
        protected internal override void Update()
        {
            if(canMove)
                MoveForward();
        }
        protected internal override void Exit()
        {

        }
    }
}
