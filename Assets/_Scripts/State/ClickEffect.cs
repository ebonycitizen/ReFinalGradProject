using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    Transform orca;
    public bool hasDone { get; private set; }
    void Start()
    {
        hasDone = false;
    }
    void Update()
    {
        
    }

    public void StartEffect(Transform orca)
    {
        this.orca = orca;
        Destroy(gameObject);
        hasDone = true;
    }
}
