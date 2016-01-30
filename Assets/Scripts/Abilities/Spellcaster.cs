using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spellcaster : MonoBehaviour {

	private List<ISpell> _spellList;

	// Use this for initialization
	void Start () {
		_spellList = new List<ISpell> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1))
			castSpell (SpellType.SUN, Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

	private void castSpell(SpellType type, Vector3 targetLocation)
	{
		System.Type spellType = null;
		string spellName;

		switch (type) {
		case SpellType.SUN:
			spellType = typeof(SunSpell);
			spellName = "SunSpell";
			break;
		case SpellType.RAIN:
			spellName = "RainSpell";
			break;
		default:
			Debug.LogError ("Invalid Spell Type");
			return;
		}
		GameObject spellObject = new GameObject (spellName);
		ISpell spell = spellObject.AddComponent (spellType) as ISpell;
		spell.Activate (_spellList, targetLocation);
	}
}
