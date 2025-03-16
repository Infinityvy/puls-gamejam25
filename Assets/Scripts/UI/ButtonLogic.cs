using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Resume()
    {
        Session.Instance.SetPaused(false);
    }

    public void TogglePause()
    {
        Session.Instance.SetPaused(!Session.Instance.isPaused);
    }
}
