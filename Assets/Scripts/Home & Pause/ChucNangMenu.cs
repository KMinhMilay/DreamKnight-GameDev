using UnityEngine;
using UnityEngine.SceneManagement;

public class ChucNangMenu : MonoBehaviour
{
    public GameObject PausePanel; // Tham chiếu đến Pause_panel
    public static bool IsGamePaused = false; // Biến trạng thái tạm dừng

    void Start()
    {
        // Đảm bảo Pause_panel được tắt khi trò chơi bắt đầu
        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
        }
    }

    public void ChoiMoi()
    {
        SceneManager.LoadScene("level 1");
    }

    public void ThoatRaMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void Thongtinnhom()
    {
        
    }

    public void help()
    {

    }

    public void Thoat()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (PausePanel != null)
        {
            PausePanel.SetActive(true);
        }
        Time.timeScale = 0f; // Tạm dừng thời gian trong trò chơi
        IsGamePaused = true; // Đặt biến tạm dừng thành true
    }

    public void ResumeGame()
    {
        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
        }
        Time.timeScale = 1f; // Tiếp tục thời gian trong trò chơi
        IsGamePaused = false; // Đặt biến tạm dừng thành false
    }
}
