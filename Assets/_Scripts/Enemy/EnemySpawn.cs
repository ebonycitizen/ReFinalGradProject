using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float destroyTime;

    public float GetFadeTime()
    {
        return fadeTime;
    }

    public float GetMoveTime()
    {
        return moveTime;
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
