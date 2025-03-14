using UnityEngine;

public class SliderGameSpeed : SliderController
{
    private Session session;

    void Start()
    {
        valuePrefix = "Game Speed: x";

        session = Session.Instance;
        //slider.value = session.gameSpeed;
        slider.value = 0f;

        SetValueText();
        OnValueChanged();
    }

    public override void OnValueChanged()
    {
        session.gameSpeed = slider.value;
    }
}
