using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandAction : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem heart;

    [SerializeField]
    private float waveInvalidTime = 10f;

    [SerializeField]
    private HandAction handAction;
    [SerializeField]
    private ControllerHand controllerHand;
    [SerializeField]
    private OrcaState orcaState;
    [SerializeField]
    private Animator animator;

    private bool isDoingAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(handAction.HasWave && !isDoingAction && orcaState.CanWave)
        {
            StartCoroutine("DoAction");
        }
    }

    private IEnumerator DoAction()
    {
        isDoingAction = true;

        controllerHand.ControllerHaptic();

        heart.Play();
        animator.SetTrigger("Wave");
        SoundManager.Instance.PlayOneShotSe(ESeTable.Orac_4, 0.7f);

        yield return new WaitForSeconds(waveInvalidTime);

        isDoingAction = false;
    }
}
