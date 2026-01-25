using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class UIWindow : UIDraggable
    {
        private RectTransform rectTransform;
        private TextMeshProUGUI windowTitle;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            windowTitle = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetSize(int width, int height)
        {
            rectTransform.sizeDelta = new Vector2(width, height);
        }

        public void SetWindowTitle(string title)
        {
            windowTitle.text = title;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
