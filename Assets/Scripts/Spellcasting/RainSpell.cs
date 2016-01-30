using UnityEngine;
using System.Collections;

public class RainSpell : ISpell {

	public override float spellDuration { get { return 10f; } }
	public override float finalizeDuration { get { return 5f; } }

	private ParticleSystem rain;
	private Animator cloud;

	void Start()
	{
		rain = GetComponentInChildren<ParticleSystem> (false);
		cloud = GetComponentInChildren<Animator> (false);
		cloud.SetTrigger ("CreateCloud");

		Vector3 pos = transform.position;
		pos.x = _targetPosition.x;
		pos.y = Camera.main.transform.position.y + Camera.main.orthographicSize/2f - 1.2f;

		transform.position = pos;
	}

	protected override void SpellFunction()
	{
	}

	protected override void FinalizeFunction()
	{
		rain.Stop ();
		cloud.SetTrigger ("DestroyCloud");
	}


}
