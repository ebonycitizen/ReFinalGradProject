using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LookAt : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private Quaternion m_initQuaternion;

    private bool m_useLookAt;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        m_initQuaternion = transform.rotation;
    }

    public void ResetRotation(float duration)
    {
        transform.DORotateQuaternion(m_initQuaternion, duration);
    }

    public void DoLookAt(float duration)
    {
        transform.DOLookAt(target.transform.position, duration);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
