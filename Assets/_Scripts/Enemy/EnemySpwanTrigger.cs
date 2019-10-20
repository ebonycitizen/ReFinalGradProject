using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWS
{

    public class EnemySpwanTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameObject spawnObj;

        [SerializeField]
        private ActiveSplineMove activeScript;

        private void OnTriggerEnter(Collider other)
        {
            if (spawnObj.activeSelf || other.gameObject.layer != LayerMask.NameToLayer("Dolly"))
                return;

            spawnObj.SetActive(true);

            if (activeScript)
                activeScript.DoSplineMove();
        }
    }
}