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

        [SerializeField]
        private splineMove m_move;

        [Range(1, 10)]
        [SerializeField]
        private float m_spurtMinSpeed = 2;

        [Range(1, 10)]
        [SerializeField]
        private float m_spurtMaxSpeed = 5;

        [SerializeField]
        private ParticleSystem m_bubbleEffect = null;

        [Range(0, 1)]
        [SerializeField]
        private float m_animatorSpeed = 1;

        private float m_stuckRandomValue = 0;

        private float m_prevAnimatorSpeed = 0;

        // Start is called before the first frame update
        [Obsolete]
        void Start()
        {
            var state = m_customAnimationStates.Find(x => x.onStart);

            if (state != null)
            {
                if (state.AnimationName != "Swim")
                {
                    m_animator.speed = m_animatorSpeed;
                    PlayNextAnimation(state.AnimationName);
                }
                else
                {
                    //再生スピードに0.1-0.00範囲の乱数を入れて調整する
                    var value = Random.value;
                    m_animator.speed = 1 - 0.2f * (value);

                    var value2 = Random.Range(5, 7);
                    Observable.Interval(TimeSpan.FromSeconds(value2))
                        .Subscribe(_ =>
                        {
                            m_bubbleEffect.Play();
                            Observable.Timer(TimeSpan.FromSeconds(m_bubbleEffect.startLifetime)).Subscribe(_m =>
                            {
                                m_bubbleEffect.Stop();
                            });
                        }).AddTo(this);

                    PlayAnimation(state.AnimationName);
                }
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

            m_move.ChangeSpeed(m_move.speed * m_stuckRandomValue);

            m_prevAnimatorSpeed = m_animator.speed;
            var randomAnimationSpeed = m_animator.speed * (10 - m_stuckRandomValue) * 0.2f;
            m_animator.speed = randomAnimationSpeed;

            m_animator.SetTrigger("SwimIdle");
        }
        private void Resume()
        {
            m_animator.speed = m_prevAnimatorSpeed;
            m_move.ChangeSpeed(m_move.speed / m_stuckRandomValue);
        }
    }

}