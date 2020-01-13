using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HI5.VRInteraction;

public class StartBtnScale : MonoBehaviour
{
    [SerializeField]
    private VRInteractiveItem item;
    [SerializeField]
    private Transform transform;

    private Vector3 originScale;
    private Vector3 enlargeScale;

    private Vector3 targetScale;

    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
        enlargeScale = originScale * 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        targetScale = item.IsOver ? enlargeScale : originScale;

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 10f);
    }
}
