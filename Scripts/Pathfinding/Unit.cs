using UnityEngine;
using System.Collections;
using System;

public class Unit : MonoBehaviour {


	public Transform target;
	private float speed;
	Vector3[] path;
	int targetIndex;
	//private bool persecuccion = true;
	//public Transform wayPoint;
	public Vector3 puntoInvestigacion;
	public bool impactoSonido;


	public StateManager _state;
	public PatrolState _patrolState;
	public InvestigateState _investigateState;
	public PersecutionState _persecutionState;
	//public GoBackToPatrolState _goBack;



    private void Start()
    {
		_state = this.GetComponent<StateManager>();
		_investigateState = this.GetComponentInChildren<InvestigateState>();
		_patrolState = this.GetComponentInChildren<PatrolState>();
		_persecutionState = this.GetComponentInChildren<PersecutionState>();
		//_goBack = this.GetComponentInChildren<GoBackToPatrolState>();
    }

    void Update() {

		//_investigateState.playerLost = false;
		
        if (_state.currentState is InvestigateState)
        {
			speed = 5;

			if(impactoSonido){
				PathRequestManager.RequestPath(transform.position, puntoInvestigacion, OnPathFound);
				impactoSonido = false;
				if(Vector3.Distance(this.transform.position, puntoInvestigacion) <= 0.5f){
					_patrolState.goToInvestigate = false;
					_investigateState.playerLost = true;
					
				}
			}
			else{
				//Debug.Log(_investigateState.playerLost);
				PathRequestManager.RequestPath(transform.position, puntoInvestigacion, OnPathFound);
				if(Vector3.Distance(this.transform.position, puntoInvestigacion) <= 0.5f){
					_patrolState.goToInvestigate = false;
					_investigateState.playerLost = true;
				}
			}
		}

		if (_state.currentState is PersecutionState)
		{
			speed = 3;

			PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
		}

		/*if (_state.currentState is GoBackToPatrolState)
		{
			speed = 10;
			//persecuccion = true;
			PathRequestManager.RequestPath(transform.position, wayPoint.position, OnPathFound);
			//StartCoroutine("Wait");

            if (Mathf.Abs((this.transform.position.x - wayPoint.position.x)) < 3.0f && Mathf.Abs((this.transform.position.z - wayPoint.position.z)) < 3.0f)
            {
				_goBack.goToInvestigate = false;
				_goBack.patrolPointReached = true;
            }
		}*/
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
