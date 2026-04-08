using System.Collections;
using UnityEngine;

public class CatMover : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float arrivalThreshold = 0.15f;

    private WaypointPath waypointPath;
    private QueueManager queueManager;
    private Coroutine queueMoveCoroutine;

    public void Initialize(QueueManager manager, WaypointPath path)
    {
        queueManager = manager;
        waypointPath = path;

        if (waypointPath == null || waypointPath.waypoints.Length == 0)
        {
            Debug.LogError("CatMover: WaypointPath no asignado o vacío.");
            return;
        }

        StartCoroutine(MoveAlongPath());
    }

    IEnumerator MoveAlongPath()
    {
        for (int i = 0; i < waypointPath.waypoints.Length; i++)
        {
            yield return MoveTo(waypointPath.waypoints[i].position);
        }

        bool joined = queueManager.JoinQueue(this);

        if (!joined)
        {
            Debug.Log("Cola llena, el gato se marcha.");
            Destroy(gameObject);
        }
    }

    public void SetQueuePosition(Vector3 targetPos)
    {
        if (queueMoveCoroutine != null)
            StopCoroutine(queueMoveCoroutine);
        queueMoveCoroutine = StartCoroutine(MoveTo(targetPos));
    }

    IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > arrivalThreshold)
        {
            Vector3 direction = (target - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
    }

    public void OnReachedOrderPoint()
    {
        GetComponent<OrderBubble>()?.ShowOrder();
    }

    public void LeaveQueue()
    {
        GetComponent<OrderBubble>()?.HideOrder();
        queueManager.LeaveQueue(this);
        StartCoroutine(DespawnAfterDelay(1f));
    }

    IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}