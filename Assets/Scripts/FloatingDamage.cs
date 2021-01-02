using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    [HideInInspector] public float damage; // Урон который будет высвечиваться.
    private TextMesh textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = $"-{damage}";
    }

    public void OnAnimationOver()
    {
        Destroy(gameObject);
    }
}