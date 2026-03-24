using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject panel; // Game Over paneli

    // Game Over ekranýný göster
    public void ShowGameOver()
    {
        panel.SetActive(true);
        Time.timeScale = 0f; // durdur
    }

    // Oyunu tekrar baþlat
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Ana menüye git
    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("main menü"); // senin sahne adýn
    }
}