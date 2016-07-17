using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GlobalGeneratorValues {

	/* Models from:
	 * Midtown Manhattan by Karim Naaji is licensed under CC Attribution https://skfb.ly/MKXH
	 * 
	 */

	public static List<GameObject> buildings = new List<GameObject>();

	public static float fullSizeBuildingScale = 70f;
	public static float buildingReduction = 0.61803398876895f; // 1 / golden ratio

	private static List<Quaternion> spawnRots = new List<Quaternion>();

	public static void initBuildingPrefabs() {
		buildings.Add( (GameObject) Resources.Load("tower_antenna") );

		for (int i = 0; i < 360; i+=90) {
			spawnRots.Add( Quaternion.Euler( Vector3.up * i ) );
			spawnRots.Add( Quaternion.Euler( Vector3.left * i ) );
			spawnRots.Add( Quaternion.Euler( Vector3.forward * i ) );
		}
	}

	public static GameObject getRandomBuilding() {
		return buildings[ Mathf.FloorToInt( Random.Range(0, buildings.Count) ) ];
	}

	public static Quaternion getSpawnRotation() {
		int i = Mathf.FloorToInt( Random.Range(0, spawnRots.Count) );
		return spawnRots[ i ];
	}
}
