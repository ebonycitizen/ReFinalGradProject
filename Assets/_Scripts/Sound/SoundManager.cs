using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    private BgmContainer m_bgmContainer = null;

    [SerializeField]
    private SeContainer m_seContainer = null;

    [SerializeField]
    private AudioSource m_bgmAudio = null;

    [SerializeField]
    private List<AudioSource> m_seAudioSources = new List<AudioSource>();

    public void DoFadeInBgm(EBgmTable bgmType, float duration = 3, float volume = 1)
    {
        if (m_bgmAudio.isPlaying)
            m_bgmAudio.Stop();

        if (m_bgmAudio.volume != 0)
            m_bgmAudio.volume = 0;

        var clip = FindClipInBgmContainer(bgmType);

        if (!clip)
            return;
        else
        {
            m_bgmAudio.clip = clip;
            m_bgmAudio.Play();
            m_bgmAudio.DOFade(volume, duration);
        }
    }
    public void DoFadeOutBgm(float duration = 3)
    {
        m_bgmAudio.DOFade(0, duration).OnComplete(() => m_bgmAudio.Stop());

    }

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

    public void FadeAllSe(float duration)
    {
        foreach (var se in m_seAudioSources)
        {
            se.DOFade(0, duration);
        }
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

    public void PlayOneShotDelaySe(ESeTable seType, float delay)
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
        availableAudio.PlayDelayed(delay);
    }

    public void PlayOneShotSe(ESeTable seType, float volume)
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

    public void PlayLoopSe(ESeTable seType)
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
        availableAudio.Play();
    }

    public void PlayLoopSe(ESeTable seType, float volume)
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
        availableAudio.volume = clampVolume;
        availableAudio.Play();
    }

    public void StopSelectedSe(ESeTable seType)
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

    public void StopAllSelectedSe(ESeTable seType)
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

    public void PlayLoop3DSe(ESeTable seType, Speaker speaker)
    {
        var audioSource = speaker.AudioSource;

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        audioSource.clip = clip;

        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayLoop3DSe(ESeTable seType, Speaker speaker, float volume)
    {
        var audioSource = speaker.AudioSource;

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        audioSource.clip = clip;

        audioSource.loop = true;

        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void PlayLoopDelay3DSe(ESeTable seType, Speaker speaker, float volume, float delaySec)
    {
        var audioSource = speaker.AudioSource;

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        audioSource.clip = clip;

        audioSource.loop = true;

        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        audioSource.PlayDelayed(delaySec);
    }

    public void PlayOneShot3DSe(ESeTable seType, Speaker speaker)
    {
        var audioSource = speaker.AudioSource;

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        //audioSource.clip = clip;

        audioSource.PlayOneShot(clip);
    }

    public void PlayOneShotDelay3DSe(ESeTable seType, Speaker speaker, float delaySec)
    {
        var audioSource = speaker.AudioSource;

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        audioSource.clip = clip;


        audioSource.PlayDelayed(delaySec);//audioSource.PlayOneShot(clip);
    }

    public void PlayOneShot3DSe(ESeTable seType, Speaker speaker, float volume)
    {
        var audioSource = speaker.AudioSource;

        var clip = FindClipInSeContainer(seType);

        if (!clip)
        {
            return;
        }

        //audioSource.clip = clip;
        volume = Mathf.Clamp01(volume);
        audioSource.PlayOneShot(clip, volume);
    }

    public void Stop3DSe(ESeTable seType, Speaker speaker)
    {
        var audioSource = speaker.AudioSource;

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
        var selectItem = m_seContainer.SE.Find(x => x.SeType == eSeTable);

        if (!m_seContainer.SE.Exists(item => item.SeType == eSeTable))
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
        var selectItem = m_bgmContainer.BGM.Find(x => x.BgmType == eBgmTable);

        if (!m_bgmContainer.BGM.Exists(item => item.BgmType == eBgmTable))
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
