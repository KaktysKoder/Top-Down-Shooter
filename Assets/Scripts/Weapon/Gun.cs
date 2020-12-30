using UnityEngine;

[AddComponentMenu(menuName: "Character/GunControllers/Gun")]
public class Gun : MonoBehaviour
{
    [Header("Set value in Inspector for test's.")]
    [SerializeField] private float offset = 1;

    public enum GunType
    {
        Default,
        Enemy
    }

    public GunType gunType;


    [Header("Please set ref's.")]
    [SerializeField] private GameObject bullet;     // Пуля.    
    [SerializeField] private Transform shotPoint;   // Точка выстрела.
    public Joystick joystick;


    private float timeBtwShot;  // Время между выстрелами   
    public float startTimeBtwShot;

    private CharacterController player;
    private float rotZ; //вращение пушки
    private Vector3 difference;

    private void Start()
    {

        // TODO: Add tag "player" in Tag helper class.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        // Если режим PC отключаем joystic
        if (player.controlType == ControlType.PC && gunType == GunType.Default)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (gunType == GunType.Default)
        {
            // Вращение если включен PC mode
            if (player.controlType == ControlType.PC)
            {
                // Слежка за курсором мыши (заставляет оружие крутиться за крсором).
                difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }
            else if (player.controlType == ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
            {
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            }
        }
        else if (gunType == GunType.Enemy)
        {
            // Слежка за курсором мыши (заставляет оружие крутиться за крсором).
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        // Вражение оружия.
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShot <= 0)
        {
            // Выстрел по правому джестику только если режим управление PC
            if (Input.GetMouseButton(0) && player.controlType == ControlType.PC || gunType == GunType.Enemy)
            {
                Shoot();
            }
            else if (Input.GetMouseButton(0) && player.controlType == ControlType.Android)
            {
                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    Shoot();
                }
            }
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }

    }

    /// <summary>
    /// Выстрел пули.
    /// </summary>
    public void Shoot()
    {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);

        timeBtwShot = startTimeBtwShot; // после выстрела перезарядка.
    }
}