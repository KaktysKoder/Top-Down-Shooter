using UnityEngine;

/// <summary>
/// Варианты комнат.
/// </summary>
public class RoomVariant : MonoBehaviour
{
    [Header("Комнаты с направлением ввверх.")]
    public GameObject[] topRooms;

    [Header("Комнаты с направлением влево.")]
    public GameObject[] leftRooms;

    [Header("Комнаты с направлением вправо.")]
    public GameObject[] rightRooms;

    [Header("Комнаты с направлением вниз.")]
    public GameObject[] bottomRooms;


}