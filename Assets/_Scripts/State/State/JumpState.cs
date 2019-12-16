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
        private float elaspedTime = 0f;
        private Sequence s;
        private Sequence s2;

        private Transform rayObject;

        private void MoveToGlitter()
        {
            orca.position = Vector3.Lerp(orca.position, rayObject.position, Time.fixedDeltaTime * 2f);
        }

        private void Move()
        {
            var direction = Quaternion.Euler(Context.idleTarget.localEulerAngles) * Context.cameraRig.transform.forward;

            var targetPos = direction * 7;

            orca.localPosition = Vector3.Lerp(orca.localPosition, targetPos, Time.fixedDeltaTime * 2f);
        }
        private void Jump(Vector3 pos)
        {
            s = DOTween.Sequence();

            var jumpDistance = 15f;
            s.Append(orca.DOLocalMoveY(orca.localPosition.y + jumpDistance, 1.5f).SetEase(Ease.InOutQuad))
            .Append(orca.DOLocalMoveY(orca.localPosition.y, 1f).SetEase(Ease.InOutQuad))
            .AppendCallback(() => stateMachine.SendEvent((int)StateEventId.Idle));

            s.Play();

        }

        private void Rotate()
        {
            s2 = DOTween.Sequence();

            s2
                .Append(orca.DOLocalRotate(new Vector3(-55, 0, 0), 0.5f).SetEase(Ease.Linear))
                .AppendCallback(() => Context.orcaAnim.SetTrigger("Jump"))
                .AppendInterval(0.2f)
                .Append(orca.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.Linear))
                .Append(orca.DOLocalRotate(new Vector3(55, 0, 0), 0.7f).SetEase(Ease.Linear))
                .Append(orca.DOLocalRotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.Linear));

            s2.Play();
        }

        protected internal override void Enter()
        {
            rayObject = Context.rayObject.transform;
            orca = Context.orcaModel.transform;
            Jump(orca.localPosition);
            Rotate();
            elaspedTime = 0f;
        }
        protected internal override void Update()
        {
            elaspedTime += Time.deltaTime;

            if (elaspedTime >= 1.5f)
                Move();
            else
                MoveToGlitter();

        }
        protected internal override void Exit()
        {

        }
    }
}
