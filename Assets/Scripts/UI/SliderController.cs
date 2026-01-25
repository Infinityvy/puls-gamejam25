using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public abstract class SliderController : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI valueText;

        protected string valuePrefix = "";
        protected string valueSuffix = "";

        protected int valuePrecision = 2;

        public void SetValueText()
        {
            valueText.text = valuePrefix + System.Math.Round(slider.value, valuePrecision).ToString() + valueSuffix;
        }

        public abstract void OnValueChanged();

        protected abstract void ResetSlider();
    }
}
