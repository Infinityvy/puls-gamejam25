using UnityEngine;

public class Human : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KillHuman();
    }

    public void KillHuman()
    {
        Session.Instance.FailLevel();
    }
}
