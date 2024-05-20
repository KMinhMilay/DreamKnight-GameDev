using UnityEngine;
using UnityEngine.SceneManagement;
public class ChucNangMenu : MonoBehaviour
{
    public void ChoiMoi()
    {
        SceneManager.LoadScene(1);
    }


    public void ThoatRaMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Thongtinnhom()
    {
        
    }

    public void Thoat()
    {
        Application.Quit();
    }

}
