using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spellcaster : MonoBehaviour {

	private List<ISpell> _spellList;

	public FireBallSpell fireBallSpellPrefab;
	public RainSpell 	 rainSpellPrefab;
    public WindSpell     windSpellPrefab;

	// Use this for initialization
	void Start () {
		_spellList = new List<ISpell> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            castSpell(SpellType.RAIN, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			castSpell (SpellType.FIRE, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            castSpell(SpellType.WIND, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

	private void castSpell(SpellType type, Vector3 targetLocation)
	{
		ISpell spell = null;

		switch (type) {
		case SpellType.FIRE:
			spell = Instantiate (fireBallSpellPrefab) as ISpell;
			break;
		case SpellType.RAIN:
			spell = Instantiate (rainSpellPrefab) as ISpell;
			break;
        case SpellType.WIND:
             spell = Instantiate(windSpellPrefab) as ISpell;
             break;
		default:
			Debug.LogError ("Invalid Spell Type");
			return;
		}
		spell.Activate (_spellList, targetLocation, this.gameObject);
	}
}
