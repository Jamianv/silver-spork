using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

	//What to chase?
	[SerializeField]
	private Transform target;

	//How many times per second we update our path
	[SerializeField]
	private float updateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rb;

	//the calculated path
	[SerializeField]
	private Path path;

	//The Ai's speed per second
	[SerializeField]
	private float speed = 300f;
	[SerializeField]
	private ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	//the max distance from the ai to a waypoint for it to continue to the next waypoint
	[SerializeField]
	private float nextWaypointDistance  = 3;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	private EnemyHealth enemyhealth;

	void Awake(){
		enemyhealth = GetComponent<EnemyHealth> ();
	}

	void Start(){
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			Debug.LogError ("No Player found!");
			return;
		}

		//start a new path to the target position, return the result to the onPathComplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}

	IEnumerator UpdatePath(){
		if (target == null) {
			//TODO: Insert a player search here.
			//return;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds( 1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete(Path p){
		Debug.Log ("We got a path. Did it have an error?" + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}

	}

	void FixedUpdate(){

		if (enemyhealth.Health < 0) {
			rb.gravityScale = 1;
		}

		if (target == null) {
			//TODO: Insert a player search here.
			return;
		}

		//TODO: Always look at player?

		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded)
				return;
			Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;
		}

		pathIsEnded = false;

		//Direction to the nxt waypoint
		Vector2 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//Move the AI
		rb.AddForce(dir, fMode);
		float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
			if(dist < nextWaypointDistance){
				currentWaypoint++;
				return;
			}
	}
}
