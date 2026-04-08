using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [Header("Puntos de la cola (arrastra 3 Transforms en orden)")]
    public Transform[] queuePositions; // [0] = mostrador, [1] = detrás, [2] = al fondo

    private List<CatMover> queue = new List<CatMover>();

    public int QueueCount() => queue.Count;

    public bool JoinQueue(CatMover cat)
    {
        if (queue.Count >= queuePositions.Length)
            return false; // Cola llena, el gato no entra

        queue.Add(cat);
        AssignPositions();

        // Si es el primero, ya está en el punto de pedido
        if (queue.Count == 1)
            cat.OnReachedOrderPoint();

        return true;
    }

    public void LeaveQueue(CatMover cat)
    {
        queue.Remove(cat);
        AssignPositions();

        // El nuevo primero de la cola recibe su comanda
        if (queue.Count > 0)
            queue[0].OnReachedOrderPoint();
    }

    void AssignPositions()
    {
        for (int i = 0; i < queue.Count; i++)
        {
            queue[i].SetQueuePosition(queuePositions[i].position);
        }
    }
}
