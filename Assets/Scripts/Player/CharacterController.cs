using UnityEngine;

[AddComponentMenu(menuName: "Character/Controllers/CharacterController")]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [Header("Please set refs.")]
    public Joystick joystic;                                                                        // Ссылка на joystic.
    public ControlType controlType;                                                                 // Тип контроллера.
    [Space]
    [Header("Set in Inspector.")]
    [SerializeField] private float moveSpeed;                                                       // Скорость движения.

    private Rigidbody2D rb2D;                                                                       // Ссылка на компонент Rigidbody2D.
    private Animator anim;                                                                          // Ссылка на аниматор игрока.

    private Vector2 moveInput;                                                                      // Направление движение.
    private Vector2 moveVelosity;                                                                   // Итоговая скорость игрока.

    private bool isFacingRight = false;                                                             // Если игрок смотрит в право false.
    public float healt = 4;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();                                                         // Кэшируем ссылку на компонент твёрдое тело игрока.
        anim = GetComponent<Animator>();                                                            // Кэшируем ссылку на компонент аниматора игрока.

        if (controlType == ControlType.PC)                                                          // Отключение джёйстика если управление передали PC
        {
            joystic.gameObject.SetActive(false);                                                    // Отключаем jpystic
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
}