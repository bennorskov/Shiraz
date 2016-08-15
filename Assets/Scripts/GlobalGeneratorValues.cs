using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GlobalGeneratorValues {

	/* Models from:
	 * Midtown Manhattan by Karim Naaji is licensed under CC Attribution https://skfb.ly/MKXH
	 * 
	 */

	public static List<GameObject> buildings = new List<GameObject>();
	private static bool prefabsInitialized = false;

	public static float fullSizeBuildingScale = 20f;
	public static float buildingReduction = 0.61803398876895f; // 1 / golden ratio

	private static List<Quaternion> spawnRots = new List<Quaternion>();

	private static float buildingLimit = Camera.main.farClipPlane;
	private static float minDistBetweenBuildings = 100f;
	private static int spawnBuildingsThisManyTimes = 20; // how many initialization loops 
	private static int buildingCountPerSpawn = 20; // used for each initalization loop
	private static int totalIterations;

	private static spawnABuilding spawner;

	public static void initBuildingPrefabs() {
		if ( prefabsInitialized ) return; // this is called a couple places
		RenderSettings.fogColor = Color.red; //global fog color call

		spawner = GameObject.FindWithTag("Player").GetComponent<spawnABuilding>();
		totalIterations = spawnBuildingsThisManyTimes;

		// This is the master list of buildings
		// each building needs to have a prefab saved in the "Resources" folder
		// Call each building by prefab name here:
		buildings.Add( (GameObject) Resources.Load("4B_1") );
		buildings.Add( (GameObject) Resources.Load("4B_2") );
		buildings.Add( (GameObject) Resources.Load("4B_3") );
		buildings.Add( (GameObject) Resources.Load("4B_4") );
		buildings.Add( (GameObject) Resources.Load("11+Brunel+Court") );
		buildings.Add( (GameObject) Resources.Load("AuroraPlace") );
		buildings.Add( (GameObject) Resources.Load("Broward+Financial+Center") );
		buildings.Add( (GameObject) Resources.Load("Empire+State+Building.fbx") );
		buildings.Add( (GameObject) Resources.Load("texturedbldg") );
		buildings.Add( (GameObject) Resources.Load("trianglebuilding") );
		buildings.Add( (GameObject) Resources.Load("TriangleBldg2") );
		buildings.Add( (GameObject) Resources.Load("UB_1") );
		buildings.Add( (GameObject) Resources.Load("UB_2") );
		buildings.Add( (GameObject) Resources.Load("UB_3") );
		buildings.Add( (GameObject) Resources.Load("UB_4") );
		buildings.Add( (GameObject) Resources.Load("UB_5") );
		buildings.Add( (GameObject) Resources.Load("UB_6") );

		spawnRots.Add( Quaternion.Euler( Vector3.up * 90 ) );
		spawnRots.Add( Quaternion.Euler( Vector3.up * 90 ) ); //weighting up higher than other directions slightly
		spawnRots.Add( Quaternion.Euler( Vector3.left * 90 ) );
		spawnRots.Add( Quaternion.Euler( Vector3.forward * 90 ) );
		spawnRots.Add( Quaternion.Euler( Vector3.back * 90 ) );
		spawnRots.Add( Quaternion.Euler( Vector3.right * 90 ) );
		spawnRots.Add( Quaternion.Euler( Vector3.down * 90 ) );
		Debug.Log("end global generator Init");
		prefabsInitialized = true;
	}
	public static void initBuildingPositions( Vector3 startPos ) {

		// setup a field of buildings based on initial spawn distances
		// spawn buildings in many diretions

		int buildingCount = buildingCountPerSpawn; // how many buildings should spawn with any iteration

		for (int i = 0; i<buildingCount; i++){
			Vector3 newPos = startPos + Random.insideUnitSphere * minDistBetweenBuildings;
			spawner.spawnBuilding( newPos, getSpawnRotation());
		}
		// check dist against farClipPlane
		// if less, move in 6 directions double spawn dist
		if (spawnBuildingsThisManyTimes > 0){
			spawnBuildingsThisManyTimes--;
			// call this function again 
			initBuildingPositions( startPos + (minDistBetweenBuildings * (totalIterations - spawnBuildingsThisManyTimes) * Random.insideUnitSphere) );
		}
	}

	private static Vector3 getNewSpawnPos( Vector3 newPos ) {
		return Vector3.one;
	}

	public static GameObject getRandomBuilding() {
		return buildings[ Mathf.FloorToInt( Random.Range(0, buildings.Count) ) ];
	}

	public static Quaternion getSpawnRotation() {
		int i = Mathf.FloorToInt( Random.Range(0, spawnRots.Count) );
		return spawnRots[ i ];
	}
}
