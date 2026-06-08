using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject levelCompleteMenu;
    public GameObject pauseBtnInGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah yang menyentuh kotak putih adalah Player
        if (collision.CompareTag("Player"))
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        levelCompleteMenu.SetActive(true); // Munculkan panel menang
        pauseBtnInGame.SetActive(false);   // Hilangkan tombol pause agar rapi
        Time.timeScale = 0f;               // Hentikan permainan
    }

}