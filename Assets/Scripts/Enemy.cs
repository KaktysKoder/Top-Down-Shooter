using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health; // Здоровье врага.
    public float speed;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


    public void TakeDanage(float damage)
    {
        health -= damage;
    }
} 