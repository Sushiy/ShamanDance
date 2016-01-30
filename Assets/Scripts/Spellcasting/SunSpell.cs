using UnityEngine;
using System.Collections;

public class SunSpell : ISpell {

	public override float spellDuration { get { return 1f; } }
	public override float finalizeDuration { get { return 0f; } }

	protected override void SpellFunction()
	{
		Debug.DrawLine (Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height)), transform.position);
	}

	protected override void FinalizeFunction()
	{
		
	}
}
