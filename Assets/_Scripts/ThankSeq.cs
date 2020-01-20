using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class ThankSeq : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject thank;
    void Start()
    {
        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 0f);
        StartCoroutine("StartUp");

        thank.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartUp()
    {
        //yield return new WaitForSeconds(0.5f);

        SteamVR_Fade.Start(new Color(0, 0, 0, 0), 2f);
        yield return new WaitForSeconds(4f);

        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 2f);
        yield return new WaitForSeconds(2f);
        thank.SetActive(true);
        title.SetActive(false);
        SteamVR_Fade.Start(new Color(0, 0, 0, 0), 2f);
        yield return new WaitForSeconds(4f);

        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 2f);
        yield return new WaitForSeconds(2f);

        var gameObj = GameObject.Find("SoundManager");
        if (gameObj != null)
            Destroy(gameObj);

        SceneManager.LoadSceneAsync("TitleF", LoadSceneMode.Single);
    }
}
