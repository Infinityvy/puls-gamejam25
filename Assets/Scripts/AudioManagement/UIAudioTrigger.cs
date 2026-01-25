using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace AudioManagement
{
    public class UIAudioTrigger : MonoBehaviour
    {
        private AudioSource audioSource;
        
        private Button button;

        private void Start()
        {
            audioSource = UISession.Instance.UIAudioSource;
            button = GetComponent<Button>();
        }

        public void OnClick()
        {
            audioSource.PlaySound("button_press", 1, AudioGroup.SFX);
        }

        public void OnHoverEnter()
        {
            if(button != null && !button.interactable) return;
            
            audioSource.PlaySound("button_hover", 1, AudioGroup.SFX);
        }

        public void OnValueChange(int groupIndex)
        {
            audioSource.PlaySoundIfReady("blip", 1, (AudioGroup)groupIndex);
        }
    }
}
