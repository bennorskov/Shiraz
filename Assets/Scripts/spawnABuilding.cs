using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnABuilding : MonoBehaviour {

	// sits on the player and Instantiates buildings into the world. 
	private GameObject buildingSpawner;
	private Rigidbody rb;

	private float spawnRange = 700f; // 200 more than far clip plane: buildings are really big

	private List<Vector3> spawnLocations = new List<Vector3>();
	private float minSpawnDist = 100f;

	// Use this for initialization
	void Start () {
		buildingSpawner = (GameObject) Resources.Load("buildingBuilder");
		rb = GetComponent<Rigidbody>();
	}

	public void spawnBuildingWhileMoving( Transform pos ) {
		//called from the player mover script when the min distance has been achieved
		GlobalGeneratorValues.initBuildingPositions( transform.position + rb.velocity.normalized * spawnRange );	
	}

	public void spawnBuilding ( Vector3 pos, Quaternion spawnRot ) {
		//used to setup initial buildings
		if (buildingSpawner == null) {
			buildingSpawner = (GameObject) Resources.Load("buildingBuilder");
		}
		// only spawn inside a minimum distance
		if ( checkPosition(pos) ) Instantiate(buildingSpawner, pos, spawnRot );	
	}

	private bool checkPosition(Vector3 _pos) {
		// only spawn a building based on a minium distance from other spawn locations
		foreach(Vector3 oldPos in spawnLocations) {
			if (Vector3.Distance(oldPos, _pos) < minSpawnDist) {
//				print("too close");
				return false;
			}
		}
		spawnLocations.Add(_pos);
		if (spawnLocations.Count > 2000) {
			spawnLocations.RemoveAt(0); // avoid memory leak, as long as Garbage Collection works like this!
		}
		return true;
	}

	void dontrun_OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position+rb.velocity.normalized * spawnRange);
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay( new Ray(transform.position, transform.forward) );
	}
}
