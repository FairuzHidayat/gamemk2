using UnityEngine;
using TMPro; // hapus ini kalau tidak pakai TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coinCount = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        coinCount++;
        Debug.Log("Total Koin: " + coinCount);
    }
}