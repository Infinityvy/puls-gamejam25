using UnityEngine;

public class Human : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.InstDestroyEffect();
            bullet.DestroyBullet();
        }

            KillHuman();
    }

    public void KillHuman()
    {
        Session.Instance.FailLevel();
    }
}
