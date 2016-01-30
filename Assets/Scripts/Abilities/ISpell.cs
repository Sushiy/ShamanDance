using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SpellType 
{ 
	SUN,
	RAIN
};

// INTERFACE FOR SPELLS
public abstract class ISpell : MonoBehaviour 
{
	// ATTRIBUTES
	private   float _castRange;
	private   bool  _isActivated;
	private   float _activationTime;

	// REFERENCES
	private List<ISpell> _activeSpells;

	// PROPERTIES
	public float castRange { get { return _castRange; } }
	public abstract float duration { get; }
	public bool isActivated { get { return _isActivated; } }

	// METHODS
	void FixedUpdate() {
		if (_isActivated) {
			// call spell function
			if (Time.time < _activationTime + duration) {
				SpellFunction ();
			}
			// destroy the spell
			else {
				_activeSpells.Remove (this);
				_isActivated = false;
				Destroy (this.gameObject);
			}
		}
	}

	public void Activate(List<ISpell> activeSpells, Vector3 targetPosition)
	{
		_activeSpells = activeSpells;
		_activeSpells.Add (this);
		transform.position = targetPosition;
		_activationTime = Time.time;
		_isActivated = true;
	}

	// override in derived class
	protected abstract void SpellFunction();
}
