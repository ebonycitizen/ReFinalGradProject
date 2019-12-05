using UnityEngine;
using Unity.Rendering;
using Unity.Mathematics;

namespace BoidECSCoral
{

    public class BootstrapCoral : MonoBehaviour
    {
        public static BootstrapCoral _Instance;

        public static BootstrapCoral Instance
        {
            get
            {
                return _Instance ?? (_Instance = FindObjectOfType<BootstrapCoral>());
            }
        }

        public static bool IsValid
        {
            get { return Instance != null; }
        }

        [SerializeField]
        ParamCoral param;

        public static ParamCoral Param
        {
            
            get
            {
                if (Instance != null)
                    return Instance.param;
                else
                    return null;
            }
        }



        [System.Serializable]
        public struct BoidInfo
        {
            public int count;
            public Vector3 scale;
            public RenderMesh renderer;
            public Transform selfTransform;
            public Transform[] targetPos;
            public Material[] material;
            public bool hasDestroy;

        }

        [SerializeField]
        BoidInfo boidInfo = new BoidInfo
        {
            count = 100,
            scale = new Vector3(0.1f, 0.1f, 0.3f),
        };

        public static BoidInfo Boid
        {
            get { return Instance.boidInfo; }
        }

        public static float3 SetSpawnTarget(int i)
        {
            #region Group 3
            if (i > 560)
                return BootstrapCoral.Boid.targetPos[15].position;
            if (i > 520)
                return BootstrapCoral.Boid.targetPos[14].position;
            if (i > 470)
                return BootstrapCoral.Boid.targetPos[13].position;
            if (i > 440)
                return BootstrapCoral.Boid.targetPos[12].position;
            if (i > 410)
                return BootstrapCoral.Boid.targetPos[11].position;
            #endregion

            #region Group 2
            if (i > 360)
                return BootstrapCoral.Boid.targetPos[10].position;
            if (i > 310)
                return BootstrapCoral.Boid.targetPos[9].position;
            if (i > 280)
                return BootstrapCoral.Boid.targetPos[8].position;
            if (i > 260)
                return BootstrapCoral.Boid.targetPos[7].position;
            if (i > 230)
                return BootstrapCoral.Boid.targetPos[6].position;
            if (i > 190)
                return BootstrapCoral.Boid.targetPos[5].position;
            #endregion

            #region Group 1
            if (i > 140)
                return BootstrapCoral.Boid.targetPos[4].position;
            if (i > 90)
                return BootstrapCoral.Boid.targetPos[3].position;
            if (i > 60)
                return BootstrapCoral.Boid.targetPos[2].position;
            if (i > 30)
                return BootstrapCoral.Boid.targetPos[1].position;
            if (i > 0)
                return BootstrapCoral.Boid.targetPos[0].position;
            #endregion
            return float3.zero;
        }

        public static Material SetSpawnMat(int i)
        {
            if (i > 410)
            {
                return BootstrapCoral.Boid.material[2];
            }

            if (i > 190)
            {
                return BootstrapCoral.Boid.material[1];
            }

            if (i > 0)
            {
                return BootstrapCoral.Boid.material[0];
            }
            return BootstrapCoral.Boid.material[0];
        }

        private void OnDisable()
        {
            boidInfo.hasDestroy = true;
        }

        void OnDrawGizmos()
        {
            if (!param) return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, param.wallScale);
        }
    }

}