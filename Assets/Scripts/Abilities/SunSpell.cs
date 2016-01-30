using UnityEngine;
using System.Collections;

public class SunSpell : ISpell {

	public override float duration { get { return 1f; } }

	protected override void SpellFunction()
	{
		Debug.DrawLine (Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height)), transform.position);
	}
}
