using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface ITriggerSetupper
{
    void Setup();
}

public class PenguinFunction : MonoBehaviour, ITriggerSetupper
{
    public virtual void Setup()
    {
    }
}


public class PenguinTriggerCtr : MonoBehaviour
{
    [SerializeField]
    private List<PenguinFunction> m_penguinFunctions = new List<PenguinFunction>();

    private bool m_hasEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly") && !m_hasEntered)
        {
            m_hasEntered = true;
            m_penguinFunctions.ForEach(x => x.Setup());
        }

    }
}
