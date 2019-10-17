using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWS
{
    public class DushController : MonoBehaviour
    {
        [SerializeField] private splineMove move;

        [SerializeField] private float startSpeed = 0;

        [SerializeField] private float dushSpeed = 0;

        // Start is called before the first frame update
        void Start()
        {
            move.speed = startSpeed;
            move.StartMove();
        }
        /// <summary>
        /// アニメーションから呼び出し
        /// </summary>
        void Dush()
        {
            move.ChangeSpeed(dushSpeed);
        }

        /// <summary>
        /// アニメーションから呼び出し
        /// </summary>
        void Rest()
        {
            move.ChangeSpeed(startSpeed);
        }
    }
}


