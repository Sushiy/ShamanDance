using UnityEngine;
using System.Collections;

public enum MovementState
{
    GROUNDED,
    JUMPING,
    FALLING,
    CRAWLING
};

//[RequireComponent typeof(Rigidbody2D)]
public class CharacterMovement : MonoBehaviour {

    public float movementSpeed = 10f;
    public float jumpingSpeed = 5f;
    public LayerMask ground_layers;

    public MovementState currentState;

   /* private bool _isGrounded;
    private bool _isJumping; */
    public bool isGrounded { get { return currentState == MovementState.GROUNDED; } }
    
	// Use this for initialization
	void Start () {
        currentState = MovementState.GROUNDED;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        // move left or right
        float sideSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        transform.position += new Vector3(sideSpeed, 0, 0);

        // check if player is on ground
        Vector2 topLeft = GameObject.Find("GroundHitCheckTopLeft").transform.position;
        Vector2 bottomRight = GameObject.Find("GroundHitCheckBottomRight").transform.position;

        // move up
        if (Input.GetAxis("Vertical") > 0.2f && currentState == MovementState.GROUNDED)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpingSpeed), ForceMode2D.Impulse);
            currentState = MovementState.JUMPING;
        }

        if (currentState == MovementState.JUMPING && !Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
        {

            currentState = MovementState.FALLING;
        }

        if (currentState == MovementState.FALLING && Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
        {

            currentState = MovementState.GROUNDED;
        }
  
    }
}
