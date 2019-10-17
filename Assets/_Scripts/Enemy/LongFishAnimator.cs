using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWS
{
    public class LongFishAnimator : MonoBehaviour
    {
        [SerializeField]
        private splineMove move;

        [SerializeField]
        private float restSpeed = 0;

        [SerializeField]
        private float spurtSpeed = 0;

        [SerializeField]
        private Animator animator;

        void Rest()
        {
            move.ChangeSpeed(restSpeed);
        }
        public void DoSpurt()
        {

        }

        void Spurt()
        {
            move.ChangeSpeed(spurtSpeed);
        }
    }
}
