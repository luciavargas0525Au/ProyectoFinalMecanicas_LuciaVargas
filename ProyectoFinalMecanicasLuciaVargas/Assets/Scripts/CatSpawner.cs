using UnityEngine;
using System.Collections;

public class CatSpawner : MonoBehaviour
{
    public GameObject catPrefab;
    public Transform spawnPoint;
    public Transform pathParent;
    public float spawnInterval = 2f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (!QueueManager.Instance.IsQueueFull())
                SpawnCat();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCat()
    {
        GameObject cat = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity);
        CatMovement movement = cat.GetComponent<CatMovement>();

        foreach (Transform wp in pathParent)
            movement.waypoints.Add(wp);
    }
}