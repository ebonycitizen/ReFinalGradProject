using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadA", 1);
    }
    void LoadA()
    {
        SceneManager.UnloadSceneAsync("Tutorial");
        SceneManager.LoadScene("Stage1", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
