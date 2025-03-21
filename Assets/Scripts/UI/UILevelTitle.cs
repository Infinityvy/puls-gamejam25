using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UILevelTitle : MonoBehaviour
{
    private Session session;

    private TextMeshProUGUI tmpro;
    [SerializeField]
    private Canvas canvas;

    private readonly float fadeOutDuration = 3f;
    private float timeSinceStart = 0;
    private Vector3 startPos;

    private readonly float horizontalOffset = -40;


    void Start()
    {
        session = Session.Instance;

        tmpro = GetComponent<TextMeshProUGUI>();

        tmpro.text = (session.levelID + 1).ToString() + " : " + session.levelTitle;

        startPos = transform.position;
    }

    void Update()
    {
        if (fadeOutDuration - timeSinceStart <= 0)
        {
            Destroy(gameObject);
            return;
        }

        timeSinceStart += Time.deltaTime;

        float fadeScale = 1 - (timeSinceStart / fadeOutDuration);

        tmpro.color = new Color(tmpro.color.r, tmpro.color.g, tmpro.color.b, fadeScale);
        transform.position = startPos + Vector3.right * horizontalOffset * fadeScale * canvas.scaleFactor;
    }
}
