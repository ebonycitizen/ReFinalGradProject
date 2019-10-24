using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject deadEffect;

    [SerializeField]
    private GameObject gemPrefab;

    [SerializeField]
    private float num;

    [SerializeField]
    private Transform directionPos;
    private Vector3 direction;

    public GameObject GetDeadEffect()
    {
        return deadEffect;
    }

    public void SpawnGem()
    {
        while (0 < num)
        {
            var gem = Instantiate(gemPrefab, transform.position, Quaternion.identity);
            gem.GetComponent<Gem>().Init(direction);

            num--;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        direction = (directionPos.position - transform.position).normalized;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
