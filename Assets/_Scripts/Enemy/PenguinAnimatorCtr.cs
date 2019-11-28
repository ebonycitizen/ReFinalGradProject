using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;
using System;
using Random = UnityEngine.Random;

public class PenguinAnimatorCtr : MonoBehaviour
{
    [Serializable]
    public class CustomAnimationState
    {
        public string AnimationName;

        public string TriggerName;

        public bool onStart;

#if DEBUG
        public bool isPlaying;
#endif
    }

    [SerializeField]
    private List<CustomAnimationState> m_customAnimationStates = new List<CustomAnimationState>();


    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private NavMeshAgent m_agent;

    // Start is called before the first frame update
    void Start()
    {
        var state = m_customAnimationStates.Find(x => x.onStart);

        if (state != null)
        {
            PlayNextAnimation(state.AnimationName);
        }

    }
    public void JumpToSwim()
    {
        StopAnimation();
        m_animator.SetTrigger("JumpToSwim");
    }

    public void SwimToIdle()
    {
        m_animator.SetTrigger("SwimToIdle");
    }

    public void PlayNextAnimation(string name)
    {
        //再生中のアニメーションがあるかどうかを調べる
        var playingState = m_customAnimationStates.Find(x => x.isPlaying);

        //あるなら
        if (playingState != null)
        {
            //ランダムにストップする時間を延長させ
            Observable.Timer(TimeSpan.FromSeconds(Random.value))
                .Subscribe(_ =>
                {
                    StopAnimation();
                    PlayAnimation(name);
                });
        }
        else
        {
            //ランダムにストップする時間を延長させ
            Observable.Timer(TimeSpan.FromSeconds(Random.value))
                .Subscribe(_ =>
                {
                    PlayAnimation(name);
                });
        }

    }

    public void StopAnimation()
    {
        var playingState = m_customAnimationStates.Find(x => x.isPlaying);

        if (playingState != null)
        {
            m_animator.SetBool(playingState.TriggerName, false);

            playingState.isPlaying = false;
        }
        else
        {
            Debug.LogError("再生中のアニメーションがない");
        }
    }

    public void PlayAnimation(string name)
    {
        var state = FindAnimationState(name);

        m_animator.SetBool(state.TriggerName, true);

        state.isPlaying = true;
    }
    private CustomAnimationState FindAnimationState(string name)
    {
        var state = m_customAnimationStates.Find(x => x.AnimationName == name);

        if (state == null)
        {
            Debug.LogError("該当のアニメーションが見つかりません");
            return null;
        }
        else
        {
            return state;
        }

    }
}
