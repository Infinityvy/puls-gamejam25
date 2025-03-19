using UnityEngine;

public static class AudioManagmentExtensions
{
    public static void PlaySound(this AudioSource source, string key, float volume, AudioGroup group)
    {
        source.outputAudioMixerGroup = AudioManager.GetAudioMixerGroup(group);

        source.PlaySound(key, volume);
    }
    public static void PlaySound(this AudioSource source, string key, float volume)
    {
        source.clip = AudioManager.GetAudioClip(key);
        source.volume = volume;
        source.Play();
    }

    public static void PlaySoundIfReady(this AudioSource source, string key, float volume, AudioGroup group)
    {
        source.outputAudioMixerGroup = AudioManager.GetAudioMixerGroup(group);

        source.PlaySoundIfReady(key, volume);
    }
    public static void PlaySoundIfReady(this AudioSource source, string key, float volume)
    {
        if (source.isPlaying) return;

        source.PlaySound(key, volume);
    }
}
