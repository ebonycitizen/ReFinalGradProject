using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public enum EBgmTable
{
    Default,
    Stage1,
}
public enum ESeTable
{
    Twinkle,
    Action,
}


namespace Stage1
{
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [Serializable]
        public class BgmItem
        {
            public EBgmTable BgmType;

            public AudioClip BgmClip;
        }

        [SerializeField]
        private List<BgmItem> m_bgmItems = new List<BgmItem>();

        [Serializable]
        public class SeItem
        {
            public ESeTable SeType;

            public AudioClip SeClip;
        }

        [SerializeField]
        private List<SeItem> m_seItems = new List<SeItem>();

        [SerializeField]
        private AudioSource m_bgmAudio = null;

        [SerializeField]
        private List<AudioSource> m_seAudioSources = new List<AudioSource>();

        [SerializeField]
        private GameObject m_3dSoundItem = null;


        public void PlayBgm(EBgmTable bgmType)
        {
            if (m_bgmAudio.isPlaying)
                m_bgmAudio.Stop();

            var clip = m_bgmItems.Find(x => x.BgmType == bgmType).BgmClip;

            if (!clip)
            {
                Debug.LogError("該当のBgmが見つかりません");
                return;
            }

            m_bgmAudio.clip = clip;
            m_bgmAudio.Play();
        }

        public void PlayBgm(EBgmTable bgmType, float volume)
        {
            if (m_bgmAudio.isPlaying)
                m_bgmAudio.Stop();

            var clampVolume = Mathf.Clamp01(volume);

            m_bgmAudio.volume = clampVolume;

            var clip = m_bgmItems.Find(x => x.BgmType == bgmType).BgmClip;

            if (!clip)
            {
                Debug.LogError("該当のBgmが見つかりません");
                return;
            }

            m_bgmAudio.clip = clip;
            m_bgmAudio.Play();
        }
        public void StopBgm()
        {
            m_bgmAudio.Stop();
        }

        public void PlayOntShotSe(ESeTable seType)
        {
            var clip = m_seItems.Find(x => x.SeType == seType).SeClip;

            if (!clip)
            {
                Debug.LogError("該当のSeが見つかりません");
                return;
            }

            var availableAudio = m_seAudioSources.Find(x => !x.isPlaying);

            availableAudio.PlayOneShot(clip);
        }

        public void PlayOntShotSe(ESeTable seType, float volume)
        {
            var clip = m_seItems.Find(x => x.SeType == seType).SeClip;

            if (!clip)
            {
                Debug.LogError("該当のSeが見つかりません");
                return;
            }

            var clampVolume = Mathf.Clamp01(volume);

            var availableAudio = m_seAudioSources.Find(x => !x.isPlaying);

            availableAudio.PlayOneShot(clip, clampVolume);
        }

        public void StopSe(ESeTable seType)
        {
            AudioClip clip = m_seItems.Find(x => x.SeType == seType).SeClip;

            if (!clip)
            {
                Debug.LogError("該当のSeが見つかりません");
                return;
            }
            var playingAudioSource = m_seAudioSources.Find(x => x.clip == clip);

            playingAudioSource.Stop();
        }
        public void Play3DSe(ESeTable seType, Transform obj)
        {
            var soundItem = Instantiate(m_3dSoundItem, obj);

            soundItem.transform.position = obj.position;

            var soundItemSource = soundItem.GetComponent<AudioSource>();

            var clip = m_seItems.Find(x => x.SeType == seType).SeClip;

            if (!clip)
            {
                Debug.LogError("該当のSeが見つかりません");
                return;
            }

            var availableAudio = m_seAudioSources.Find(x => !x.isPlaying);

            availableAudio.PlayOneShot(clip);
        }

        public void Play3DSe(ESeTable seType, float volume, Transform obj)
        {
            var soundItem = Instantiate(m_3dSoundItem, obj);

            soundItem.transform.position = obj.position;

            var soundItemSource = soundItem.GetComponent<AudioSource>();

            var clip = m_seItems.Find(x => x.SeType == seType).SeClip;

            if (!clip)
            {
                Debug.LogError("該当のSeが見つかりません");
                return;
            }

            var clampVolume = Mathf.Clamp01(volume);

            var availableAudio = m_seAudioSources.Find(x => !x.isPlaying);

            availableAudio.PlayOneShot(clip, clampVolume);

        }
    }
}