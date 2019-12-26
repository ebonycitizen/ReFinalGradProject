using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSing : PenguinFunction
{
    [SerializeField]
    private List<PenguinAnimatorCtr> m_penguins = new List<PenguinAnimatorCtr>();

    [SerializeField]
    private ParticleSystem m_singEffect = null;

    [SerializeField]
    private Speaker m_speaker = null;

    [SerializeField]
    private ESeTable m_eSe = ESeTable.Tmp_PenguinSinging;

    public override void Setup()
    {
        m_penguins.ForEach(x => x.PlayNextAnimation("Sing"));
        //m_singEffect.Play();
        SoundManager.Instance.PlayOneShot3DSe(m_eSe, m_speaker, 0.6f);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setup();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
