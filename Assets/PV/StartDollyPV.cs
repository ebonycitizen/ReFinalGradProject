using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDollyPV : MonoBehaviour
{
    public float speed = 20f;
    MyCinemachineDollyCart dollyCart;
    // Start is called before the first frame update
    void Start()
    {
        dollyCart = GameObject.FindObjectOfType<MyCinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            dollyCart.m_Speed = speed;
    }
}
