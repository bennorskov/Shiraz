using UnityEngine;
using System.Collections;

public class titleCardDestroy : MonoBehaviour {

	public float timeToShow = 8f;
	private SpriteRenderer sr;
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		StartCoroutine(FadeOutDestory());
	}
	IEnumerator FadeOutDestory() {
		yield return new WaitForSeconds(timeToShow);
		transform.parent = null;
		while (sr.color.a > 0) {
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - .01f);
			yield return null;
		}
		Destroy(gameObject);
	}
		
}
