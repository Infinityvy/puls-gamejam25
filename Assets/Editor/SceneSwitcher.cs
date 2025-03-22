using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class SceneSwitcher : EditorWindow
{
    private int selectedSceneIndex = 0;
    private List<string> scenePaths = new List<string>();
    private List<string> sceneNames = new List<string>();

    [MenuItem("Tools/Scene Switcher")]
    public static void ShowWindow()
    {
        GetWindow<SceneSwitcher>("Scene Switcher");
    }

    private void OnEnable()
    {
        LoadScenes();
    }

    private void LoadScenes()
    {
        scenePaths.Clear();
        sceneNames.Clear();

        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenePaths.Add(scene.path);
                sceneNames.Add(System.IO.Path.GetFileNameWithoutExtension(scene.path));
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Select Scene", EditorStyles.boldLabel);

        selectedSceneIndex = EditorGUILayout.Popup(selectedSceneIndex, sceneNames.ToArray());

        if (GUILayout.Button("Switch Scene"))
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePaths[selectedSceneIndex]);
        }
    }
}
