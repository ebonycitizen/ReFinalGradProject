using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceCoralBone : EmissionAction
{
    private Vector3 scale;
    private Vector3 slimScale;

    protected override void Start()
    {
        scale = transform.localScale;
        slimScale = new Vector3(scale.x / 4, scale.y, scale.z / 4);

        transform.localScale = slimScale;

        base.Start();
    }

    public override void DoAction()
    {
        transform.DOScale(scale, 1f);
    }
}
