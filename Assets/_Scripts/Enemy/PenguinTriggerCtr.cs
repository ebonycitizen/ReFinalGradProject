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

    [SerializeField]
    private MyCinemachineDollyCart m_cart = null;

    [SerializeField]
    private float m_slowTime = 5;

    [SerializeField]
    private float m_slowSpeed = 10;

    private float m_prevSpeed = 0;

    private bool m_hasEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OrcaModel" && !m_hasEntered)
        {
            m_hasEntered = true;
            m_penguinFunctions.ForEach(x => x.Setup());

            //m_prevSpeed = m_cart.m_Speed;
            //m_cart.ChangeSpeed(m_slowSpeed);

            //Observable.Timer(TimeSpan.FromSeconds(m_slowTime))
            //    .Subscribe(_ => m_cart.ChangeSpeed(m_prevSpeed)).AddTo(this);
        }

    }
}
