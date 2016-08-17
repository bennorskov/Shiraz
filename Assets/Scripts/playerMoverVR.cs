using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class playerMoverVR : MonoBehaviour {

	public float acceleration = 20f;

	private Rigidbody rb;

	// Building spawner variables
	private float amountToMoveBeforeSpawningABuilding = 100f;
	private Vector3 lastSpawnPosition;
	private spawnABuilding sAB;

	public Transform t;

	private GvrViewer vrViewer;

	public float maxSpeed = 100f;

	// Use this for initialization
	void Start () {
		GlobalGeneratorValues.initBuildingPrefabs(); // this needs to be called somewhere; there's only one player
		GlobalGeneratorValues.initBuildingPositions( transform.position );
		rb = GetComponent<Rigidbody>();
		sAB = GetComponent<spawnABuilding>();
		vrViewer = GetComponent<GvrViewer>();
		lastSpawnPosition = transform.position;
	}

	void FixedUpdate () {
		Vector3 v = vrViewer.HeadPose.Orientation * Vector3.forward;
		rb.AddForce(v  * acceleration);

		if (rb.velocity.magnitude>maxSpeed) {
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

	void Update() {
		//transform.rotation = vrViewer.HeadPose.Orientation;

		if (Vector3.Distance( transform.position, lastSpawnPosition) > amountToMoveBeforeSpawningABuilding ) {
			// create a building spawner at spawn range in front of you
			print("new building spawner!");
			sAB.spawnBuildingWhileMoving( transform );
			lastSpawnPosition = transform.position;
		}
	}

	void OnTriggerExit(Collider other) {
		// clean up buildings beyond a collision trigger sphere (buildings too far)
		Destroy(other.gameObject);
	}
}
