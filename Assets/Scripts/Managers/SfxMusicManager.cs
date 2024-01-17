using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SfxMusicManager : TemporalSingleton<SfxMusicManager>
{
    private float m_sfxVolume = 0.2f;

    private Dictionary<string, AudioClip> m_sfxSoundDictionary = null;

    private AudioSource m_sfxMusic;

    private void Start()
    {
        m_sfxMusic = CreteAudioSource("Sfx", false);

        m_sfxSoundDictionary = new Dictionary<string, AudioClip>();

        AudioClip[] audioSfxVector = Resources.LoadAll<AudioClip>(AppPaths.PATH_RESOURCE_SFX);
        for (int i = 0; i < audioSfxVector.Length; i++)
        {
            m_sfxSoundDictionary.Add(audioSfxVector[i].name, audioSfxVector[i]);
        }
    }
    public AudioSource CreteAudioSource(string name, bool isLoop)
    {
        GameObject temporalAudioHost = new GameObject(name);
        AudioSource audioSource = temporalAudioHost.AddComponent<AudioSource>() as AudioSource;
        audioSource.playOnAwake = false;
        audioSource.loop = isLoop;
        audioSource.spatialBlend = 0.0f;
        temporalAudioHost.transform.SetParent(this.transform);
        return audioSource;
    }

    public void PlaySfxMusic(string audioName)
    {
        if (m_sfxSoundDictionary.ContainsKey(audioName))
        {
            m_sfxMusic.clip = m_sfxSoundDictionary[audioName];
            m_sfxMusic.volume = m_sfxVolume;
            m_sfxMusic.Play();
        }
    }

    public float SfxVolume
    {
        get
        {
            return m_sfxVolume;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_sfxMusic.volume = m_sfxVolume;
            m_sfxVolume = value;
        }
    }
    public float SfxVolumeSave
    {
        get
        {
            return m_sfxVolume;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_sfxMusic.volume = m_sfxVolume;
            //PlayerPrefs.SetFloat(AppPlayerPrefKeys.SFX_VOLUME, value);
            m_sfxVolume = value;
        }
    }
}