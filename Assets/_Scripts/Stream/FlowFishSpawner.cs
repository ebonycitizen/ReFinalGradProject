using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFishSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fishPrefab;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private int maxParticle;

    [SerializeField]
    private int rateOverTime;
    [SerializeField]
    private float lifeTimeMin;
    [SerializeField]
    private float lifeTimeMax;

    [SerializeField]
    private float speedMin;
    [SerializeField]
    private float speedMax;

    [SerializeField]
    private Vector3 spawnBoundSize;

    public void SpawnFish()
    {
        StartCoroutine("Spawn");
    }
    private IEnumerator Spawn()
    {
        while(true)
        {
            if (transform.childCount >= maxParticle)
                yield break;

            GameObject[] fish = new GameObject[rateOverTime];
            for(int i = 0;i < rateOverTime;i++)
            {
                float x = Random.Range(transform.position.x - spawnBoundSize.x / 2, transform.position.x + spawnBoundSize.x / 2);
                float y = Random.Range(transform.position.y - spawnBoundSize.y / 2, transform.position.y + spawnBoundSize.y / 2);
                float z = Random.Range(transform.position.z - spawnBoundSize.z / 2, transform.position.z + spawnBoundSize.z / 2);

                fish[i] = Instantiate(fishPrefab, new Vector3(x, y, z), fishPrefab.transform.rotation, transform);
                fish[i].transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
                fish[i].GetComponentInChildren<Animator>().speed = Random.Range(0.5f, 2f);

                var flowFish = fish[i].GetComponent<FlowFish>();

                flowFish.direction = direction;
                flowFish.speedMin = speedMin;
                flowFish.speedMax = speedMax;

                float lifeTime = Random.Range(lifeTimeMin, lifeTimeMax);

                Destroy(fish[i], lifeTime);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnBoundSize);
    }
}
