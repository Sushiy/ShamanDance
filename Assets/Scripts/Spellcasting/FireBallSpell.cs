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
	public float castRange = 50f;
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
	}

	protected override void SpellFunction()
	{
		if (!isFired) {
			float LX = Input.GetAxis ("Horizontal");
			float LY = Input.GetAxis ("Vertical");

			Vector2 targetDirection = new Vector2 (LX, LY) / 2f;

			if(Mathf.Abs(LX) > 0.7f || Mathf.Abs(LY) > 0.7f)
			{
				_targetPosition = CalculateFireBallHit (targetDirection);
				isFired = true;

				Vector3 dir = _targetPosition - transform.position;
				float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}
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
		if (!isFired) {
			transform.position = _caster.transform.position + Vector3.up * 3f;
			fireBallSprite.transform.localScale = Vector3.Lerp (fireBallSprite.transform.localScale, new Vector3 (0, 0, 0), Time.fixedDeltaTime*5f);
			fireBallSprite.color = Color.Lerp (fireBallSprite.color, new Color (1f, 1f, 1f, 0f), Time.fixedDeltaTime * 5f);
			sparks.gameObject.SetActive (false);
		}
	}

	private Vector3 CalculateFireBallHit(Vector2 direction)
	{
		return Vector3.Normalize (new Vector3(direction.x, direction.y, 0f)) * castRange;
	}
}
