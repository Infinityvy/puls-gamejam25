using UnityEngine;
using UnityEngine.Audio;

public class UIAudioTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public void OnClick()
    {
        audioSource.PlaySound("button_press", 1, AudioGroup.SFX);
    }

    public void OnHover()
    {
        audioSource.PlaySound("button_hover", 1, AudioGroup.SFX);
    }

    public void OnValueChange(int groupIndex)
    {
        audioSource.PlaySoundIfReady("blip", 1, (AudioGroup)groupIndex);
    }
}
