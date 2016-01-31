using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
public class Wheel : MonoBehaviour {

	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		rotationSpeed = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(rotationSpeed > -1f)
			rotationSpeed -= Time.deltaTime;
		
	}

	void OnParticleCollision(GameObject other) {
		Debug.Log ("AAJ");
		if (other.CompareTag ("Rain")) {
			rotationSpeed = 1f;	
		}
	}

}
