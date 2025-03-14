using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderController : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;

    protected string valuePrefix = "x";

    public void SetValueText()
    {
        valueText.text = valuePrefix + System.Math.Round(slider.value, 2).ToString();
    }

    public abstract void OnValueChanged();
}
