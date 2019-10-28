using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    private class JumpState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        Sequence s;
        Sequence s2;

        private void Move()
        {
            var direction = Quaternion.Euler(Context.idleTarget.localEulerAngles) * Context.cameraRig.transform.forward;

            var targetPos = direction * 7;

            orca.localPosition = Vector3.Lerp(orca.localPosition, targetPos, Time.fixedDeltaTime * 2f);
        }

        private void Jump(Vector3 pos)
        {
            s = DOTween.Sequence();
            s.Append(orca.DOLocalMoveY(orca.localPosition.y + 6, 1).SetEase(Ease.InOutQuad))
                .Append(orca.DOLocalMoveY(orca.localPosition.y, 1).SetEase(Ease.InQuad))
                .AppendCallback(()=> stateMachine.SendEvent((int)StateEventId.Idle));

            s.Play();
        }

        private void Rotate()
        {
            s2 = DOTween.Sequence();
            s2.Append(orca.DOBlendableLocalRotateBy(new Vector3(-45, 0, 0), 0.5f).SetEase(Ease.InOutQuad))
                .Append(orca.DOBlendableLocalRotateBy(new Vector3(90, 0, 0), 1f).SetEase(Ease.InOutQuad))
                .Append(orca.DOBlendableLocalRotateBy(new Vector3(-45, 0, 0), 1))
                ;
                
            s2.Play();
        }

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            Jump(orca.localPosition);
            Rotate();
            //Context.ChangeParentRayObject();
        }
        protected internal override void Update()
        {
            //Move();

            if (Input.GetKeyDown(KeyCode.A))
            {
                
                //stateMachine.SendEvent((int)StateEventId.Idle);
            }
        }
        protected internal override void Exit()
        {
        }
    }
}
