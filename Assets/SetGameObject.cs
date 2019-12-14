using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObject : MonoBehaviour
{

    [SerializeField]
    private string setParentName;
    [SerializeField]
    private Transform setObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            Transform parent = GameObject.Find("DollyCart").transform;
            if (parent==null)
                return;

            var setObjPos = setObj.position;

            setObj.parent = parent;

            setObj.localPosition = setObjPos;
        }
    }
}
