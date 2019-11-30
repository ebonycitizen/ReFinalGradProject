using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhenStart : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_enableObjs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_enableObjs.ForEach(x => x.SetActive(true));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
