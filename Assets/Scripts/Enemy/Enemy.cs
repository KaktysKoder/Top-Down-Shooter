using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float timeBtwAttack;          // перезарядка, которая позволит изменять скорость атаки.
    public float startTimeBtwAttack;
    public float damage;

    public float health; // Здоровье врага.
    public float speed;

    /*Времяч на которое остановится враг при получении урона!*/
    public float stopTime;
    public float startStopTime;
    public float narmalSpeed;
    //TODO: Сменить название CharacterController  на PlayerController
    public CharacterController player;
    private Animator anim;

    public GameObject deadEffect;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<CharacterController>();
        narmalSpeed = speed;
    }

    private void Update()
    {
        if (stopTime <= 0)
        {
            speed = narmalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // Направление движение врага.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                anim.SetTrigger("EnemySwordAttack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Атака врага
    /// </summary>
    public void OnEnemyAttack()
    {
        Instantiate(deadEffect, player.transform.position, Quaternion.identity);
        player.healt -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }

    public void TakeDanage(float damage)
    {
        stopTime = startStopTime;
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        health -= damage;
    }
}