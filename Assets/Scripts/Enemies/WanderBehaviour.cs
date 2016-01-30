using UnityEngine;
using System.Collections;

public class WanderBehaviour : MonoBehaviour {

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
            transform.Translate(new Vector3(wanderDirection * Time.deltaTime, 0f, 0f));

            if (Vector3.Distance(desiredPosition, transform.position) >= wanderDistance)
            {
                if (wanderDirection == -1) wanderDirection = 1;
                else if (wanderDirection == 1) wanderDirection = -1;
                setNewDesiredPosition();
            }
        }
	}

    void setNewDesiredPosition() {

        wanderDistance = Random.Range(3f, 5f);
        desiredPosition = new Vector3(transform.position.x + wanderDirection * wanderDistance, transform.position.y, transform.position.z);
    }

  
}
