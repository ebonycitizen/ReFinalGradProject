using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGlitter : MonoBehaviour
{
    [SerializeField]
    private GameObject glitter;

    [SerializeField]
    private List<GameObject> m_glitters = new List<GameObject>();

    [SerializeField]
    private float duration;
    private Speaker speaker;

    // Start is called before the first frame update
    void Awake()
    {
        speaker = glitter.GetComponentInChildren<Speaker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            glitter.SetActive(true);

            if (m_glitters.Count > 0)
                m_glitters.ForEach(x => x.SetActive(true));

            if (duration > 0)
                Destroy(glitter, duration);

            SoundManager.Instance.PlayOneShot3DSe(ESeTable.Twinkle, speaker, 0.5f);
            
            Destroy(gameObject);
        }
    }
}
