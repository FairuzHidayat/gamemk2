using UnityEngine;
using UnityEngine.SceneManagement; // Pastikan baris ini ada di paling atas

public class PauseManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseButton;
    public GameObject pauseMenu;

    [Header("Audio")]
    public AudioSource bgm; // Variabel baru untuk menampung musik

    void Start()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        // Berhentikan musik
        if (bgm != null) bgm.Pause();
    }

    public void ResumeGame()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        // Jalankan musik lagi
        if (bgm != null) bgm.UnPause();
    }

    // Fungsi untuk kembali ke Menu Utama
    public void GoToHome()
    {
        Time.timeScale = 1f; // PENTING: Kembalikan waktu ke normal sebelum pindah scene
        SceneManager.LoadScene("Menu");
    }

    // Fungsi untuk mengulang Level (Restart)
    public void RestartLevel()
    {
        Time.timeScale = 1f; // PENTING: Kembalikan waktu ke normal agar game tidak macet saat restart

        // Mengambil nama scene yang sedang aktif saat ini agar otomatis
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}