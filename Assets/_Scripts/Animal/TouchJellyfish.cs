using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TouchJellyfish : MonoBehaviour
{
    [SerializeField]
    private GameObject eyeEffect;
    [SerializeField]
    private GameObject handEffect;

    private GameObject cameraEye;

    // Start is called before the first frame update
    void Start()
    {
        cameraEye = Object.FindObjectOfType<RayFromCamera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            ElectricShock(collision.gameObject);
        }
    }

    private void ElectricShock(GameObject hand)
    {
        Sequence s = DOTween.Sequence();
        s.AppendCallback(() => hand.GetComponentInChildren<SkinnedMeshRenderer>().material.EnableKeyword("_EMISSION"))
            .AppendCallback(() => Instantiate(handEffect, hand.transform))
            .AppendCallback(() => Instantiate(eyeEffect, cameraEye.transform))
            .AppendInterval(0.6f)
            .AppendCallback(() => hand.GetComponentInChildren<SkinnedMeshRenderer>().material.DisableKeyword("_EMISSION"));
        s.Play();
    }
}
