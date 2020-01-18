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

        private GameObject soulBall;
        private SkinnedMeshRenderer meshRenderer;
        private float rimStrength;

        private bool canMove;

        protected internal override void Enter()
        {
            Context.ChangeParentRayObject();
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject;

            rot = Context.idleRotation;
            rigid = orca.GetComponent<Rigidbody>();

            constraints = rigid.constraints;

            rigid.constraints = RigidbodyConstraints.FreezeAll;
            canMove = true;

            var colliders = orca.GetComponents<Collider>();
            foreach (var c in colliders)
                c.enabled = false;

            soulBall = Instantiate(rayObject.GetComponent<SendObj>().soulBall, orca);
            soulBall.name = "OrcaSoulBall";

            SetSecondMat();
            DissapearSeq();
        }

        private void SetSecondMat()
        {
            var meshes = orca.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshRenderer = meshes[1];
            var originMat = meshRenderer.material;
            var secondMat = rayObject.GetComponent<SendObj>().soulMat;

            rimStrength = secondMat.GetFloat("_RimStrength");

            //create second mat
            meshRenderer.materials = new Material[2];

            Material[] mats = meshRenderer.materials;
            mats[0] = originMat;
            mats[1] = secondMat;
            meshRenderer.materials = mats;

            //init soul mat
            meshRenderer.materials[1].SetFloat("_RimStrength", 10f);
        }            

        private void ChangeMat()
        {
            var dissolveMat = rayObject.GetComponent<SendObj>().dissolveMat;
            var renderer = orca.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var r in renderer)
            {
                r.material = dissolveMat;
                r.material.DOFloat(1f, "_BurnAmount", 50f);
            }

            meshRenderer.materials[1].DOFloat(rimStrength, "_RimStrength", 7f);
        }

        private void CreateEffect()
        {
            var eff = rayObject.GetComponent<SendObj>().dissapearEff;
            Instantiate(eff, orca);
        }

        private void DissapearSeq()
        {
            var speaker = orca.GetComponentInChildren<Speaker>();
            var animator = orca.GetComponentInChildren<Animator>();
            Sequence s = DOTween.Sequence();

            s.AppendCallback(() => SoundManager.Instance.PlayOneShot3DSe(ESeTable.Orac_5, speaker))
                .AppendInterval(1f)
                .AppendCallback(() => SoundManager.Instance.PlayOneShot3DSe(ESeTable.Disappear, speaker))
                .AppendCallback(() => ChangeMat())
                .AppendCallback(() => CreateEffect())
                .AppendInterval(14f)
                .AppendCallback(() => canMove = false)
                .AppendCallback(() => DOTween.To(() => animator.speed, (i) => animator.speed = i, 0f, 4f))
                .AppendCallback(() => soulBall.transform.parent = rayObject.transform.parent)
                .AppendCallback(() => orca.transform.parent = soulBall.transform);
                //.AppendCallback(() => Object.FindObjectOfType<ShowSoulOrca>().startShow = true)
                //.AppendCallback(() => orca.gameObject.SetActive(false));

            s.Play();
        }

        protected internal override void Update()
        {
            if (canMove)
            {
                orca.localPosition = Vector3.Lerp(orca.localPosition, Vector3.zero, Time.fixedDeltaTime * 0.5f);
                orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(0, 0, rot.localEulerAngles.z), Time.fixedDeltaTime * 2f);
            }
        }
        protected internal override void Exit()
        {
            rigid.constraints = constraints;
            Context.ChangeParentNull();
        }
    }
}