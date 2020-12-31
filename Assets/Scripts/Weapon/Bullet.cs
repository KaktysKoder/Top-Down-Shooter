using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;             // Скорость пули
    public float lifeTime;          // Время существования патрона.
    public float distance;          // Дистанция на которую летит патрон.
    public int damage;            // Повреждения которую наносит пуля.
    public LayerMask whatIsSolid;   // Что пуля будет считать твёрдым телом? 

    public GameObject deathEffeck;   // Ссылка на эффект.

    [SerializeField] private bool enemyBullet; // патроны которыми стреляют враги

    private void Start()
    {
        Invoke("DestroyBullet()", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            // Если игрок попал по врагу
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDanage(damage);
            }

            // Если враг попал по игроку
            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitInfo.collider.GetComponent<CharacterController>().ChangeHealth(-damage);
            }
            DestroyBullet();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void DestroyBullet()
    {
        Instantiate(deathEffeck, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}