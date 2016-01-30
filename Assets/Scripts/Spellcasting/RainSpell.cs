using UnityEngine;
using System.Collections;

public class RainSpell : ISpell {

	public override float spellDuration { get { return 10f; } }
	public override float finalizeDuration { get { return 5f; } }

	private ParticleSystem[] emitter;

	void Start()
	{
		
		emitter = GetComponentsInChildren<ParticleSystem> (false);
	}

	protected override void SpellFunction()
	{
		
	}

	protected override void FinalizeFunction()
	{
		foreach (ParticleSystem e in emitter)
			e.Stop ();
	}
}
