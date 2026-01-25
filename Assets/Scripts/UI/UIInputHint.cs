using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class UIInputHint : MonoBehaviour
    {
        [SerializeField]
        private Sprite keyReleasedSprite;

        [SerializeField]
        private Sprite keyPressedSprite;

        private Image image;

        private Button button;
        private bool isButton = false;

        public InputHintState State { get; private set; } = InputHintState.RELEASED;

        private void Start()
        {
            image = GetComponent<Image>();

            isButton = TryGetComponent(out button);
        }

        public void SetState(InputHintState state)
        {
            this.State = state;

            switch(state)
            {
                case InputHintState.RELEASED:
                    if (!isButton) image.sprite = keyReleasedSprite;
                    if (isButton) button.interactable = true;
                    break;
                case InputHintState.PRESSED:
                    if(!isButton) image.sprite = keyPressedSprite;
                    if (isButton) button.interactable = false;
                    break;
                default:
                    break;
            }
        }
    }
}
