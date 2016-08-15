using UnityEngine;
using System.Collections;

public class playerMover : MonoBehaviour {

	public float acceleration = 2f;

	private Rigidbody rb;

	// Building spawner variables
	private float amountToMoveBeforeSpawningABuilding = 100f;
	private Vector3 lastSpawnPosition;
	private float spawnRangeForNewBuildingSpawner;

	// Use this for initialization
	void Start () {
		GlobalGeneratorValues.initBuildingPrefabs(); // this needs to be called somewhere; there's only one player
		GlobalGeneratorValues.initBuildingPositions( transform.position );
		rb = GetComponent<Rigidbody>();
		lastSpawnPosition = transform.position;
		spawnRangeForNewBuildingSpawner = Camera.main.farClipPlane;
	}

	void FixedUpdate () {
		rb.AddForce( transform.forward * acceleration);
	}

	void Update() {
		if (Vector3.Distance( transform.position, lastSpawnPosition) > amountToMoveBeforeSpawningABuilding ) {
			// create a building spawner at spawn range in front of you
			print("new building spawner!");
			GetComponent<spawnABuilding>().spawnBuildingWhileMoving( transform );
			lastSpawnPosition = transform.position;
		}
	}

	void OnTriggerExit(Collider other) {
		// clean up too far buildings
		Destroy(other.gameObject);
	}
}
