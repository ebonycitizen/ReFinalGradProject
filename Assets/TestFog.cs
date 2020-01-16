using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class TestFog : MonoBehaviour
{
    [SerializeField]
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "rending " + RenderSettings.fog.ToString()
            + "\n " +GameObject.FindObjectOfType<GlobalFog>().distanceFog.ToString()
            + "\n " + GameObject.FindObjectOfType<GlobalFog>().useRadialDistance.ToString()
            + "\n " + GameObject.FindObjectOfType<GlobalFog>().heightFog.ToString()
            + "\n " + GameObject.FindObjectOfType<GlobalFog>().height.ToString()
            + "\n " + GameObject.FindObjectOfType<GlobalFog>().heightDensity.ToString()
            + "\n " + GameObject.FindObjectOfType<GlobalFog>().startDistance.ToString();
    }
}
