using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{

    [SerializeField]
    private GameObject gemPrefab;

    [SerializeField]
    private float num;
    private void OnDestroy()
    {
        while(0<num)
        {
            var gem = Instantiate(gemPrefab, transform.position, Quaternion.identity);

            num--;
        }
    }
}
