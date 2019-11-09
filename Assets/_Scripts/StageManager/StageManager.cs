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
    [SerializeField]
    private float waitActiveSec;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBgm(EBgmTable.Tutorial);
    }

    private IEnumerator Load(string scene)
    {
        Scene oldScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        foreach(GameObject obj in transferObj)
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName(scene));

        yield return new WaitForSeconds(waitActiveSec);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        ChangeBGM(scene);

        yield return new WaitForSeconds(UnloadWaitSec);
        SceneManager.UnloadSceneAsync(oldScene);
    }

    public void LoadNextScene(string scene)
    {
        StartCoroutine("Load", scene);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ChangeBGM(string scene)
    {
        SoundManager.Instance.StopAllBgm();
        if (scene == "TutorialF")
            SoundManager.Instance.PlayBgm(EBgmTable.Tutorial);
        if (scene == "SeasideF")
            SoundManager.Instance.PlayBgm(EBgmTable.Seaside);
        if (scene == "OceanF")
            SoundManager.Instance.PlayBgm(EBgmTable.Ocean);
        
    }
}
