using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPointScript : MonoBehaviour
{
    public int requiredCoins = 10; // Số coin yêu cầu để về đích
    public GameObject reminderPanel; // Tham chiếu đến Panel nhắc nhở

    private void Start()
    {
        // Đảm bảo rằng Panel nhắc nhở được tắt khi bắt đầu
        reminderPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int currentCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentCoins >= requiredCoins)
            {
                // Xử lý khi người chơi đủ số coin và về đích
                Debug.Log("Về đích!");

                // Xóa key "NumberOfCoins" trong PlayerPrefs
                PlayerPrefs.DeleteKey("NumberOfCoins");

                // Chuyển đến màn tiếp theo
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                // Xử lý khi người chơi chưa đủ số coin
                Debug.Log("Bạn cần đủ số coin để về đích!");

                // Hiển thị Panel nhắc nhở
                reminderPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Ẩn Panel nhắc nhở nếu người chơi đã rời khỏi vùng về đích
            reminderPanel.SetActive(false);
        }
    }

  
}
