using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBgm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SoundManager.Instance.PlayBgm(EBgmTable.Tutorial);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SoundManager.Instance.PlayBgm(EBgmTable.Island);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SoundManager.Instance.PlayBgm(EBgmTable.Ocean);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SoundManager.Instance.PlayBgm(EBgmTable.TestCave, 0.7f);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SoundManager.Instance.PlayBgm(EBgmTable.TestStream);
    }
}
