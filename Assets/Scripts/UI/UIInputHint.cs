using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIInputHint : MonoBehaviour
{
    [SerializeField]
    private Sprite keyReleasedSprite;

    [SerializeField]
    private Sprite keyPressedSprite;

    private Image image;
    private TextMeshProUGUI tmpro;

    private Button button;
    private bool isButton = false;

    public InputHintState state { get; private set; } = InputHintState.RELEASED;

    private void Start()
    {
        image = GetComponent<Image>();
        tmpro = GetComponentInChildren<TextMeshProUGUI>();

        isButton = TryGetComponent(out button);
    }

    public void SetState(InputHintState state)
    {
        this.state = state;

        switch(state)
        {
            case InputHintState.RELEASED:
                if (!isButton) image.sprite = keyReleasedSprite;
                tmpro.color = Color.white;
                if (isButton) button.interactable = true;
                break;
            case InputHintState.PRESSED:
                if(!isButton) image.sprite = keyPressedSprite;
                tmpro.color = Color.grey;
                if (isButton) button.interactable = false;
                break;
            default:
                break;
        }
    }
}
