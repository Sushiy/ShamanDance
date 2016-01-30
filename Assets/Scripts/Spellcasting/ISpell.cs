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
	private   bool  _finalize;

	// REFERENCES
	private List<ISpell> _activeSpells;

	// PROPERTIES
	public float castRange { get { return _castRange; } }
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
					_activationTime = Time.time;
					_finalize = true;
					Debug.Log ("Finalize");
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

	public void Activate(List<ISpell> activeSpells, Vector3 targetPosition)
	{
		_activeSpells = activeSpells;
		_activeSpells.Add (this);
		transform.position = targetPosition;
		_activationTime = Time.time;
		_isActivated = true;
		_finalize = false;
	}

	// override in derived class
	protected abstract void SpellFunction();
	protected abstract void FinalizeFunction();
}
