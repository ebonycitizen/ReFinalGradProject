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
            if (i > 870)
                return BootstrapCoral.Boid.targetPos[11].position;
            if (i > 800)
                return BootstrapCoral.Boid.targetPos[11].position;
            if (i > 750)
                return BootstrapCoral.Boid.targetPos[10].position;
            if (i > 710)
                return BootstrapCoral.Boid.targetPos[9].position;
            if (i > 680)
                return BootstrapCoral.Boid.targetPos[8].position;
            #endregion

            #region Group 2
            if (i > 580)
                return BootstrapCoral.Boid.targetPos[7].position;
            if (i > 480)
                return BootstrapCoral.Boid.targetPos[7].position;
            if (i > 430)
                return BootstrapCoral.Boid.targetPos[7].position;
            if (i > 380)
                return BootstrapCoral.Boid.targetPos[7].position;
            if (i > 350)
                return BootstrapCoral.Boid.targetPos[6].position;
            if (i > 310)
                return BootstrapCoral.Boid.targetPos[5].position;
            #endregion

            #region Group 1
            if (i > 210)
                return BootstrapCoral.Boid.targetPos[4].position;
            if (i > 110)
                return BootstrapCoral.Boid.targetPos[3].position;
            if (i > 80)
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
            Unity.Mathematics.Random random = new Unity.Mathematics.Random(853);
            if (i > 680)
            {
                if (random.NextBool())
                    return BootstrapCoral.Boid.material[4];
                else
                    return BootstrapCoral.Boid.material[5];
            }

            if (i > 310)
            {
                if (random.NextBool())
                    return BootstrapCoral.Boid.material[2];
                else
                    return BootstrapCoral.Boid.material[3];
            }

            if (i > 0)
            {
                if (random.NextBool())
                    return BootstrapCoral.Boid.material[0];
                else
                    return BootstrapCoral.Boid.material[1];
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