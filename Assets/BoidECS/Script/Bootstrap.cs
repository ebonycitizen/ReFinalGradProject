using UnityEngine;
using Unity.Rendering;

namespace BoidECS
{

public class Bootstrap : MonoBehaviour 
{
    public static Bootstrap _Instance;

    public static Bootstrap Instance 
    { 
        get 
        { 
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

    void OnDrawGizmos()
    {
        if (!param) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, param.wallScale);
    }
}

}