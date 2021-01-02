using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[AddComponentMenu(menuName: "Character/Controllers/CharacterController")]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [Header("Controls =======================================")]
    public Joystick joystic;                                                                        // Ссылка на joystic.
    public ControlType controlType;                                                                 // Тип контроллера.
    public float moveSpeed;                                                                         // Скорость движения.

    [Header("Health =======================================")]
    public float healt = 4;
    public GameObject potionEffect;                                                                 // Эффект при подборе зелья.
    public Text healthText;

    [Header("Shield =======================================")]
    public GameObject shield;                                                                       // Эффект при подборе щита. (Включает защиту игрока.)
    public GameObject shildEffect;                                                                  // Эффект при подборе щита.
    public Shield shieldTimer;                                                                           // Ссылка на таймер щита.

    [Header("Weapons =======================================")]
    public List<GameObject> unlockedWeapons;                                                         // Разблокированные пушки
    public GameObject[] allWeapons;                                                                  // Массив из всех видов оружия.
    public Image imageWeapon;                                                                        // Активная на UI Иконка оружия.

    private Rigidbody2D rb2D;                                                                       // Ссылка на компонент Rigidbody2D.
    private Animator anim;                                                                          // Ссылка на аниматор игрока.
    private Vector2 moveInput;                                                                      // Направление движение.
    private Vector2 moveVelosity;                                                                   // Итоговая скорость игрока.

    private bool isFacingRight = false;                                                             // Если игрок смотрит в право false.

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();                                                         // Кэшируем ссылку на компонент твёрдое тело игрока.
        anim = GetComponent<Animator>();                                                            // Кэшируем ссылку на компонент аниматора игрока.

        if (controlType == ControlType.PC)                                                          // Отключение джёйстика если управление передали PC
        {
            joystic.gameObject.SetActive(false);                                                    // Отключаем jpystic
        }

        shield.SetActive(false);
    }

    public void ChangeHealth(int healthValue)
    {
        // Получаем урон только если щит не активен или же если щит активен но мы получаем не урон а хилимся.
        // [shield.activeInHierarchy && healthValue > 0] подбор бонусов
        if (!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0)
        {
            healt += healthValue;               // Отнимаем здоровье
            healthText.text = $"HP: {healt}";   // Отображаем Current health in Display.
        }
        else if (shield.activeInHierarchy && healthValue < 0)
        {
            shieldTimer.ReduceTimer(healthValue);
        }

    }

    private void Update()
    {
        if (controlType == ControlType.PC)                                                          // Если игрок выбрал контроллер для PC
        {
            moveInput = new Vector2(Input.GetAxis(Axis.Horizontal), Input.GetAxis(Axis.Vertical));  // Считываем горизонтальное и вертикальное движение.
        }

        if (controlType == ControlType.Android)                                                     // Если игрок выбрал контроллер для PC
        {
            moveInput = new Vector2(joystic.Horizontal, joystic.Vertical);                          // Считываем горизонтальное и вертикальное движение джестика.
        }

        moveVelosity = moveInput.normalized * moveSpeed;                                            // В moveVelosity присваиваем конечную скорость.

        if (moveInput.x == 0)                                                                       // Если мы стоим на месте.
        {
            anim.SetBool("IsRunning", true);                                                        // Отключаем анимацию бега.
        }
        else
        {
            anim.SetBool("IsRunning", false);                                                       // Включаем анимацию бега.
        }

        if (!isFacingRight && moveInput.x < 0)                                                       // Если мы смотрим в лево то идём в право.
        {
            Flip();
        }
        else if (isFacingRight && moveInput.x > 0)                                                   // Если мы смотрим в право то идём в лево.
        {
            Flip();
        }

        // Перезагрузка сцены после смерти.
        if (healt <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Переключение оружия на Q 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + moveVelosity * Time.fixedDeltaTime);                      // Двигает игрока.
    }

    /// <summary>
    /// Разварот игрока (false - right, true - left).
    /// </summary>
    private void Flip()
    {
        isFacingRight = !isFacingRight;         // Инверсия

        Vector3 scale = transform.localScale;   // Сохраняем в scale локальный скейл this объекта.
        scale.x *= -1;                          // scale.x =  scale.x * -1; Умножая x на -1 происходить разварот спрайта.

        transform.localScale = scale;           // В локальный скейл this объекта присваиваем новый (вычисленный скейл).
    }

    public void SwitchWeapon()
    {
        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            // Если разблокированная пушка активна то мы её выключаем.
            if (unlockedWeapons[i].activeInHierarchy)
            {
                unlockedWeapons[i].SetActive(false);

                if (i != 0) // Если эта пушка не нулевая в списке.
                {
                    // То мы включаем пушку которая идёт на 1 раньше.
                    unlockedWeapons[i - 1].SetActive(true);
                    imageWeapon.sprite = unlockedWeapons[i - 1].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    // Активация последней пушки
                    unlockedWeapons[unlockedWeapons.Count - 1].SetActive(true);
                    imageWeapon.sprite = unlockedWeapons[unlockedWeapons.Count - 1].GetComponent<SpriteRenderer>().sprite;
                }

                imageWeapon.SetNativeSize();
                break;
            }
        }
    }

    // Подбор зелий.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Potion"))
        {
            // TODO: убрать магические числа.
            ChangeHealth(5);
            Instantiate(potionEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            // Стаки щитов
            if (!shield.activeInHierarchy)
            {
                Instantiate(shildEffect, other.transform.position, Quaternion.identity);

                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true); // активируем объект таймера
                shieldTimer.isCooldown = true;

                Destroy(other.gameObject);
            }
            else
            {
                shieldTimer.ResrtTimet(); // Reset time
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Weapon"))
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                // Если имя пушки которую мы подобрали == совподает с именем пушки из массива то
                if (other.name == allWeapons[i].name)
                {
                    // Разблокируем. Можем теперь на неё переключаться.
                    unlockedWeapons.Add(allWeapons[i]);
                }
            }
            // Новую пушку взяли и сразу же на неё переключились.
            SwitchWeapon();
            Destroy(other.gameObject);


        }
    }
}