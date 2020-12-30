using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;             // Скорость пули
    public float lifeTime;          // Время существования патрона.
    public float distance;          // Дистанция на которую летит патрон.
    public float damage;            // Повреждения которую наносит пуля.
    public LayerMask whatIsSolid;   // Что пуля будет считать твёрдым телом? 

    // public GameObject deathEffeck;   // Ссылка на эффект.

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                // Эффект после сметри.
                //Instantiate(deathEffeck, transform.position, Quaternion.identity);

                // Applay Damage !
                hitInfo.collider.GetComponent<Enemy>().TakeDanage(damage);
            }
            Destroy(gameObject);
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}