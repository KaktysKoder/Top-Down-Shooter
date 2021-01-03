using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject block;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            // 2 спавна так как стена из 2х блоков
            Instantiate(block, transform.GetChild(0).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(1).position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}