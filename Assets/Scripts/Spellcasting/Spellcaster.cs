using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spellcaster : MonoBehaviour {

	private List<ISpell> _spellList;

	public SunSpell sunSpellPrefab;
	public RainSpell rainSpellPrefab;

	// Use this for initialization
	void Start () {
		_spellList = new List<ISpell> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			castSpell (SpellType.RAIN, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		if (Input.GetMouseButtonDown (1))
			castSpell (SpellType.SUN, Camera.main.transform.position);
	}

	private void castSpell(SpellType type, Vector3 targetLocation)
	{
		ISpell spell = null;

		switch (type) {
		case SpellType.SUN:
			spell = Instantiate (sunSpellPrefab) as ISpell;
			break;
		case SpellType.RAIN:
			spell = Instantiate (rainSpellPrefab) as ISpell;
			break;
		default:
			Debug.LogError ("Invalid Spell Type");
			return;
		}
		spell.Activate (_spellList, targetLocation);
	}
}
