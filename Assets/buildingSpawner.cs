using UnityEngine;
using System.Collections;

public class buildingSpawner : MonoBehaviour {

	private GameObject building;
	public int buildingSequenceNumber = 0;

	// Use this for initialization
	void Start () {
		GlobalGeneratorValues.initBuildingPrefabs();
		building = GlobalGeneratorValues.getRandomBuilding();
		building.transform.localScale = Vector3.one * GlobalGeneratorValues.fullSizeBuildingScale *  Mathf.Pow( GlobalGeneratorValues.buildingReduction, buildingSequenceNumber);
		Instantiate(building, transform.position, transform.rotation);
	}
}
