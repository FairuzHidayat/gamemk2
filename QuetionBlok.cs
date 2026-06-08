using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public GameObject coinPrefab;   // drag prefab koin ke sini
    public Sprite usedBlockSprite;  // sprite kotak setelah dipakai
    public AudioClip hitSound;

    private bool isUsed = false;
    private SpriteRenderer sr;
    private AudioSource audioSrc;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Hanya bereaksi jika belum dipakai dan dipukul dari bawah
        if (isUsed) return;

        bool hitFromBelow = col.gameObject.CompareTag("Player") &&
            col.contacts[0].normal.y < -0.5f;

        if (hitFromBelow)
        {
            isUsed = true;

            // Ganti sprite jadi kotak kosong
            if (usedBlockSprite != null)
                sr.sprite = usedBlockSprite;

            // Spawn koin di atas kotak
            if (coinPrefab != null)
            {
                Vector3 spawnPos = transform.position + new Vector3(0, 1f, 0);
                GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
                // Beri animasi pop ke atas
                Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();
                if (coinRb != null)
                    coinRb.linearVelocity = new Vector2(0, 8f);
            }

            // Efek suara
            if (hitSound != null)
                audioSrc.PlayOneShot(hitSound);

            // Animasi bounce kotak
            StartCoroutine(BounceBlock());
        }
    }

    System.Collections.IEnumerator BounceBlock()
    {
        Vector3 original = transform.localPosition;
        transform.localPosition += new Vector3(0, 0.15f, 0);
        yield return new WaitForSeconds(0.1f);
        transform.localPosition = original;
    }
}