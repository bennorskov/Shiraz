using UnityEngine;
using System.Collections;

public class buildingSpawner : MonoBehaviour {

	private GameObject building;
	private AudioClip clip;
	public int buildingSequenceNumber = 0;

	// Use this for initialization
	public void Start () {
		GlobalGeneratorValues.initBuildingPrefabs();
		building = GlobalGeneratorValues.getRandomBuilding(); 

		if (Random.value * 2 < 1) { //don't always put an audio clip in
			clip = GlobalGeneratorValues.getRandomAudioClip();
			AudioSource ac = gameObject.AddComponent<AudioSource>();
			ac.clip = clip;
			ac.loop = true;
			ac.spatialBlend = 1.0f; //full 3D Sound
			ac.playOnAwake = true;
		}
//		print("about to place: " + building.name);
//		building.transform.localScale = Vector3.one * GlobalGeneratorValues.fullSizeBuildingScale *  Mathf.Pow( GlobalGeneratorValues.buildingReduction, buildingSequenceNumber);
		GameObject b = Instantiate(building, transform.position, transform.rotation) as GameObject;
		b.transform.localScale *= GlobalGeneratorValues.fullSizeBuildingScale;
		b.transform.parent = transform;
	}	
}
