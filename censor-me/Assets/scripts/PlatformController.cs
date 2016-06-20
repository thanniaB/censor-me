using UnityEngine;

public class PlatformController : MonoBehaviour {
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
	}

	void FixedUpdate () {
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		if (myTransform.position.x + myTransform.GetComponent<Collider2D> ().bounds.size.x < min.x) {
			Destroy (gameObject);
		}
	}
}
