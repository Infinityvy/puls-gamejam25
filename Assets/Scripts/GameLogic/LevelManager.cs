using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    private static Dictionary<int, Level> levels;

    public static Level activeLevel;

    public static bool initiated { get; private set; } = false;

    public static void Init()
    {
        if (initiated) return;

        initiated = true;

        levels = new Dictionary<int, Level>();

        Level[] loadedLevels = Resources.LoadAll<Level>("LevelAssets");

        foreach(Level l in loadedLevels)
        {
            levels.Add(l.getID(), l);
        }
    }

    public static void LoadLevel(int id)
    {
        Level newLevel = levels[id];

        activeLevel = newLevel;

        SceneManager.LoadScene(newLevel.GetSceneName());
    }

    public static int GetLevelCount()
    {
        return levels.Count;
    }
}
