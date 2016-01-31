using UnityEngine;
using System.Collections;

public class AttackBehaviour : MonoBehaviour {

    [SerializeField]
    private Transform toungeTarget;
    [SerializeField]
    private float AttackDelay = 0.5f;
    [SerializeField]
    private float AttackDuration = 1f;

    private Transform uppertarget;
    private Transform lowertarget;
    private Vector3 orig_tounge_pos;
    private float attackTimer = -1f;
    bool attacking = false;

    void Awake()
    {
        orig_tounge_pos = toungeTarget.localPosition;
        uppertarget = transform.FindChild("uppertarget");
        lowertarget = transform.FindChild("lowertarget");
    }

    void Update()
    {
        if (attackTimer >= 0f) {
            attackTimer += Time.deltaTime;

            if (attackTimer > AttackDuration) {
                attackTimer = -1f;
                toungeTarget.position = transform.position;
                attacking = false;
            }
        }
    }

    public void StartAttack()
    {
        if (attackTimer < 0f && !attacking) {
            Invoke("Attack", AttackDelay);
            attacking = true;
        }
    }

    void Attack()
    {
        Vector3 target;
        float rndm = Random.Range(0f, 1f);
        target = Vector3.Lerp(uppertarget.position, lowertarget.position, rndm);
        toungeTarget.position = target;
        attackTimer = Time.deltaTime;
    }
}
