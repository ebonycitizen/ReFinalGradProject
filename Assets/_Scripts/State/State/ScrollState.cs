using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class ScrollState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;

        private MyCinemachineDollyCart dolly;

        private GameObject camera;

        private Vector3 targetPos;

        private float limit = 20;

        private float num = 10;

        protected internal override void Enter()
        {
            Context.ChangeParentRayObject();

            orca = Context.orcaModel.transform;

            dolly = Context.dolly;

            camera = Context.cameraEye;

            targetPos = orca.position;

            Context.SetBehaviorStatus(true);
            Context.CanWave = true;

            SoundManager.Instance.PlayOneShot3DSe(ESeTable.Orac_5, orca.GetComponentInChildren<Speaker>());
        }
        protected internal override void Update()
        {
            

            var angle = camera.transform.eulerAngles.x;
            float maxLimit = limit, minLimit = -limit;

            if (angle > 180)
                angle = -(360 - angle);

           // angle += num;

            if (angle > limit )
                angle = limit;
            if (angle < minLimit)
                angle = minLimit;

            //angle = 0;

           Debug.Log(angle);

            orca.localPosition = Vector3.Lerp(orca.localPosition, Vector3.zero - new Vector3(0, angle*1.7f, 0), Time.fixedDeltaTime);
            orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(0, 0, 0), Time.fixedDeltaTime * 2f);

        }

        protected internal override void Exit()
        {
            Context.SetBehaviorStatus(false);
            Context.CanWave = false;
        }
    }
}