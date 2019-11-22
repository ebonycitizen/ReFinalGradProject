using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class PenguinFall : MonoBehaviour
{
    [SerializeField]
    private GameObject penguin;

    private splineMove spline;
    private Speaker speaker;
    // Start is called before the first frame update
    void Start()
    {
        spline = penguin.GetComponent<splineMove>();
        speaker = penguin.GetComponentInChildren<Speaker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            //SoundManager.Instance.PlayOneShot3DSe(ESeTable.Drown, speaker, 0.1f);
            spline.StartMove();
            Destroy(gameObject);
        }
    }
}
