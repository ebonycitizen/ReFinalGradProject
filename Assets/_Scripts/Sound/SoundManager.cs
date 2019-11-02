using System.Collections.Generic;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
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

        var clip = FindClipInBgmContainer(bgmType);

        if (!clip)
            return;
        else
        {
            m_bgmAudio.clip = clip;
            m_bgmAudio.Play();
        }
    }

    public void PlayBgm(EBgmTable bgmType, float volume)
    {
        if (m_bgmAudio.isPlaying)
            m_bgmAudio.Stop();

        //音量調整
        var clampVolume = Mathf.Clamp01(volume);

        var clip = FindClipInBgmContainer(bgmType);

        if (!clip)
            return;
        else
        {
            m_bgmAudio.volume = clampVolume;
            m_bgmAudio.clip = clip;
            m_bgmAudio.Play();
        }
    }

    public void StopAllBgm()
    {
        m_bgmAudio.Stop();
    }

    public void PlayOntShotSe(ESeTable seType)
    {
        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        var availableAudio = m_seAudioSources.Find(x => !x.isPlaying);
        if (!availableAudio)
        {
            availableAudio = this.gameObject.AddComponent<AudioSource>();
            m_seAudioSources.Add(availableAudio);
        }
        availableAudio.clip = clip;
        availableAudio.PlayOneShot(clip);
    }

    public void PlayOntShotSe(ESeTable seType, float volume)
    {

        var clampVolume = Mathf.Clamp01(volume);

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        var availableAudio = m_seAudioSources.Find(x => !x.isPlaying);
        if (!availableAudio)
        {
            availableAudio = this.gameObject.AddComponent<AudioSource>();
            m_seAudioSources.Add(availableAudio);
        }
        availableAudio.clip = clip;
        availableAudio.PlayOneShot(clip, clampVolume);
    }

    public void StopOntShotSe(ESeTable seType)
    {
        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;

        }
        var playingAudioSources = m_seAudioSources.Find(x => x.clip == clip);

        if (!m_seAudioSources.Exists(item => item.clip == clip))
        {
            Debug.LogError("該当のAudioSourceが見つかりません");
            return;
        }

        playingAudioSources.Stop();

    }
    
    public void StopMultipleSe(ESeTable seType)
    {
        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;

        }
        var playingAudioSources = m_seAudioSources.FindAll(x => x.clip == clip);

        if (!m_seAudioSources.Exists(item => item.clip == clip))
        {
            Debug.LogError("該当のAudioSourceが見つかりません");
            return;
        }

        playingAudioSources.ForEach(x => x.Stop());

    }

    public void Play3DSe(ESeTable seType, GameObject obj)
    {
        AudioSource soundItemSource = Create3DSpeaker(obj);

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        soundItemSource.clip = clip;

        soundItemSource.loop = true;
        soundItemSource.Play();
    }

    private AudioSource Create3DSpeaker(GameObject obj)
    {
        var soundItem = Instantiate(m_3dSoundItem, obj.transform);

        soundItem.transform.position = obj.transform.position;

        var soundItemSource = soundItem.GetComponent<AudioSource>();
        return soundItemSource;
    }

    public void Play3DSe(ESeTable seType, float volume, GameObject obj)
    {
        AudioSource soundItemSource = Create3DSpeaker(obj);

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        var clampVolume = Mathf.Clamp01(volume);
        soundItemSource.clip = clip;
        soundItemSource.loop = true;
        soundItemSource.Play();
    }

    public void Stop3DSe(ESeTable seType, GameObject obj)
    {
        var audioSource = obj.GetComponentInChildren<AudioSource>();

        if (!audioSource)
        {
            Debug.LogError("Cant found audiosource");
            return;
        }
        else
        {
            audioSource.Stop();
        }
    }


    private AudioClip FindClipInSeContainer(ESeTable eSeTable)
    {
        var selectItem = m_seItems.Find(x => x.SeType == eSeTable);

        if (!m_seItems.Exists(item => item.SeType == eSeTable))
        {
            Debug.LogError("該当のBgmが見つかりません");
            return null;
        }
        else
        {
            Debug.Log("Play" + selectItem.SeClip.name);
            return selectItem.SeClip;
        }

    }

    private AudioClip FindClipInBgmContainer(EBgmTable eBgmTable)
    {
        var selectItem = m_bgmItems.Find(x => x.BgmType == eBgmTable);

        if (!m_bgmItems.Exists(item => item.BgmType == eBgmTable))
        {
            Debug.LogError("該当のBgmが見つかりません");
            return null;
        }
        else
        {
            Debug.Log("Play" + selectItem.BgmClip.name);
            return selectItem.BgmClip;
        }

    }

}
