using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    [SerializeField]
    private string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.DoFadeOutBgm();
        }
#endif

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Dolly"))
            return;

        StageManager stageManager = Object.FindObjectOfType<StageManager>();
        if (stageManager != null)
            stageManager.LoadNextScene(nextScene);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
    }
}
