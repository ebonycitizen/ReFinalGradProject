using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    public class PlayerJumpState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform player;
        Sequence s;
        
        private Vector3 oldPos;
        private void Rotate()
        {
            var d = player.position - oldPos;
            if (d.magnitude > 0)
            {
                var q = Quaternion.LookRotation(d);

                if (q.eulerAngles != Vector3.zero)
                {
                    //orca.localEulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, rot.localEulerAngles.z);
                    orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, 0), 0.2f);
                }

            }

            oldPos = player.position;

        }
        private void Come()
        {
            s = DOTween.Sequence();
            s.Append(orca.DOLocalMove(new Vector3(0, -0.05f, 0.8f), 1).SetEase(Ease.InOutQuad))
                .Append(player.DOLocalMoveY(70, 2).SetEase(Ease.OutQuad))
                .AppendInterval(0.3f)
                .Append(player.DOLocalMoveY(0, 2).SetEase(Ease.InQuad))
                .AppendCallback(() => stateMachine.SendEvent((int)StateEventId.Idle));

            s.Play();
        }
        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            player = Context.transform.parent;
            Context.ChangeParentCameraRig();
            
            Come();
        }
        protected internal override void Update()
        {
            Rotate();
        }
        protected internal override void Exit()
        {

        }
    }
}