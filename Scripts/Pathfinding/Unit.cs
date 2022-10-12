using UnityEngine;
using System.Collections;
using System;

public class Unit : MonoBehaviour {


	public Transform target;
    public Transform wayPointPatrol;
	public float speed = 2;
	Vector3[] path;
	int targetIndex;
	public bool persecuccion = false;
    public bool returnPatrol = false;

	void Update() {
        if (persecuccion)
        {
			PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
		}

        if (returnPatrol)
        {
            PathRequestManager.RequestPath(transform.position, wayPointPatrol.position, OnPathFound);
        }
	}


	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");

		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];
		while (true) {
			
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
            if (!persecuccion && !returnPatrol)
            {
				Array.Clear(path, 0, path.Length-1);
				path[0] = currentWaypoint;
				yield break;
            }
			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			yield return null;

		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
