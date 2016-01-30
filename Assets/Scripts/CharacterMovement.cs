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
public class CharacterMovement : MonoBehaviour
{

    float sideSpeed = 0.0f;

    public float movementSpeed = 0.25f;
    public float jumpingSpeed = 5f;
    public LayerMask ground_layers;

    public MovementState currentState;

    public bool isGrounded { get { return currentState == MovementState.GROUNDED; } }
    public MovementState getCurrentState() { return currentState; }

    BoxCollider2D _collider;

    Animator anim;

    Rigidbody2D rigidbody;

    bool left = false;
    bool landed = false;

    // Use this for initialization
    void Start () {
        currentState = MovementState.GROUNDED;
        _collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(2).GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // move left or right
        if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1)
        {
            //float sideSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
            //transform.position += new Vector3(sideSpeed, 0, 0);
        }
        // get playerSprite borders
        Vector2 topLeft = _collider.bounds.min;
        Vector2 bottomRight = _collider.bounds.max;
        
        // jump  (Joystick Up or Button "A")
        if ((Input.GetButtonDown("Jump") && currentState == MovementState.GROUNDED) || (Input.GetAxis("Vertical") > 0.2f && currentState == MovementState.GROUNDED))
        {
            anim.SetTrigger("Jump");
            currentState = MovementState.JUMPING;
        }

        if (currentState == MovementState.JUMPING && !Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
            currentState = MovementState.FALLING;

        if (currentState == MovementState.FALLING && Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
        {
            currentState = MovementState.GROUNDED;
            anim.SetTrigger("Land");
            landed = true;
            sideSpeed *= 0.2f;
        }

        // crouching (Joystick Down)

        if (Input.GetAxis("Vertical") < -0.2 && currentState == MovementState.GROUNDED)
        {
            //this.transform.localScale = new Vector3(1f, 0.5f, 1f);
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

        bool b = false;
        float axis = 0.0f;
        if(Input.GetAxis("LeftTrigger") > 0.5)
        {
            sideSpeed = -1.0f * Time.fixedDeltaTime * movementSpeed;
            b = true;
            left = true;
        }
        if (Input.GetAxis("RightTrigger") > 0.5)
        {
            sideSpeed = 1.0f * Time.fixedDeltaTime * movementSpeed;
            b = true;
            left = false;
        }

        
        transform.position += new Vector3(sideSpeed/2, 0, 0);
        sideSpeed *= 0.75f;
        anim.SetBool("isWalking", b);
        if (left)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void OnDrawGizmos()
    {
        Bounds b = GetComponent<BoxCollider2D>().bounds;
        Gizmos.color = Color.green;
        if (!isGrounded)
            Gizmos.color = Color.red;
        Gizmos.DrawWireCube(b.center, b.size);
    }

    public void Jump()
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpingSpeed), ForceMode2D.Impulse);
    }
}
