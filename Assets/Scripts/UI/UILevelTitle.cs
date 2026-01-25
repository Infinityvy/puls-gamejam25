using GameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UILevelTitle : MonoBehaviour
    {
        private Level activeLevel;

        private TextMeshProUGUI tmpro;
        private Image background;

        private const float FadeOutDelay = 1f;
        private const float FadeOutDuration = 2f;
        private const float BackgroundOpacity = 0.5f;
        private float timeSinceStart = 0;


        private void Start()
        {
            activeLevel = LevelManager.activeLevel;

            tmpro = GetComponent<TextMeshProUGUI>();
            background = GetComponentInChildren<Image>();

            tmpro.text = activeLevel.getID().ToString() + " : " + activeLevel.GetTitle();
        }

        private void Update()
        {
            timeSinceStart += Time.deltaTime;

            if (FadeOutDelay > timeSinceStart) return;

            if (FadeOutDuration + FadeOutDelay <= timeSinceStart)
            {
                Destroy(gameObject);
                return;
            }

            float fadeScale = 1 - ((timeSinceStart - FadeOutDelay) / FadeOutDuration);

            tmpro.color = new Color(tmpro.color.r, tmpro.color.g, tmpro.color.b, fadeScale);
            background.color = new Color(0, 0, 0, BackgroundOpacity * fadeScale);
        }
    }
}
