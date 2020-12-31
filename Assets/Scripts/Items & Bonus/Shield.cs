using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float cooldown;                                          // Время длительности таймера.
    [HideInInspector] public bool isCooldown;                       // Включен ли таймер.

    private Image shieldImage;
    private CharacterController player;

    private void Start()
    {
        shieldImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        isCooldown = true;
    }

    private void Update()
    {
        if (isCooldown)                                              // Если таймер включен
        {
            shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime; // Уменьшение таймера щита, и отображение.

            if (shieldImage.fillAmount <= 0)                         // Если время щита закончилось
            {
                shieldImage.fillAmount = 1;                          // Вернули спрайту исходное сосотояние.
                isCooldown = false;                                  // Отключили таймер.
                player.shield.SetActive(false);                      // Отключили щит у игрока.
                gameObject.SetActive(false);                         // Отключение картинки таймера.
            }
        }
    }

    /// <summary>
    /// Перезапуск таймера.
    /// </summary>
    public void ResrtTimet()
    {
        shieldImage.fillAmount = 1;
    }

    /// <summary>
    /// Уменьшение таймера щита в зависимости от урона врага.
    /// </summary>
    /// <param name="damage">Получаемое повреждение.</param>
    public void ReduceTimer(int damage)
    {
        // При получении урона время щита снижается.
        shieldImage.fillAmount += damage / 5.0f; // 1f - снос с 1 пули
    }
}