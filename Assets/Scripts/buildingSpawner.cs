using UnityEngine;
using System.Collections;

public class buildingSpawner : MonoBehaviour {

	private GameObject building;
	public int buildingSequenceNumber = 0;

	// Use this for initialization
	public void Start () {
		GlobalGeneratorValues.initBuildingPrefabs();
		building = GlobalGeneratorValues.getRandomBuilding(); 

		AudioClip clip = GlobalGeneratorValues.getRandomAudioClip();
		AudioSource ac = GetComponent<AudioSource>();
		ac.clip = clip;
		ac.loop = true;
		ac.Play();
//		print("Clip: " + clip);

//		print("about to place: " + building.name);
//		building.transform.localScale = Vector3.one * GlobalGeneratorValues.fullSizeBuildingScale *  Mathf.Pow( GlobalGeneratorValues.buildingReduction, buildingSequenceNumber);
		if (building != null) {
			GameObject b = Instantiate(building, transform.position, transform.rotation) as GameObject;
			b.transform.localScale *= GlobalGeneratorValues.fullSizeBuildingScale;
			b.transform.parent = transform;
		} else {
			Debug.Log("Building not found! " + building);
		}
	}	
}
