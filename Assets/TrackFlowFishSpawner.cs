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
    private float speed=20;
    [SerializeField]
    private float speedMax;
    [SerializeField]
    private float speedMin;

    [SerializeField]
    private Vector3 spawnBowndSize;

    [SerializeField]
    private float spawnDelayTime;

    private List<GameObject> fishes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitFishes();
        SpawnFish();
    }
    public void SpawnFish()
    {
        StartCoroutine("InitSpawn");
        StartCoroutine("Spawn");
        cart.m_Speed = 35f;
    }
    private IEnumerator Spawn()
    {
        while(true)
        {
            for (int i = 0; i<fishes.Count; i++)
            {
                
                fishes[i].SetActive(true);
                fishes[i].GetComponent<TrackFlowFish>().pos = cart.m_Position;

                yield return new WaitForSeconds(spawnDelayTime);
            }
        }
    }
    private IEnumerator InitSpawn()
    {

        for (int i = fishes.Count-1; i > 0; i--)
        {
            if (fishes[i].activeSelf == true)
                break;

            fishes[i].SetActive(true);

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
            var fishObj = Instantiate(fishPrefab);
            var fish= fishObj.GetComponent<TrackFlowFish>();
            fish.speed = speed;
            fish.speedMax = speedMax;
            fish.speedMin = speedMin;
            fish.cart = cart;
            fish.transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
            fish.GetComponentInChildren<Animator>().speed = Random.Range(0.5f, 2f);

            fishes.Add(fishObj);
            fishObj.SetActive(false);
        }
    }
}
