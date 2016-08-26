using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class addCameraEffects : MonoBehaviour {

	private GameObject pre;
	private GameObject post;

	void Start () {
		pre = GameObject.Find("PreRender");
		post = GameObject.Find("PostRender");
		post.AddComponent<myEdgeDetection>();
	}
}
