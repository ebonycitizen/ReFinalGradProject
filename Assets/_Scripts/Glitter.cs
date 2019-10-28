using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitter : MonoBehaviour
{
    [SerializeField]
    private RayFromCamera rayCamera;

    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private ParticleSystem startLockOn;

    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color lockonColor;

    private GameObject lockTarget;

    // Start is called before the first frame update
    void Start()
    {
        lockTarget = null;
        startLockOn.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject cameraTarget = rayCamera.LockOn(targetLayer);

        if (cameraTarget != null && cameraTarget != lockTarget)
        {
            if (startLockOn.isStopped)
                startLockOn.Play();

            foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
            {
                p.startColor = lockonColor;
            }
        }

        if(cameraTarget == null && lockTarget != null)
        {
            foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
            {
                p.startColor = normalColor;
            }
        }

        lockTarget = cameraTarget;
    }
}
