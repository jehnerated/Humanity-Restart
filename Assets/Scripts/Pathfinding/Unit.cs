using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	public Vector3 target;
	public Vector3 resourceLocation;
	public Vector3 refinaryLocation;

	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	public bool crashTest = false;
	public bool targetReached = true;
	public bool followingPath = false;

	public string actionType;

	Path path;

	public ResourceInfo targetMine;


	void Start() {
		StartCoroutine (UpdatePath ());
	}

    public void ResetTarget()
    {

    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new Path(waypoints, transform.position, turnDst, stoppingDst);

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath() {

		if (Time.timeSinceLevelLoad < .3f) {
			yield return new WaitForSeconds (.3f);
		}
		PathRequestManager.RequestPath (new PathRequest(transform.position, target, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target;

		while (true) {
			yield return new WaitForSeconds (minPathUpdateTime);
			if ((target - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				PathRequestManager.RequestPath (new PathRequest(transform.position, target, OnPathFound));
				targetPosOld = target;
			}
		}
	}

    IEnumerator FollowPath() {

		followingPath = true;
		int pathIndex = 0;
		transform.LookAt (path.lookPoints [0]);

		float speedPercent = 1;

		while (followingPath) {
			gameObject.GetComponent<Animator>().SetInteger("AniChoice", 1);
			crashTest = false;
			Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finishLineIndex) {
					followingPath = false;
					gameObject.GetComponent<Animator>().SetInteger("AniChoice", 0);
					if (targetMine)
					{
						if (targetMine.getResources() > 0 || targetMine != null)
						{
							if (actionType == "Mining")
							{
								gameObject.GetComponent<Animator>().SetInteger("AniChoice", 2);
								yield return new WaitForSeconds(2);
								gameObject.GetComponent<Animator>().SetInteger("AniChoice", 1);
								PathRequestManager.RequestPath(new PathRequest(transform.position, refinaryLocation, OnPathFound));
								actionType = "Refining";
								targetMine.removeResources(10);
							}
							if (actionType == "Refining")
							{
								PathRequestManager.RequestPath(new PathRequest(transform.position, resourceLocation, OnPathFound));
								actionType = "Mining";
								Player.addResource(10);
							}
						}
					}
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {

				if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
					speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
					if (speedPercent < 0.01f)
					{
						followingPath = false;
						gameObject.GetComponent<Animator>().SetInteger("AniChoice", 0);
						if (targetMine.getResources() > 0 || targetMine != null)
						{
							if (actionType == "Mining")
							{
								gameObject.GetComponent<Animator>().SetInteger("AniChoice", 2);
								yield return new WaitForSeconds(2);
								gameObject.GetComponent<Animator>().SetInteger("AniChoice", 1);
								PathRequestManager.RequestPath(new PathRequest(transform.position, refinaryLocation, OnPathFound));
								actionType = "Refining";
								targetMine.removeResources(10);
							}
							if (actionType == "Refining")
							{
								PathRequestManager.RequestPath(new PathRequest(transform.position, resourceLocation, OnPathFound));
								actionType = "Mining";
								Player.addResource(10);
							}
						}
					}
				}

				Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}

			yield return null;

		}

	}
}
