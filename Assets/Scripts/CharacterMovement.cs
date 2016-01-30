using UnityEngine;
using System.Collections;

public enum MovementState
{
    GROUNDED,
    JUMPING,
    FALLING,
    CROUCHING
};

[RequireComponent (typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour {

    public float movementSpeed = 10f;
    public float jumpingSpeed = 5f;
    public LayerMask ground_layers;

    public MovementState currentState;

    public bool isGrounded { get { return currentState == MovementState.GROUNDED; } }
    public MovementState getCurrentState() { return currentState; }
    
	// Use this for initialization
	void Start () {
        currentState = MovementState.GROUNDED;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // move left or right
        if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1)
        {
            float sideSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
            transform.position += new Vector3(sideSpeed, 0, 0);
        }
        // get playerSprite borders
        Vector2 topLeft = GameObject.Find("GroundHitCheckTopLeft").transform.position;
        Vector2 bottomRight = GameObject.Find("GroundHitCheckBottomRight").transform.position;
        
        // jump  (Joystick Up or Button "A")
        if ((Input.GetButtonDown("Jump") && currentState == MovementState.GROUNDED) || (Input.GetAxis("Vertical") > 0.2f && currentState == MovementState.GROUNDED))
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpingSpeed), ForceMode2D.Impulse);
            currentState = MovementState.JUMPING;
        }

        if (currentState == MovementState.JUMPING && !Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
            currentState = MovementState.FALLING;

        if (currentState == MovementState.FALLING && Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
            currentState = MovementState.GROUNDED;

        // crouching (Joystick Down)

        if (Input.GetAxis("Vertical") < -0.2 && currentState == MovementState.GROUNDED)
        {
            this.transform.localScale = new Vector3(1f, 0.5f, 1f);
            currentState = MovementState.CROUCHING;
            movementSpeed /= 2;
        }

        // stand up
        if (Input.GetAxis("Vertical") > -0.2 && currentState == MovementState.CROUCHING)
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
            currentState = MovementState.GROUNDED;
            movementSpeed *= 2;

        }
    }
}
