using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SWS;
using UniRx;
using System;

public class MoveTest : MonoBehaviour
{
    [SerializeField]
    private Transform m_orca = null;

    [SerializeField]
    private splineMove m_splineMove;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            m_splineMove.StopAllCoroutines();
            m_splineMove.Stop();
            m_splineMove.enabled = false;

            m_splineMove.pathContainer = CreatePathToNearPoint();
            m_splineMove.StartMove();
        }

    }

    private PathManager CreatePathToNearPoint()
    {
        GameObject newPath = new GameObject("Path7 (Runtime Creation)");
        PathManager path = newPath.AddComponent<PathManager>();
        var forward = 70;
        var left = 30;
        var speed = 50;

        //declare waypoint positions
        Vector3[] positions = new Vector3[]
        {
            transform.position,
            m_orca.position+m_orca.forward*(forward+2*speed),
            m_orca.position+m_orca.forward*(forward+speed)+m_orca.right*-1*30,
            m_orca.position+m_orca.forward*(forward+3*speed),


        };

        Transform[] waypoints = new Transform[positions.Length];

        //instantiate waypoints
        for (int i = 0; i < positions.Length; i++)
        {
            GameObject newPoint = new GameObject("Waypoint " + i);
            waypoints[i] = newPoint.transform;
            waypoints[i].position = positions[i];
        }

        //assign waypoints to path
        path.Create(waypoints, true);

        //optional for visibility in the build
        newPath.AddComponent<PathRenderer>();
        newPath.GetComponent<LineRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        newPath.GetComponent<LineRenderer>().startWidth = 0.5f;
        return path;
    }
}

