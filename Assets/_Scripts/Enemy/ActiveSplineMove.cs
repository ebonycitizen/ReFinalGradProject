using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWS
{
    public class ActiveSplineMove : MonoBehaviour
    {
        [SerializeField]
        private List<splineMove> fishes;

        public void DoSplineMove()
        {
            fishes.ForEach(x => x.StartMove());
        }
    }
}
