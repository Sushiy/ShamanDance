﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SpellType 
{ 
	FIRE,
	RAIN,
	WIND,
    NULL
};

// INTERFACE FOR SPELLS
public abstract class ISpell : MonoBehaviour 
{
	// ATTRIBUTES
	private bool _isActivated;
	private float _activationTime;
	private bool _finalize;
	protected Vector3 _targetPosition;
	protected GameObject _caster;

	// REFERENCES
	private List<ISpell> _activeSpells;

	// PROPERTIES
	public bool isActivated { get { return _isActivated; } }
	public abstract float spellDuration { get; }
	public abstract float finalizeDuration { get; }

	// METHODS
	void FixedUpdate() {
		if (_isActivated) {
			if (!_finalize) {
				// SPELL IS ACTIVE
				if (Time.time < _activationTime + spellDuration) {
					SpellFunction ();
				}
				// SWITCH TO FINALIZE
				else {
					Finalize ();
				}
			} else {
				// SPELL IS FINALIZING
				if (Time.time < _activationTime + finalizeDuration) {
					FinalizeFunction ();
				}
				// SPELL IS FINALIZED -> DELETE
				else {
					_activeSpells.Remove (this);
					_isActivated = false;
					Destroy (this.gameObject);
				}
			}
		}
	}

	protected void Finalize ()
	{
		_activationTime = Time.time;
		_finalize = true;
	}


	public void Activate(List<ISpell> activeSpells, Vector3 targetPosition, GameObject caster)
	{
		transform.position = new Vector3(0,0,0);

		_caster = caster;
		_targetPosition = targetPosition;
		_activeSpells = activeSpells;
		_activeSpells.Add (this);
		_activationTime = Time.time;
		_isActivated = true;
		_finalize = false;
	}

	// override in derived class
	protected abstract void SpellFunction();
	protected abstract void FinalizeFunction();
}
