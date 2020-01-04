using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private OrcaState orcaState;
    [SerializeField]
    private GameObject sendObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            orcaState.ChangeState("G_Dissapear", sendObj);
            sendObj.SetActive(false);
        }
    }
}
