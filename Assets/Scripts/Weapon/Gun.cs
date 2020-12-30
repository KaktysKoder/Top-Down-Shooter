using UnityEngine;

[AddComponentMenu(menuName: "Character/GunControllers/Gun")]
public class Gun : MonoBehaviour
{
    [Header("Set value in Inspector for test's.")]
    [SerializeField] private float offset = 1;

    [Header("Please set ref's.")]
    [SerializeField] private GameObject bullet;     // Пуля.    
    [SerializeField] private Transform shotPoint;   // Точка выстрела.

    private float timeBtwShot;  // Время между выстрелами   
    public float startTimeBtwShot;

    private void Update()
    {
        // Слежка за курсором мыши.
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // Вражение оружия.
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShot <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);

                timeBtwShot = startTimeBtwShot; // после выстрела перезарядка.
            }
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }
}