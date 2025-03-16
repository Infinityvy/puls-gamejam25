using UnityEngine;

public class Human : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.Play();

        Session.Instance.FailLevel();
    }
}
