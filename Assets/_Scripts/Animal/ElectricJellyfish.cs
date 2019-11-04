using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricJellyfish : MonoBehaviour
{
    [SerializeField]
    private GameObject boomEffect;
    [SerializeField]
    private GameObject shockEffect;

    public GameObject GetBoomEffect()
    {
        return boomEffect;
    }

    public GameObject GetShockEffect()
    {
        return shockEffect;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
