using UnityEngine;
using System.Collections;

public class spawnABuilding : MonoBehaviour {

	// sits on the player and Instantiates buildings into the world. 
	private GameObject buildingSpawner;
	public GameObject limitSphere;

	// Use this for initialization
	void Start () {
		buildingSpawner = (GameObject) Resources.Load("buildingBuilder");
	}

	public void spawnBuildingWhileMoving( Transform pos ) {
		//called from the player mover script when the min distance has been achieved
		GlobalGeneratorValues.initBuildingPositions( pos.forward.normalized * Camera.main.farClipPlane );	
	}

	public void spawnBuilding ( Vector3 pos, Quaternion spawnRot ) {
		//used to setup initial buildings
		if (buildingSpawner == null) {
			buildingSpawner = (GameObject) Resources.Load("buildingBuilder");
		}
		Instantiate(buildingSpawner, pos, spawnRot );	
	}

	public void spawnLimitSphere( Vector3 pos, float size ) {
		Instantiate(limitSphere, pos, Quaternion.identity);
		print("Limit Sphere");
	}
}
