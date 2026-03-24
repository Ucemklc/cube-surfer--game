using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Baþlat butonuna baðlanacak
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Oyunun sahne adý
    }

    // Çýkýþ butonuna baðlanacak
    public void QuitGame()
    {
        Debug.Log("Oyun kapatýlýyor...");
        Application.Quit();
    }
}