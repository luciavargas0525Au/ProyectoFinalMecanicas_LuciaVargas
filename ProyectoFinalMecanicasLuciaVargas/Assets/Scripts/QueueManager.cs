using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public static QueueManager Instance;
    public List<Transform> queuePoints;
    public int maxQueueSize = 5;

    private List<CatMovement> queue = new List<CatMovement>();

    private void Awake()
    {
        Instance = this;
    }

    public bool IsQueueFull()
    {
        return queue.Count >= maxQueueSize;
    }


    void UpdateQueuePositions()
    {
        for (int i = 0; i < queue.Count; i++)
        {
            queue[i].inQueue = true; // asegurar
            queue[i].transform.position = queuePoints[i].position;
            queue[i].transform.rotation = queuePoints[i].rotation;
        }
    }

    void UpdateOrders()
    {
        for (int i = 0; i < queue.Count; i++)
        {
            CatOrder order = queue[i].GetComponent<CatOrder>();
            order.ShowOrder(i == 0); // solo el primero
        }
    }

    public void ServeFirstCat()
    {
        if (queue.Count == 0) return;
        CatMovement first = queue[0];
        queue.RemoveAt(0);
        Destroy(first.gameObject);

        UpdateQueuePositions();
        UpdateOrders();
    }

    public void AddToQueue(CatMovement cat)
    {
        if (IsQueueFull())
        {
            Destroy(cat.gameObject); // opcional: efecto visual
            return;
        }

        // Mover inmediatamente al gato a la "espera" en el final de la ruta
        Vector3 waitPos = cat.waypoints[cat.waypoints.Count - 1].position;
        waitPos.y = 0.87f;
        cat.transform.position = waitPos;

        queue.Add(cat);
        UpdateQueuePositions();
        UpdateOrders();
    }
}