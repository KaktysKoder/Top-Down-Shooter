using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject gun;
    public GameObject sword;

   // public GameObject[] weapons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // TODO: Позже сменить код  .
            if (gun.activeInHierarchy == true)
            {
                gun.SetActive(false);
                sword.SetActive(true);
            }
            else if (sword.activeInHierarchy == true)
            {
                sword.SetActive(false);
                gun.SetActive(true);
            }

           
            //for (int i = 0; i < weapons.Length; i++)
            //{
            //    if (weapons[i].activeInHierarchy == true)
            //    {
            //        weapons[i].SetActive(false);

            //        if (i != 0)
            //        {
            //            weapons[i - 1].SetActive(true);
            //        }
            //        else
            //        {
            //            weapons[weapons.Length - 1].SetActive(true);
            //        }
            //        break;
            //    }
            //}
        }
    }
}