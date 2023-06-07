using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickObject : MonoBehaviour
{
    public float kickForce = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu nhân vật va chạm với bàn đạp
        if (collision.CompareTag("Player"))
        {
            // Lấy tham chiếu tới Rigidbody của nhân vật
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

            // Kiểm tra xem có Rigidbody không và có thể bật lên không
            if (playerRigidbody != null && playerRigidbody.velocity.y <= 0f)
            {
                // Áp dụng lực để bật lên
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, kickForce);
            }
        }
    }
}
