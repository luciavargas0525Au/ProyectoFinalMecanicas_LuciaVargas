using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [Header("Configuración de spawn")]
    public GameObject catPrefab;
    public float spawnInterval = 4f;
    public int maxCatsInScene = 3;

    [Header("Referencias")]
    public QueueManager queueManager;
    public WaypointPath waypointPath;

    private List<GameObject> activeCats = new List<GameObject>();

    IEnumerator Start()
    {
        yield return null;

        if (queueManager == null)
            queueManager = FindObjectOfType<QueueManager>();
        if (waypointPath == null)
            waypointPath = FindObjectOfType<WaypointPath>();

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            activeCats.RemoveAll(c => c == null);

            if (activeCats.Count < maxCatsInScene)
                SpawnCat();
        }
    }

    void SpawnCat()
    {
        GameObject cat = Instantiate(catPrefab, transform.position, transform.rotation);
        activeCats.Add(cat);

        CatMover mover = cat.GetComponent<CatMover>();
        if (mover != null)
            mover.Initialize(queueManager, waypointPath);
    }
}