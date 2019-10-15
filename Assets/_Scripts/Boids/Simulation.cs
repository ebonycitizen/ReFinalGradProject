using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Simulation : MonoBehaviour
{

    [SerializeField]
    int boidCount = 10;
    [SerializeField]
    GameObject boidPrefab;

    [SerializeField]
    GameObject goalPrefab;

    [SerializeField]
    Param param;

    [SerializeField]
    Transform spawnPosition;
    [SerializeField]
    float spawnRadius;

    List<Boid> boids_ = new List<Boid>();
    public ReadOnlyCollection<Boid> boids
    {
        get { return boids_.AsReadOnly(); }
    }

    void AddBoid()
    {
        var go = Instantiate(boidPrefab, Random.insideUnitSphere * spawnRadius + spawnPosition.position, Random.rotation);
        go.transform.SetParent(transform);
        var boid = go.GetComponent<Boid>();
        boid.simulation = this;
        boid.param = param;
        boid.wallPos = transform.position;

        boid.goalPrefab = goalPrefab;

        boids_.Add(boid);


    }

    void RemoveBoid()
    {
        if (boids_.Count == 0) return;

        var lastIndex = boids_.Count - 1;
        var boid = boids_[lastIndex];
        Destroy(boid.gameObject);
        boids_.RemoveAt(lastIndex);
    }

    private void Start()
    {
        AddBoid();
    }

    void Update()
    {
        while (boids_.Count < boidCount)
        {
            AddBoid();
        }
        while (boids_.Count > boidCount)
        {
            RemoveBoid();
        }
    }

    void OnDrawGizmos()
    {
        if (!param) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, param.wallScale);
    }
}
