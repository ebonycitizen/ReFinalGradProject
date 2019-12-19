using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ChangeFog : MonoBehaviour
{
    [SerializeField]
    private Color fogColor;

    private GlobalFog globalFog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            StartCoroutine("ChangeColor");
        }
    }

    private IEnumerator ChangeColor()
    {
        globalFog = GameObject.FindObjectOfType<GlobalFog>();
        float elaspedTime = 0f;
        while (elaspedTime < 1f)
        {
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, fogColor, elaspedTime);
            elaspedTime += Time.deltaTime;
            yield return null;
        }
    }
}
