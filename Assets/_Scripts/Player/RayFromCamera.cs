using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayFromCamera : MonoBehaviour
{
    [SerializeField]
    private Transform rayPos;
    [SerializeField]
    private Image lockOnImg;


    private float rayLegth = 150;
    private RaycastHit hit;
    private Vector3 rayDirection;
    private GameObject hitObject;

    private GameObject lockObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLockUI(float amount, float max)
    {
        lockOnImg.fillAmount = Mathf.Clamp(amount / max, 0, 1);
    }

    public void ResetLockOnImg()
    {
        lockOnImg.fillAmount = 0f;
    }

    public GameObject LockOn(LayerMask layerMask)
    {
        rayDirection = (rayPos.position - transform.position).normalized;
        
        Debug.DrawRay(transform.position, rayDirection * rayLegth);
        bool isHit = Physics.Raycast(transform.position, rayDirection, out hit, rayLegth, layerMask);

        if (isHit)
        {
            lockObj = hit.transform.gameObject;
            return hit.transform.gameObject;
        }

        lockObj = null;

        return null;
    }
    public Vector3 ScrollHit(string layer)
    {
        rayDirection = (rayPos.position - transform.position).normalized;

        Debug.DrawRay(transform.position, rayDirection * rayLegth);
        bool isHit = Physics.Raycast(transform.position, rayDirection, out hit, rayLegth, 1<<LayerMask.NameToLayer(layer));

        if (isHit)
            return hit.point;

        return Vector3.zero;
    }
}
