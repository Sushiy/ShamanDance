using UnityEngine;
using System.Collections;

public class WanderBehaviour : MonoBehaviour {

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private Vector2 wanderRange;

    private int wanderDirection; // -1 : left ; +1 : right
    private Vector3 desiredPosition;
    private float wanderDistance;
    private SeekBehaviour seekBehaviour;

	// Use this for initialization
	void Start () {
        wanderDirection = 1;
        setNewDesiredPosition();

        seekBehaviour = this.GetComponentInParent<SeekBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!seekBehaviour.isPlayerInSight)
        {
            transform.Translate(new Vector3(wanderDirection * speed * Time.deltaTime, 0f, 0f));

            if (Vector3.Distance(desiredPosition, transform.position) >= wanderDistance)
            {
                if (wanderDirection == -1) wanderDirection = 1;
                else if (wanderDirection == 1) wanderDirection = -1;
                setNewDesiredPosition();

                transform.localScale = new Vector3(wanderDirection, 1f, 1f);
            }
        }
	}

    void setNewDesiredPosition() {

        wanderDistance = Random.Range(wanderRange.x, wanderRange.y);
        desiredPosition = new Vector3(transform.position.x + wanderDirection * wanderDistance, transform.position.y, transform.position.z);
    }

  
}
