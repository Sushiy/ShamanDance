using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class WaterHole : MonoBehaviour {

	public float _fillHeight;
	private BoxCollider2D waterBox;
	private float maxHeight;
	private Transform waterTex;
	public float fillHeight { get { return _fillHeight; } }

	// Use this for initialization
	void Start () {
		_fillHeight = 0f;
		waterBox = GetComponent<BoxCollider2D> ();
		waterTex = (GetComponentInChildren<SpriteRenderer> (false)).transform;
		maxHeight = waterBox.size.y;

		transform.position = new Vector3 (transform.position.x, transform.position.y - waterBox.size.y / 2f);
		waterBox.size = new Vector2 (waterBox.size.x, 0.01f);
		waterBox.offset = new Vector2 (waterBox.offset.x, 0.005f);
	}
	
	// Update is called once per frame
	void Update () {
		waterBox.size = Vector2.Lerp (waterBox.size, new Vector2 (waterBox.size.x, 0.1f + _fillHeight * maxHeight), Time.deltaTime);
		waterBox.offset = Vector2.Lerp (waterBox.offset, new Vector2 (0f, (-0.05f + _fillHeight * maxHeight)/2f), Time.deltaTime);
		waterTex.localScale = new Vector2(waterBox.size.x, waterBox.size.y + 0.2f);
		waterTex.localPosition = new Vector3 (waterTex.localPosition.x, waterBox.size.y/2f - 0.2f, 0f);
	}

	void OnParticleCollision(GameObject other) {
		if (other.CompareTag ("Rain") && _fillHeight < 1f) {
			_fillHeight += 0.005f;
		}
	}
}
