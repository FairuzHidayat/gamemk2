using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    public float moveDistance = 1.5f;  // seberapa jauh naik
    public float moveSpeed = 1.5f;    // kecepatan gerak
    public float waitTime = 1f;       // jeda di bawah sebelum naik lagi

    private Vector3 startPos;
    private Vector3 topPos;
    private bool movingUp = true;
    private bool waiting = false;

    void Start()
    {
        startPos = transform.position;
        topPos = startPos + new Vector3(0, moveDistance, 0);
    }

    void Update()
    {
        if (waiting) return;

        Vector3 target = movingUp ? topPos : startPos;
        transform.position = Vector3.MoveTowards(
            transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            movingUp = !movingUp;
            if (!movingUp)  // baru sampai bawah, jeda dulu
                StartCoroutine(Wait());
        }
    }

    System.Collections.IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }

    // Jika player menyentuh tanaman = mati / game over
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player kena tanaman!");
            // Panggil fungsi mati player di sini
        }
    }
}