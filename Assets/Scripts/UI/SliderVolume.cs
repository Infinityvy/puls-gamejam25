using AudioManagement;
using UnityEngine;

namespace UI
{
    public class SliderVolume : SliderController
    {
        [SerializeField]
        private AudioGroup group;

        private void Start()
        {
            valueSuffix = "%";
            valuePrecision = 0;

            slider.SetValueWithoutNotify(AudioManager.Instance.GetGroupVolume(group));

            SetValueText();
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
            AudioManager.Instance.SetGroupVolume(group, volume);
        }
    }
}
