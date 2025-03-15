using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Bullet : MonoBehaviour
{
    private Session session;

    private float bulletSpeed = 10f;

    private Vector3 startPos;

    [SerializeField]
    private Transform trailTrans;

    [SerializeField]
    private Transform destroyEffect;

    void Start()
    {
        session = Session.Instance;

        startPos = transform.position;

        session.resetEvent.AddListener(DestroyBullet);

        session.IncreaseBulletCount();
    }

    void Update()
    { 
        transform.position += transform.up * bulletSpeed * Time.deltaTime * session.gameSpeed;

        trailTrans.localPosition = new Vector3(Mathf.Sin(Vector3.Distance(startPos, transform.position)) * 0.07f, -0.3f, 0);
    }


    public void DestroyBullet()
    {
        session.resetEvent.RemoveListener(DestroyBullet);
        session.DecreseBulletCount();
        Destroy(gameObject);
    }
}
