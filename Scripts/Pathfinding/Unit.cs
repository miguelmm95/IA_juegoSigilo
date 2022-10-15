using UnityEngine;
using System.Collections;
using System;

public class Unit : MonoBehaviour {


	public Transform target;
	private float speed;
	Vector3[] path;
	int targetIndex;
	//private bool persecuccion = true;
	public Transform wayPoint;


	public StateManager _stateManager;
	public GoBackToPatrolState _goBack;



    private void Start()
    {
		_stateManager = this.GetComponent<StateManager>();
		_goBack = this.GetComponentInChildren<GoBackToPatrolState>();
    }

    void Update() {

        if (_stateManager.currentState is InvestigateState)
        {
			speed = 1;

			PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
		}

		if (_stateManager.currentState is PersecutionState)
		{
			speed = 2;

			PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
		}

		if (_stateManager.currentState is GoBackToPatrolState)
		{
			speed = 2;
			//persecuccion = true;
			PathRequestManager.RequestPath(transform.position, wayPoint.position, OnPathFound);
			//StartCoroutine("Wait");

            if (MathF.Abs((this.transform.position.x - wayPoint.position.x)) < 5 && MathF.Abs((this.transform.position.z - wayPoint.position.z)) < 5)
            {
				_goBack.goToInvestigate = false;
				_goBack.patrolPointReached = true;
            }
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
			Debug.Log(currentWaypoint);
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
            /*if (!persecuccion)
            {
				Array.Clear(path, 0, path.Length-1);
				path[0] = currentWaypoint;
				yield break;
            }*/
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

	/*IEnumerator Wait()
	{	
		yield return new WaitForSeconds(2.0f);
		persecuccion = true;

	}*/
}
