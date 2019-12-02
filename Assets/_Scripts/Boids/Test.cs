using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

public class Test : MonoBehaviour
{
    [SerializeField]
    private const int MaxCount = 30;

    [SerializeField]
    private GameObject m_boidPrefab = null;

    [SerializeField]
    private GameObject m_boidParent = null;

    [SerializeField]
    private List<GameObject> m_boids = new List<GameObject>();

    [SerializeField]
    private float m_spawnInfluence = 5;

    [SerializeField]
    private GameObject m_target = null;

    [SerializeField]
    private GameObject m_center = null;

    [SerializeField]
    private float m_turbulence = 1;

    private void Start()
    {
        for (int i = 0; i < MaxCount; i++)
        {
            var boid = Instantiate(m_boidPrefab, m_boidParent.transform);

            var randomCircle = Random.insideUnitCircle * m_spawnInfluence;
            boid.transform.position = new Vector3(randomCircle.x
                , 0, randomCircle.y);

            m_boids.Add(boid);

        }
    }


    private void FixedUpdate()
    {
        Vector3 center = Vector3.zero;

        m_boids.ForEach(x => center += x.transform.position);

        center /= m_boids.Count - 1;

        center += m_target.transform.position;

        center /= 2;

        m_center.transform.position = center;

    }
}
