using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;
using System;
using Random = UnityEngine.Random;

namespace SWS
{
    public class PenguinAnimatorCtr : MonoBehaviour
    {
        [Serializable]
        public class CustomAnimationState
        {
            public string AnimationName;

            public string TriggerName;

            public bool onStart;

            public bool isPlaying;
        }

        [SerializeField]
        private List<CustomAnimationState> m_customAnimationStates = new List<CustomAnimationState>();


        [SerializeField]
        private Animator m_animator;

        [SerializeField]
        private splineMove m_move;

        [Range(1, 10)]
        [SerializeField]
        private float m_spurtMinSpeed = 2;

        [Range(1, 10)]
        [SerializeField]
        private float m_spurtMaxSpeed = 5;

        private float m_stuckRandomValue = 0;

        private float m_prevAnimatorSpeed = 0;

        // Start is called before the first frame update
        [Obsolete]
        void Start()
        {
            var state = m_customAnimationStates.Find(x => x.onStart);

            if (state != null)
            {
                PlayNextAnimation(state.AnimationName);
            }

        }
        public void PlaySlidAnimation()
        {
            StopAnimation();
            PlayNextAnimation("Slid");
        }
        public void JumpToSwim()
        {
            StopAnimation();
            m_animator.SetTrigger("JumpToSwim");
            m_animator.SetBool("PlaySwim", true);
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
                    }).AddTo(this);
            }
            else
            {
                //ランダムにストップする時間を延長させ
                Observable.Timer(TimeSpan.FromSeconds(Random.value))
                    .Subscribe(_ =>
                    {
                        PlayAnimation(name);
                    }).AddTo(this);
            }

        }

        public void StopAnimation()
        {
            //再生中のアニメーションがあるかどうかを調べる
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
        private void Spurt()
        {
            //length=speed*t; t=length/speed; 1*(10-2)*0.1=0.8 1*(10-5)*0.1=0.5  
            m_stuckRandomValue = Random.Range(m_spurtMinSpeed, m_spurtMaxSpeed);

            if (m_move)
                m_move.ChangeSpeed(m_move.speed * m_stuckRandomValue);

            m_prevAnimatorSpeed = m_animator.speed;
            var randomAnimationSpeed = m_animator.speed * (10 - m_stuckRandomValue) * 0.2f;
            m_animator.speed = randomAnimationSpeed;

            m_animator.SetTrigger("SwimIdle");
        }
        private void Resume()
        {
            m_animator.speed = m_prevAnimatorSpeed;
            if (m_move)
                m_move.ChangeSpeed(m_move.speed / m_stuckRandomValue);
        }
    }

}