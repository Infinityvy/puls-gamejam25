using GameLogic;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class UISession : MonoBehaviour
    {
        public static UISession Instance;
        
        public AudioSource UIAudioSource { get; private set; }

        private void Awake()
        {
            Instance = this;
            UIAudioSource = GetComponent<AudioSource>();
            
            LevelManager.Init();
        }
    }
}
