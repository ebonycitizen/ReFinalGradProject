using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaterialParm : MonoBehaviour
{
    [SerializeField]
    private Material normal;
    public Material GetNormalMat()
    {
        return normal;
    }

    [SerializeField]
    private Material fade;
    public Material GetFadeMat()
    {
        return fade;
    }   
}
