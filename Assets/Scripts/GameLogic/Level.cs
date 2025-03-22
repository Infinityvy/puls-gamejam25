using UnityEngine;

[CreateAssetMenu(menuName = "Create Level Asset", fileName = "Level", order = 0)]
public class Level : ScriptableObject
{
    [SerializeField]
    protected int id;

    [SerializeField]
    protected string title;

    public int getID()
    {
        return id;
    }

    public string GetSceneName()
    {
        return "Level" + id.ToString();
    }

    public string GetTitle()
    {
        return title;
    }
}
