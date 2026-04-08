using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public float speed = 2f;
    public float rotationSpeed = 5f;

    [HideInInspector]
    public bool inQueue = false;

    private int currentWaypoint = 0;

    void Update()
    {
        if (inQueue || waypoints.Count == 0) return;

        Transform target = waypoints[currentWaypoint];

        // Movimiento
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotación
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            transform.rotation = lookRotation;
        }

        // Llegada al waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
            {
                // Llegó al final, pasa a la cola
                inQueue = true;
                transform.position = target.position; // asegúrate que quede exacto
                QueueManager.Instance.AddToQueue(this);
            }
        }
    }
}