using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class FireBallSpell : ISpell {

	public override float spellDuration { get { return 5f; } }
	public override float finalizeDuration { get { return 1f; } }
    
    private Transform handTransform;

	private CircleCollider2D ballCollider;
	private SpriteRenderer fireBallSprite;
	private ParticleSystem sparks;
	private ParticleSystem fireBallExplode;
	public float speed = 50f;
	private bool isFired;
    private bool hasFired;
    private bool canFire;
	private bool explode;

    Rigidbody2D rigid;

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
        canFire = false;
        if (_caster == null)
            _caster = this.gameObject;
        handTransform = _caster.transform.GetChild(2).GetChild(4);
        //transform.parent = handTransform;
        rigid = GetComponent<Rigidbody2D>();
	}

	protected override void SpellFunction()
	{
        float LX = Input.GetAxis("Horizontal");
        float LY = Input.GetAxis("Vertical");

        Vector2 targetDirection = new Vector2(LX, LY) / 2f;
        Vector2 direction = Vector2.zero;
        if (!isFired)
        {
            direction = targetDirection;
            if ((Mathf.Abs(LX) < 0.3f || Mathf.Abs(LY) < 0.3f))
            {
                Invoke("SetCanFire", 1f);
            }

            if (canFire && (Mathf.Abs(LX) > 0.7f || Mathf.Abs(LY) > 0.7f))
			{
				isFired = true;

				Vector3 dir = _targetPosition - transform.position;
				float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}
        }
            

        if (isFired)
        {
            if(!hasFired)
            {
                hasFired = true;
                rigid.isKinematic = (false);
                rigid.AddForce(direction.normalized * speed * 3f, ForceMode2D.Impulse);
            }
		}
        else
            transform.position = handTransform.position;
    }
    
    private void SetCanFire()
    {
        canFire = true;
        hasFired = false;
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

    void OnCollisionEnter2D(Collision2D c)
    {
        GameObject g = c.gameObject;
        if(g.CompareTag("SpellTarget"))
        {
            g.GetComponent<ISpellTarget>().TakeSpell(SpellType.FIRE);
        }
        Finalize();
        fireBallExplode.Play();
        sparks.gameObject.SetActive(false);
        fireBallSprite.enabled = false;
    }
}
