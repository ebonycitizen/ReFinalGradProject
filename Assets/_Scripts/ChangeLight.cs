using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    [SerializeField]
    [ColorUsage(true, true)]
    private Color skyColor;
    [SerializeField]
    [ColorUsage(true, true)]
    private Color equatorColor;
    [SerializeField]
    [ColorUsage(true, true)]
    private Color groundColor;

    [SerializeField]
    private float duration = 1f;

    [SerializeField]
    private bool resetColor = false;
    [SerializeField]
    private float resetSec;

    private Color skyColorN;
    private Color equatorColorN;
    private Color groundColorN;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ChangeColor()
    {
        float elaspedTime = 0f;
        while (elaspedTime < duration)
        {
            RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, skyColor, elaspedTime);
            RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, equatorColor, elaspedTime);
            RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, groundColor, elaspedTime);

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        if (resetColor)
        {
            yield return new WaitForSeconds(resetSec);
            StartCoroutine("ResetColor");
        }
    }

    private IEnumerator ResetColor()
    {
        float elaspedTime = 0f;
        while (elaspedTime < duration)
        {
            RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, skyColorN, elaspedTime);
            RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, equatorColorN, elaspedTime);
            RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, groundColorN, elaspedTime);

            elaspedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            skyColorN = RenderSettings.ambientSkyColor;
            equatorColorN = RenderSettings.ambientEquatorColor;
            groundColorN = RenderSettings.ambientGroundColor;

            StartCoroutine("ChangeColor");
        }
    }
}
