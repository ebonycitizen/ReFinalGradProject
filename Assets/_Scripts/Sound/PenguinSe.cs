using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSe : MonoBehaviour
{
    [SerializeField]
    private Speaker speaker;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayLoop3DSe(ESeTable.Penguin_1, speaker, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
