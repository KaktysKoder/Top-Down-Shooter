using UnityEngine;

/// <summary>
/// Спавнер комнат.
/// </summary>
public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    /// <summary>
    /// Направление спавна комнат.
    /// </summary>
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    private RoomVariant roomVariants;   // Варианты комнат.
    private int rand;                   // Вариант рандомной комнаты для спавна.
    private bool spawned = false;       // Заспавнилась ли комната.
    private float waitTime = 3.0f;      // Время ожидания перед уничтожением.

    private void Start()
    {
        roomVariants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariant>();

        Destroy(gameObject, waitTime);

        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        // если комнат еще не заспавнилась.
        if (direction == Direction.Top)
        {
            rand = Random.Range(0, roomVariants.topRooms.Length);

            Instantiate(roomVariants.topRooms[rand], transform.position, roomVariants.topRooms[rand].transform.rotation);
        }
        else if (direction == Direction.Bottom)
        {
            rand = Random.Range(0, roomVariants.bottomRooms.Length);

            Instantiate(roomVariants.bottomRooms[rand], transform.position, roomVariants.bottomRooms[rand].transform.rotation);
        }
        else if (direction == Direction.Left)
        {
            rand = Random.Range(0, roomVariants.leftRooms.Length);

            Instantiate(roomVariants.leftRooms[rand], transform.position, roomVariants.leftRooms[rand].transform.rotation);
        }
        else if (direction == Direction.Right)
        {
            rand = Random.Range(0, roomVariants.rightRooms.Length);

            Instantiate(roomVariants.rightRooms[rand], transform.position, roomVariants.rightRooms[rand].transform.rotation);
        }

        spawned = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ЕСли 1 SpawnPoint коснулся другого и у другого комната уже заспавнилась.
        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }
}