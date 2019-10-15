using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{

    public GameObject fishPrefab;
    public GameObject goalPrefab;

    public static int tankSize = 10;

    static int numFish = 10;
    public static GameObject[] allFish = new GameObject[numFish];

    public static Vector3 goalPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<numFish;i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
            allFish[i] =Instantiate(fishPrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Random.Range(0,10000)<50)
        //{
        //    goalPos= new Vector3(Random.Range(-tankSize, tankSize),
        //        Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));

        //    goalPrefab.transform.position = goalPos;
        //}
        goalPos = goalPrefab.transform.position;
    }
}
