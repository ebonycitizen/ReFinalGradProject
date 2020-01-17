using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    private class LightUpState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;
        private Spark spark;
        private Vector3 oldPos;

        private float moveSpeed = 50f;
        private Material originMat;
        private bool hasChangeMat;
        private bool hasTouch;

        private void MoveToRayObj()
        {
            if (Vector3.Distance(orca.position, rayObject.position) > 20f)
            {
                var dir = rayObject.position - orca.position;
                var rot = Quaternion.LookRotation(dir);
                orca.rotation = Quaternion.Lerp(orca.rotation, rot, Time.fixedDeltaTime * 2f);

                orca.position += orca.forward * Time.fixedDeltaTime * moveSpeed;
            }
            else
            {
                hasTouch = true;
                ChangeMat();
            }
        }

        private void MoveToCameraRig()
        {
            Vector3 targetPos = Context.cameraRig.transform.position + Context.cameraRig.transform.forward * 70f;
            orca.position = Vector3.Lerp(orca.position, targetPos, Time.fixedDeltaTime / 2.5f);

            var dir = targetPos - orca.position;
            var rot = Quaternion.LookRotation(dir);
            orca.rotation = Quaternion.Lerp(orca.rotation, rot, Time.fixedDeltaTime);

            if (Vector3.Distance(orca.position, Context.cameraRig.transform.position) < 40f)
            {
                stateMachine.SendEvent((int)StateEventId.Idle);
                Context.ChangeParentCameraRig();

            }
        }
        private void SetMat(Material mat, SkinnedMeshRenderer[] renderer)
        {
            foreach (var r in renderer)
                r.material = mat;
        }

        private void ChangeMat()
        {
            var renderer = orca.GetComponentsInChildren<SkinnedMeshRenderer>();
            originMat = renderer[0].material;

            Sequence s = DOTween.Sequence();

            s.AppendCallback(() => spark.StartUp())
                .AppendInterval(0.2f)
                .AppendCallback(() => SetMat(spark.GetScanMaterial(), renderer))
                .AppendInterval(0.8f)
                .AppendCallback(() => SetMat(originMat, renderer))
                .AppendCallback(() => stateMachine.SendEvent((int)StateEventId.Idle));

            s.Play();
        }

        protected internal override void Enter()
        {
            rayObject = Context.rayObject.transform;
            orca = Context.orcaModel.transform;
            spark = rayObject.GetComponent<Spark>();
            //oldPos = orca.transform.position;

            //Context.ChangeParentNull();

            //hasTouch = false;
            //hasChangeMat = false;

            spark.GetBgm().Play();
            ChangeMat();
            spark.GetSparkBgm().Play();
        }
        protected internal override void Update()
        {
            //if (hasTouch)
            //{
            //    MoveToCameraRig();
            //    //stateMachine.SendEvent((int)StateEventId.Idle);
            //}
            //else
            //{
            //    MoveToRayObj();
            //}
        }
        protected internal override void Exit()
        {
            
        }
    }
}
