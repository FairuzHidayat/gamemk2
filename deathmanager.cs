using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverMenu; // Masukkan Panel Failed di sini
    public GameObject pauseBtnInGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah yang menyentuh adalah Player
        if (collision.CompareTag("Player"))
        {
            GameOver(collision.gameObject);
        }
    }

    void GameOver(GameObject player)
    {
        gameOverMenu.SetActive(true);      // Munculkan panel gagal
        pauseBtnInGame.SetActive(false);    // Sembunyikan tombol pause

        player.SetActive(true);            // Karakter menghilang
        // ATAU jika ingin animasi dulu, bisa pakai: 
        // player.GetComponent<SpriteRenderer>().enabled = false;

        Time.timeScale = 0f;                // Hentikan permainan
    }

    // Fungsi untuk tombol Restart di menu Failed
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}