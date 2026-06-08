using UnityEngine;

public class musiconoff : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource bgm; // Variabel baru untuk menampung musik
    public void SoundOnBtn()
    {
        // Menyalakan musik
        if (bgm != null) bgm.UnPause();
    }

    public void SoundOffBtn()
    {
        // Berhentikan musik
        if (bgm != null) bgm.Pause();
    }
}
