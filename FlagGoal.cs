using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagGoal : MonoBehaviour
{
    public GameObject levelCompleteMenu;
    public GameObject pauseBtnInGame;
    public AudioClip goalSound;
    public float delayBeforeEnd = 3f; // Jeda waktu (3 detik) sebelum pindah level
    public Animator flagAnimator;
    public string nextSceneName = "";
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Validasi agar tidak sengaja terpicu dua kali
        if (triggered) return;
        if (!other.CompareTag("Player")) return;
        triggered = true;

        // 2. Munculkan UI Complete Menu & Sembunyikan Tombol Pause
        if (levelCompleteMenu != null)
            levelCompleteMenu.SetActive(true);

        if (pauseBtnInGame != null)
            pauseBtnInGame.SetActive(false);

        // 3. Matikan kontrol player agar tidak bisa digerakkan lagi saat menang
        PlayerMovement2 player2 = other.GetComponent<PlayerMovement2>();
        if (player2 != null) player2.enabled = false;

        PlayerMovement player1 = other.GetComponent<PlayerMovement>();
        if (player1 != null) player1.enabled = false;

        // 4. Mainkan animasi bendera turun
        if (flagAnimator != null)
            flagAnimator.SetTrigger("FlagDown");

        // 5. Efek suara menang
        if (goalSound != null)
            AudioSource.PlayClipAtPoint(goalSound, transform.position);

        Debug.Log("Level selesai! Menu complete muncul.");

        // 6. Pindah ke scene berikutnya otomatis setelah jeda (delayBeforeEnd) detik
        Invoke("LoadNextScene", delayBeforeEnd);
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Waduh, isi dulu 'Next Scene Name' di Inspector bendera!");
        }
    }
}