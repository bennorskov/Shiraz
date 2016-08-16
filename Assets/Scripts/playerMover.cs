using UnityEngine;
using System.Collections;

public class playerMover : MonoBehaviour {

	public float acceleration = 2f;

	private Rigidbody rb;

	// Building spawner variables
	private float amountToMoveBeforeSpawningABuilding = 100f;
	private Vector3 lastSpawnPosition;
	private spawnABuilding sAB;

	private float maxSpeed = 10f;

	// Use this for initialization
	void Start () {
		GlobalGeneratorValues.initBuildingPrefabs(); // this needs to be called somewhere; there's only one player
		GlobalGeneratorValues.initBuildingPositions( transform.position );
		rb = GetComponent<Rigidbody>();
		sAB = GetComponent<spawnABuilding>();
		lastSpawnPosition = transform.position;
	}

	void FixedUpdate () {
		rb.AddForce( transform.forward * acceleration);

		if (rb.velocity.magnitude>maxSpeed) {
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

	void Update() {
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
