using UnityEngine;
using System.Collections;

public enum MovementState
{
    GROUNDED,
    JUMPING,
    FALLING,
    CROUCHING,
    DANCING
};

[RequireComponent (typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour {

    public float movementSpeed = 10f;
    public float jumpingSpeed = 5f;
    public LayerMask ground_layers;

    public MovementState currentState;

    public bool isGrounded { get { return currentState == MovementState.GROUNDED; } }
    public MovementState CurrentState { get; set; }


    BoxCollider2D _collider;
    
    Rigidbody2D rigidbody;

    // Use this for initialization
    void Start () {
        currentState = MovementState.GROUNDED;
        _collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        /*float downspeed = 0f;// GetComponent<Rigidbody2D>().velocity.y*2;
           if (GetComponent<Rigidbody2D>().velocity.y > -0.1f && currentState != MovementState.GROUNDED) downspeed = -9.81f;

        if (Input.GetKeyDown(KeyCode.F))
            Debug.Log(downspeed);
*/
        // move left or right
        if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1)
        {
            float sideSpeed = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * movementSpeed ;

            Vector2 characterVelocity = new Vector2( sideSpeed, rigidbody.velocity.y); // where y is gravity 
            GetComponent<Rigidbody2D>().velocity = characterVelocity;

            //transform.position += new Vector3(sideSpeed, 0, 0);
            // Vector3 newPosition = transform.position + new Vector3(sideSpeed, downspeed, 0);
            //   GetComponent<Rigidbody2D>().MovePosition(newPosition);


        }
    
        // get playerSprite borders
        
        Vector2 topLeft = _collider.bounds.min;
        Vector2 bottomRight = _collider.bounds.max;
        
        // jump  (Joystick Up or Button "A")
        if ((Input.GetButtonDown("Jump") && currentState == MovementState.GROUNDED) || (Input.GetAxis("Vertical") > 0.2f && currentState == MovementState.GROUNDED) )//&& downspeed == 0)
        {
            //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpingSpeed), ForceMode2D.Impulse);
            currentState = MovementState.JUMPING;
        }

        if (currentState == MovementState.JUMPING && !Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
            currentState = MovementState.FALLING;

        if ((currentState == MovementState.FALLING || currentState == MovementState.JUMPING) && Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
            currentState = MovementState.GROUNDED;

        // crouching (Joystick Down)

        if (Input.GetAxis("Vertical") < -0.2 && currentState == MovementState.GROUNDED)
        {
            // this.transform.localScale = new Vector3(1f, 0.5f, 1f);  replace Sprite
            currentState = MovementState.CROUCHING;
            movementSpeed /= 2;
        }

        // stand up
        if (Input.GetAxis("Vertical") > -0.2 && currentState == MovementState.CROUCHING)
        {
            // this.transform.localScale = new Vector3(1f, 1f, 1f); replace Sprite
            currentState = MovementState.GROUNDED;
            movementSpeed *= 2;

        }

        if(isGrounded)
        {
            if(Input.GetAxis("LeftTrigger") > 0.5)
            {
                rigidbody.AddForce(new Vector2(-jumpingSpeed/4, jumpingSpeed/2), ForceMode2D.Impulse);
                currentState = MovementState.JUMPING;
            }
            if (Input.GetAxis("RightTrigger") > 0.5)
            {
                rigidbody.AddForce(new Vector2(jumpingSpeed / 4, jumpingSpeed / 2), ForceMode2D.Impulse);
                currentState = MovementState.JUMPING;
            }
        }
    }

    void OnDrawGizmos()
    {
        Bounds b = GetComponent<BoxCollider2D>().bounds;
        Gizmos.color = Color.green;
        if (!isGrounded)
            Gizmos.color = Color.red;
        Gizmos.DrawWireCube(b.center, b.size);
    }
}
