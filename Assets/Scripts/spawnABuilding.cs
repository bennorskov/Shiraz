using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnABuilding : MonoBehaviour {

	// sits on the player and Instantiates buildings into the world. 
	private GameObject buildingSpawner;
	private Rigidbody rb;

	private List<Vector3> spawnLocations = new List<Vector3>();
	private float minSpawnDist = 100f;
	private float maxSpawnDist = 1000f;

	// Use this for initialization
	void Start () {
		buildingSpawner = (GameObject) Resources.Load("buildingBuilder");
		rb = GetComponent<Rigidbody>();
	}

	public void spawnBuildingWhileMoving( Transform pos ) {
		//called from the player mover script when the min distance has been achieved
		GlobalGeneratorValues.initBuildingPositions( rb.velocity.normalized * Camera.main.farClipPlane );	
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
		return true;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, GetComponent<Rigidbody>().velocity.normalized * Camera.main.farClipPlane);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine( transform.position, transform.forward * 20f );
	}
}
