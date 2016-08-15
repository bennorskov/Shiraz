﻿using UnityEngine;
using System.Collections;

public class buildingSpawner : MonoBehaviour {

	private GameObject building;
	public int buildingSequenceNumber = 0;

	// Use this for initialization
	public void Start () {
		GlobalGeneratorValues.initBuildingPrefabs();
		building = GlobalGeneratorValues.getRandomBuilding();
//		building.transform.localScale = Vector3.one * GlobalGeneratorValues.fullSizeBuildingScale *  Mathf.Pow( GlobalGeneratorValues.buildingReduction, buildingSequenceNumber);
		GameObject b = Instantiate(building, transform.position, transform.rotation) as GameObject;
		b.transform.localScale *= GlobalGeneratorValues.fullSizeBuildingScale;
		b.transform.parent = transform;
	}	
}
