using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackFlowFishSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fishPrefab;
    [SerializeField]
    private FlowBoidCart cart;
    [SerializeField]
    private float fishMax;

    [SerializeField]
    private float speedMin = 20f;
    [SerializeField]
    private float speedMax = 25f;

    [SerializeField]
    private Vector3 scale = Vector3.one;

    [SerializeField]
    private Vector3 spawnBoundSize;

    [SerializeField]
    private float spawnDelayTime;

    [SerializeField]
    private float initPos=500;

    private List<GameObject> fishes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitFishes();
        SpawnFish();
    }
    public void SpawnFish()
    {
        cart.m_Speed = 35f;
        cart.m_Position = initPos;
        StartCoroutine("InitSpawn");
        StartCoroutine("Spawn");
    }
    private IEnumerator Spawn()
    {
        while(true)
        {
            for (int i = 0; i<fishes.Count; i++)
            {
                
                fishes[i].SetActive(true);
                fishes[i].GetComponent<TrackFlowFish>().pos = cart.m_Position;

                float x = Random.Range(-spawnBoundSize.x / 2, spawnBoundSize.x / 2);
                float y = Random.Range(-spawnBoundSize.y / 2, spawnBoundSize.y / 2);
                float z = Random.Range(- spawnBoundSize.z / 2, spawnBoundSize.z / 2);

                fishes[i].GetComponent<TrackFlowFish>().randOffset = new Vector3(x, y, z);

                yield return new WaitForSeconds(spawnDelayTime);
            }
        }
    }
    private IEnumerator InitSpawn()
    {

        float interval = initPos;
        for (int i = fishes.Count-1; i > 0; i--)
        {
            if (fishes[i].activeSelf == true)
                break;
            if (interval < 0)
                break;

            fishes[i].GetComponent<TrackFlowFish>().pos = initPos- interval;
            fishes[i].SetActive(true);

            float x = Random.Range(-spawnBoundSize.x / 2, spawnBoundSize.x / 2);
            float y = Random.Range(-spawnBoundSize.y / 2, spawnBoundSize.y / 2);
            float z = Random.Range(-spawnBoundSize.z / 2, spawnBoundSize.z / 2);

            fishes[i].GetComponent<TrackFlowFish>().randOffset = new Vector3(x, y, z);

            interval -= cart.m_Speed * spawnDelayTime;
        }
        for (int i = fishes.Count - 1; i > 0; i--)
        {
            if (fishes[i].activeSelf == true)
                continue;
            fishes[i].SetActive(true);

            float x = Random.Range(-spawnBoundSize.x / 2, spawnBoundSize.x / 2);
            float y = Random.Range(-spawnBoundSize.y / 2, spawnBoundSize.y / 2);
            float z = Random.Range(-spawnBoundSize.z / 2, spawnBoundSize.z / 2);

            fishes[i].GetComponent<TrackFlowFish>().randOffset = new Vector3(x, y, z);

            yield return new WaitForSeconds(spawnDelayTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitFishes()
    {
        for(int i=0;i<fishMax;i++)
        {
            var fishObj = Instantiate(fishPrefab, transform);
            var fish= fishObj.GetComponent<TrackFlowFish>();
            fish.speed = Random.Range(speedMin, speedMax);

            fish.cart = cart;
            fish.transform.localScale = scale * Random.Range(0.5f, 1f);
            fish.GetComponentInChildren<Animator>().speed = Random.Range(0.5f, 2f);

            fishes.Add(fishObj);
            fishObj.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnBoundSize);
    }
}
