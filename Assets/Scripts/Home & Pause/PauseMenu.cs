using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour
{
    [ SerializeField ] GameObject Pause_panel;
    public void Pause()
    {
        Pause_panel.SetActive(true);
    }
    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        Pause_panel.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




}
