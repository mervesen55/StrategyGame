using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathFollower : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Queue<Vector3> pathQueue = new Queue<Vector3>();
    private bool isMoving = false;

    public void FollowPath(List<GridCell> path)
    {
        pathQueue.Clear();

        foreach (var cell in path)
        {
            pathQueue.Enqueue(cell.WorldPosition + new Vector3(GridManager.Instance.CellSize / 2f, GridManager.Instance.CellSize / 2f, 0f));
        }

        if (!isMoving)
            StartCoroutine(MoveAlongPath());
    }

    private IEnumerator MoveAlongPath()
    {
        isMoving = true;

        while (pathQueue.Count > 0)
        {
            Vector3 targetPos = pathQueue.Dequeue();

            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;
        }

        isMoving = false;
    }
}
