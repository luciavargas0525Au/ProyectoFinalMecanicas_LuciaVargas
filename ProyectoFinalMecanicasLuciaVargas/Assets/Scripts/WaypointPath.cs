using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [Header("Arrastra aquí los GameObjects de waypoints en orden")]
    public Transform[] waypoints;

    // Dibuja la ruta en el editor para visualizarla fácilmente
    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                Gizmos.DrawSphere(waypoints[i].position, 0.12f);
            }
        }

        // Último waypoint (punto de pedido) en verde
        if (waypoints[waypoints.Length - 1] != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(waypoints[waypoints.Length - 1].position, 0.18f);
        }
    }
}
