using UnityEngine;
using System.Collections;

public class SunSpell : ISpell {

	public override float spellDuration { get { return 10f; } }
	public override float finalizeDuration { get { return 5f; } }

	private Light sceneLight;
	public float sunIntensity = 2f;

	void Start()
	{
		sceneLight = GameObject.FindGameObjectWithTag ("Light").GetComponent<Light>();
	}

	protected override void SpellFunction()
	{
		Debug.DrawLine (Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height)), transform.position);
		sceneLight.intensity = Mathf.Lerp (sceneLight.intensity, sunIntensity, Time.deltaTime * 0.5f);
	}

	protected override void FinalizeFunction()
	{
		sceneLight.intensity = Mathf.Lerp (sceneLight.intensity, 1f, Time.deltaTime * 0.5f);
	}
}
