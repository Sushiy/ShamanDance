using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class FireBallSpell : ISpell {

	public override float spellDuration { get { return 5f; } }
	public override float finalizeDuration { get { return 1f; } }

	private CircleCollider2D ballCollider;
	private SpriteRenderer fireBallSprite;
	private ParticleSystem sparks;
	private ParticleSystem fireBallExplode;
	public float castRange = 100f;
	public float speed = 50f;
	private bool isFired;
	private bool explode;

	void Start()
	{
		ballCollider = GetComponent<CircleCollider2D> ();
		fireBallSprite = GetComponent<SpriteRenderer> ();
		foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem> (false)) {
			if (p.name == "FireBallExplode")
				fireBallExplode = p;
			else if(p.name == "Flare")
				sparks = p;
		}
		if (fireBallExplode == null)
			Debug.LogError ("NO PARTICLE SYSTEM FOUND IN FIREBALL: FIREBALLEXPLODE");
		
		isFired = false;

        if (_caster == null)
            _caster = this.gameObject;
		transform.position = _caster.transform.position + Vector3.up*3f;
		_targetPosition = CalculateFireBallHit ();
	}

	protected override void SpellFunction()
	{
		if (!isFired && Input.GetKeyDown (KeyCode.Space)) {
			isFired = true;
			_targetPosition = CalculateFireBallHit ();

			Vector3 dir = _targetPosition - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}

		if (isFired) {
			transform.position = Vector3.MoveTowards (transform.position, _targetPosition, Time.deltaTime * speed);
			if (Vector3.Distance (transform.position, _targetPosition) < 1f) {
				Finalize ();
				fireBallExplode.Play ();
				sparks.gameObject.SetActive(false);
				fireBallSprite.enabled = false;
			}
		} else
			transform.position = _caster.transform.position + Vector3.up*3f;
	}

	protected override void FinalizeFunction()
	{
		
	}

	private Vector3 CalculateFireBallHit()
	{
		Vector3 target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if(Vector3.Distance(transform.position, target) > castRange)
			target = Vector3.Normalize (target) * castRange;
		return target;
	}
}
