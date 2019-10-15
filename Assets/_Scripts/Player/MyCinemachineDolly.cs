using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class MyCinemachineDolly : MonoBehaviour
{
    public CinemachinePathBase m_Path;

    public CinemachinePathBase.PositionUnits m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;

    [FormerlySerializedAs("m_CurrentDistance")]
    public float m_Position;

    void Update()
    {
        SetCartPosition(m_Position);
    }

    void SetCartPosition(float distanceAlongPath)
    {
        if (m_Path != null)
        {
            m_Position = m_Path.StandardizeUnit(distanceAlongPath, m_PositionUnits);
            transform.position = m_Path.EvaluatePositionAtUnit(m_Position, m_PositionUnits);
            var tangent = m_Path.EvaluateTangentAtUnit(m_Position, m_PositionUnits);
            tangent.y = 0; // Y方向を無効
            transform.rotation = Quaternion.LookRotation(tangent);
        }
    }
}