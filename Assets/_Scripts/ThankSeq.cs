using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class ThankSeq : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 0f);
        StartCoroutine("StartUp");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartUp()
    {
        yield return new WaitForSeconds(2f);

        SteamVR_Fade.Start(new Color(0, 0, 0, 0), 2f);
        yield return new WaitForSeconds(4f);

        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 2f);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadSceneAsync("TitleF", LoadSceneMode.Single);
    }
}
