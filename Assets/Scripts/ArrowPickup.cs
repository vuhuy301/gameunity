using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra nếu người chơi chạm vào
        {
            Player player = collision.GetComponent<Player>(); // Lấy reference đến script Player của người chơi
            if (player != null)
            {
                player.IncreaseArrowCount(3); // Gọi phương thức trong script Player để tăng số lượng mũi tên
            }

            // Sau khi chạm vào, hủy GameObject của mũi tên
            Destroy(gameObject);
        }
    }
}
