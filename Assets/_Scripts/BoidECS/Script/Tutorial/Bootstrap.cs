using UnityEngine;
using Unity.Rendering;
using Unity.Mathematics;

namespace BoidECS
{

public class Bootstrap : MonoBehaviour 
{
    private static bool uu;

    public static Bootstrap _Instance;

    public static Bootstrap Instance 
    { 
        get 
        {
            if (_Instance == null && FindObjectOfType<Bootstrap>() != null)
                _Instance = FindObjectOfType<Bootstrap>();
            return _Instance ?? (_Instance = FindObjectOfType<Bootstrap>());
        }
    }

    public static bool IsValid
    {
        get { return Instance != null; }
    }

    [SerializeField]
    Param param;

    public static Param Param
    {
        get { return Instance.param; }
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
        if (i > 395)
            return Bootstrap.Boid.targetPos[11].position;
        if (i > 375)
            return Bootstrap.Boid.targetPos[10].position;
        if (i > 330)
            return Bootstrap.Boid.targetPos[9].position;
        if (i > 300)
            return Bootstrap.Boid.targetPos[8].position;
        #endregion

        #region Group 2
        if (i > 260)
            return Bootstrap.Boid.targetPos[7].position;
        if (i > 220)
            return Bootstrap.Boid.targetPos[6].position;
        if (i > 170)
            return Bootstrap.Boid.targetPos[5].position;
        #endregion

        #region Group 1
        if (i > 130)
            return Bootstrap.Boid.targetPos[4].position;
        if (i > 105)
            return Bootstrap.Boid.targetPos[3].position;
        if (i > 80)
            return Bootstrap.Boid.targetPos[2].position;
        if (i > 30)
            return Bootstrap.Boid.targetPos[1].position;
        if (i > 0)
            return Bootstrap.Boid.targetPos[0].position;
        #endregion
        return float3.zero;
    }

    public static Material SetSpawnMat(int i)
    {
        if (i > 300)
        {
            return Bootstrap.Boid.material[2];
        }

        if (i > 170)
        {
            return Bootstrap.Boid.material[1];
        }

        if (i > 0)
        {
            return Bootstrap.Boid.material[0];
        }
        return null;
    }

    private void OnEnable()
    {
            // boidInfo.hasDestroy = false;
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