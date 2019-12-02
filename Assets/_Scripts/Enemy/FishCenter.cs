using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCenter : MonoBehaviour
{
    [SerializeField]
    private Transform m_center;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = m_center.GetChild(m_center.childCount - 2).transform.position;
    }
}
