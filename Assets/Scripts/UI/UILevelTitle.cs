using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UILevelTitle : MonoBehaviour
{
    private Level activeLevel;

    private TextMeshProUGUI tmpro;
    private Image background;

    private readonly float fadeOutDelay = 1f;
    private readonly float fadeOutDuration = 2f;
    private readonly float backgroundOpacity = 0.5f;
    private float timeSinceStart = 0;


    void Start()
    {
        activeLevel = LevelManager.activeLevel;

        tmpro = GetComponent<TextMeshProUGUI>();
        background = GetComponentInChildren<Image>();

        tmpro.text = activeLevel.getID().ToString() + " : " + activeLevel.GetTitle();
    }

    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (fadeOutDelay > timeSinceStart) return;

        if (fadeOutDuration + fadeOutDelay <= timeSinceStart)
        {
            Destroy(gameObject);
            return;
        }

        float fadeScale = 1 - ((timeSinceStart - fadeOutDelay) / fadeOutDuration);

        tmpro.color = new Color(tmpro.color.r, tmpro.color.g, tmpro.color.b, fadeScale);
        background.color = new Color(0, 0, 0, backgroundOpacity * fadeScale);
    }
}
