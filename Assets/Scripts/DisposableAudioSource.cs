using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class DisposableAudioSource : MonoBehaviour
{
    private AudioSource audioSource;

    private bool initiated = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Session.Instance.gameSpeedUpdatedEvent.AddListener(UpdatePitch);
        Session.Instance.resetEvent.AddListener(OnReset);

        UpdatePitch();

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (!initiated) return;

        if (!audioSource.isPlaying) DestroyThis();
    }

    private void Init(AudioClip clip, AudioMixerGroup mixerGroup)
    {
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = mixerGroup;

        audioSource.Play();
        initiated = true;
    }

    private void UpdatePitch()
    {
        audioSource.pitch = Session.Instance.gameSpeed;
    }

    private void OnReset()
    {
        audioSource.Stop();
        DestroyThis();
    }

    private void DestroyThis()
    {
        initiated = false;
        Session.Instance.gameSpeedUpdatedEvent.RemoveListener(UpdatePitch);
        Session.Instance.resetEvent.RemoveListener(OnReset);

        Destroy(gameObject);
    }

    public static void Play(AudioClip clip, AudioMixerGroup mixerGroup)
    {
        GameObject g = new GameObject();
        g.AddComponent<AudioSource>();
        DisposableAudioSource das = g.AddComponent<DisposableAudioSource>();
        das.Init(clip, mixerGroup);
    }
}
