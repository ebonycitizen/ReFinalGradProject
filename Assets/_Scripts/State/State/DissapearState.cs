using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    public class DissapearState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private GameObject rayObject;

        private Transform rot;
        private Rigidbody rigid;
        private RigidbodyConstraints constraints;

        protected internal override void Enter()
        {
            //Context.ChangeParentRayObject();
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject;

            rot = Context.idleRotation;
            rigid = orca.GetComponent<Rigidbody>();

            constraints = rigid.constraints;

            rigid.constraints = RigidbodyConstraints.FreezeAll;

            DissapearSeq();
        }

        private void ChangeMat()
        {
            var material = rayObject.GetComponent<SendObj>().material;
            var renderer = orca.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var r in renderer)
            {
                r.material = material;
                r.material.DOFloat(1f, "_BurnAmount", 15f);
            }
        }

        private void CreateEffect()
        {
            var eff = rayObject.GetComponent<SendObj>().dissapearEff;
            Instantiate(eff, orca);
        }

        private void DissapearSeq()
        {
            var speaker = orca.GetComponentInChildren<Speaker>();

            Sequence s = DOTween.Sequence();

            s.AppendCallback(() => SoundManager.Instance.PlayOneShot3DSe(ESeTable.Orac_5, speaker))
                .AppendInterval(1f)
                .AppendCallback(() => ChangeMat())
                .AppendCallback(() => CreateEffect())
                .AppendInterval(10f)
                .AppendCallback(() => Object.FindObjectOfType<ShowSoulOrca>().startShow = true)
                .AppendCallback(() => orca.gameObject.SetActive(false));

            s.Play();
        }

        protected internal override void Update()
        {
            
        }
        protected internal override void Exit()
        {
            rigid.constraints = constraints;
            Context.ChangeParentNull();
        }
    }
}