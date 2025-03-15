using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    private Session session;

    private TextMeshProUGUI tmpro;

    private float timePassed = 0f;

    void Start()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        session = Session.Instance;
        session.resetEvent.AddListener(ResetTimer);
    }

    void Update()
    {
        if (session.levelEnded) return;

        timePassed += Time.deltaTime * session.gameSpeed;

        tmpro.text = System.Math.Round(timePassed, 2).ToString();
    }

    private void ResetTimer()
    {
        timePassed = 0;
        tmpro.text = "0.0";
    }
}
