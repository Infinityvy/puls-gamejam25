using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer mainMixer;

    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();


    [System.Serializable]
    private struct LoadCategory
    {
        public LoadCategory(string path, bool loadInScene)
        {
            this.path = path;
            this.loadInScene = loadInScene;
        }

        public string path;
        public bool loadInScene;
    }

    [SerializeField]
    private LoadCategory[] categoriesToLoad;


    private void Awake()
    {
        Instance = this;

        List<AudioClip> clips = new List<AudioClip>();

        foreach(LoadCategory lc in categoriesToLoad)
        {
            if(lc.loadInScene)
            {
                clips.AddRange(Resources.LoadAll<AudioClip>(lc.path));
            }
        }

        foreach(AudioClip cl in clips)
        {
            audioClips.Add(cl.name, cl);
        }

        SetGroupVolume(AudioGroup.Master, PlayerPrefs.GetFloat("MasterVolume", 50f));
        SetGroupVolume(AudioGroup.SFX, PlayerPrefs.GetFloat("SFXVolume", 50f));
        SetGroupVolume(AudioGroup.Music, PlayerPrefs.GetFloat("MusicVolume", 50f));
    }

    public void SetGroupVolume(AudioGroup group, float volume)
    {
        float dB = (volume > 0) ? Mathf.Log10(volume * 0.01f) * 20f : -80f;

        mainMixer.SetFloat(group.ToString() + "Volume", dB);

        PlayerPrefs.SetFloat(group.ToString() + "Volume", volume);
    }
    public void SetMasterVolume(Slider slider) { SetGroupVolume(AudioGroup.Master, slider.value); }
    public void SetSFXVolume(Slider slider) { SetGroupVolume(AudioGroup.SFX, slider.value); }
    public void SetMusicVolume(Slider slider) { SetGroupVolume(AudioGroup.Music, slider.value); }

    public float GetGroupVolume(AudioGroup group)
    {
        return PlayerPrefs.GetFloat(group.ToString() + "Volume");
    }

    public static AudioClip GetAudioClip(string key)
    {
        return Instance.audioClips[key];
    }

    public static AudioMixerGroup GetAudioMixerGroup(AudioGroup group)
    {
        switch(group)
        {
            case AudioGroup.Master:
                return Instance.mainMixer.FindMatchingGroups(group.ToString())[0];
            case AudioGroup.SFX:
            case AudioGroup.Music:
                return Instance.mainMixer.FindMatchingGroups(AudioGroup.Master.ToString() + "/" + group.ToString())[0];
            default:
                throw new System.Exception("Unkown AudioGroup.");
        }
    }
}
