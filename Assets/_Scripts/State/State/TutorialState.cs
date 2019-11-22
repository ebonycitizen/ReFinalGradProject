using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    private class TutorialState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;
        private PathMoveEvent path;

        private float ratio;

        private bool gotoPath;

        private Speaker speaker;

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            path = rayObject.GetComponent<PathMoveEvent>();

            speaker = orca.GetComponentInChildren<Speaker>();

            gotoPath = true;
            ratio = 0;

            SoundManager.Instance.PlayOneShotDelay3DSe(ESeTable.Orac_7, speaker, 2);
        }
        protected internal override void Update()
        {
            if (rayObject == null)
                return;
            if (!gotoPath)
            {
                orca.position = Vector3.Lerp(orca.position, rayObject.position, Time.fixedDeltaTime);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), Time.fixedDeltaTime);

                return;
            }


            if (Vector3.Distance(orca.position, rayObject.position) > 10f)
            {
                var dir = rayObject.position - orca.position;
                var rot = Quaternion.LookRotation(dir);
                orca.rotation = Quaternion.Lerp(orca.rotation, rot, Time.fixedDeltaTime);

                orca.position += orca.forward * Time.deltaTime * 12f;
            }
            else
            {
                gotoPath = false;
                path.startEvent();
            }


            
        }
        protected internal override void Exit()
        {
            //Destroy(path.gameObject);
        }
    }
}