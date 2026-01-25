using System;
using System.Collections.Generic;
using GameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadLevelButtons : MonoBehaviour
    {
        [SerializeField] private Button buttonPrefab;

        private const float ButtonHeight = 100;

        private void Start()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, LevelManager.GetLevelCount() * ButtonHeight);
            
            Dictionary<int, Level> levels = LevelManager.levels;

            for (int i = 1; i < levels.Count + 1; i++)
            {
                Button button = Instantiate(buttonPrefab, transform);
                Level level = levels[i];
                
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + level.getID() + ": " + level.GetTitle();
                
                button.onClick.AddListener(() =>
                {
                    button.GetComponent<ButtonLogic>().LoadLevel(level.getID());
                });
            }
        }
    }
}
