using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] transferObj;
    [SerializeField]
    private float UnloadWaitSec;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private IEnumerator Load(string scene)
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        foreach(GameObject obj in transferObj)
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName(scene));

        yield return new WaitForSeconds(4f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        yield return new WaitForSeconds(UnloadWaitSec);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void LoadNextScene(string scene)
    {
        StartCoroutine("Load", scene);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
