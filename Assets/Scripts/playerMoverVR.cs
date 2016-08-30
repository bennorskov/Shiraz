using UnityEngine;
using System.Collections;
using Gvr.Internal;
using UnityStandardAssets.Cameras;

public class playerMoverVR : MonoBehaviour {

	public float acceleration = 20f;

	private Rigidbody rb;

	// Building spawner variables
	private float amountToMoveBeforeSpawningABuilding = 100f;
	private Vector3 lastSpawnPosition;
	private spawnABuilding sAB;

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

		GetComponent<FreeLookCam>().enabled = !vrViewer.VRModeEnabled;

		Cursor.visible = false;
	}

	void FixedUpdate () {
		Vector3 v;
		if (vrViewer.VRModeEnabled) {
			v = vrViewer.HeadPose.Orientation * Vector3.forward;
		} else {
			v = transform.forward;
		}
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
