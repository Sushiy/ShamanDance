using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class WindSpell : ISpell {

    public override float spellDuration { get { return 5f; } }
    public override float finalizeDuration { get { return 1f; } }

    private ParticleSystem windEmitter;
    private bool isCalled;
    private int forceDirection;

    private BoxCollider2D trigger;
    void Start()
    {
        windEmitter = GetComponentInChildren<ParticleSystem>(false);
        trigger = GetComponent<BoxCollider2D>();
        isCalled = false;
    }
    
    protected override void SpellFunction() {

        transform.position = _caster.transform.position;
        if (!isCalled)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 dir = transform.right;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            forceDirection = dir.x > 0 ? -1 : 1;
            isCalled = true;
        }
        _caster.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceDirection * 4f, 0.01f));
         
       

    }
    protected override void FinalizeFunction() {

        windEmitter.Stop();
    }

    void OnTriggerStay2D(Collider2D other) {
       
      Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();
      if (rigidbody != null && other.CompareTag("Enemy"))
      {
          other.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f, 0.01f));
      }
        
    }
}
