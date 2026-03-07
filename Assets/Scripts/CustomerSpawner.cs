using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;   // Spawn edilecek müşteri
    public Transform spawnPoint;         // Kapı noktası

    public float spawnInterval = 5f;     // Kaç saniyede bir
    float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }
    void SpawnCustomer()
    {
        Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
