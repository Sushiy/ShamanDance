using UnityEngine;
using System.Collections;

public class WaterWheelPlatform : MonoBehaviour {

	private Wheel wheel;
	[SerializeField]
	private float height = 0f;
	public float maxHeight = 10f;
	private float rotSpeed = 0f;
	private Vector3 botPos;

	// Use this for initialization
	void Start () {
		wheel = GetComponentInChildren<Wheel> ();
		botPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		height += Time.deltaTime * wheel.rotationSpeed * 3f;
		height = Mathf.Clamp (height, 0, maxHeight);


		transform.position = Vector3.Lerp (transform.position, botPos + (Vector3.up * height), Time.fixedDeltaTime);

		float rot = 0f;
		if (transform.position.y > botPos.y + 0.05f)
			rot = wheel.rotationSpeed * 3f;

		rotSpeed = Mathf.Lerp (rotSpeed, rot, Time.deltaTime * 3f);
		
		wheel.transform.Rotate (Vector3.back * rotSpeed);
	}
}
