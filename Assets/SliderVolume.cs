using UnityEngine;
using UnityEngine.Audio;

public class SliderVolume : SliderController
{
    [SerializeField]
    private AudioMixer audioMixer;

    private void Start()
    {
        valuePrefix = "Volume: ";

        OnValueChanged();
    }

    public override void OnValueChanged()
    {
        SetValueText();
        SetVolume(slider.value);
    }

    protected override void ResetSlider()
    {
        // do nothing
    }

    private void SetVolume(float volume)
    {
        float dB = (slider.value > 0) ? Mathf.Log10(slider.value / 100f) * 20f : -80f;
        audioMixer.SetFloat("MasterVolume", dB);
    }
}
