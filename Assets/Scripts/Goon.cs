using UnityEngine;

public class Goon : MonoBehaviour
{
    [SerializeField]
    private Transform bulletPrefab;

    [SerializeField]
    private Transform bulletInstPos;

    void Start()
    {
        Session.Instance.resetEvent.AddListener(ResetGoon);

        Shoot();
    }

    private void ResetGoon()
    {
        Shoot();
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletInstPos.position, transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(bulletInstPos.position, bulletInstPos.position + transform.up * 100);
    }
}
