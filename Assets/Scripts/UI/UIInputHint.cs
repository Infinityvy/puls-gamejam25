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

    public InputHintState state { get; private set; } = InputHintState.RELEASED;

    private void Start()
    {
        image = GetComponent<Image>();
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetState(InputHintState state)
    {
        this.state = state;

        switch(state)
        {
            case InputHintState.RELEASED:
                image.sprite = keyReleasedSprite;
                tmpro.color = Color.white;
                break;
            case InputHintState.PRESSED:
                image.sprite = keyPressedSprite;
                tmpro.color = Color.grey;
                break;
            default:
                break;
        }
    }
}
