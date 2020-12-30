using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;          // перезарядка, которая позволит изменять скорость атаки.
    public float startTimeBtwAttack;

    public Transform attackPos;          // Позиция атаки.
    public LayerMask enemy;              // Маска, слой которой будет получать урон.

    public float attackRange;            // Радиус атаки меча.
    public float damage;                 // Урон.        
    public Animator anim;                // Анимация атаки.

    private void Update()
    {
        if (timeBtwAttack <= 0) // Если перезарядка кончилась,
        {
            // И нажата левая кнопка мыши.
            if (Input.GetMouseButton(0))
            {
                anim.SetTrigger("SwordAttack");

                OnAttack();
            }
            timeBtwAttack = startTimeBtwAttack; // Делаем перезарядку равной стартовой.
        }
        else timeBtwAttack -= Time.deltaTime;
    }

    /// <summary>
    /// Атака - [Для вызова метода из анимации.]
    /// </summary>
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < enemies.Length; i++)
        {
            // Каждый враг который оказался в рвдиусе атки получает урон.
            enemies[i].GetComponent<Enemy>().TakeDanage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Отрисовки радиуса атаки для удобства отладки.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}