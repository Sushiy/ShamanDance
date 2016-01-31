using UnityEngine;
using System.Collections;

public enum MovementState
{
    GROUNDED,
    JUMPING,
    FALLING,
    DROWNING
};


[RequireComponent (typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
	// private stuff
	private Vector3 playerLeft = new Vector3 (-1f, 1f, 1f);
	private Transform spawnPosition;
	[SerializeField]
	private MovementState currentState;
	private BoxCollider2D _collider;
	private Rigidbody2D rigidbody;
	private ParticleSystem spawnEmitter;
	private float drownTimer;

	// public attributes for inspector
    public float movementSpeed 	= 10f;
    public float jumpingSpeed 	= 5f;
	public float drownTime 		= 1f;
    public LayerMask ground_layers;
	public LayerMask water_layers;

	// PROPERTIES
	public bool isGrounded 	 { get { return currentState == MovementState.GROUNDED; } }
	public bool isJumping 	 { get { return currentState == MovementState.JUMPING;  } }
	public bool isFalling 	 { get { return currentState == MovementState.FALLING;  } }
	public bool isDrowning 	 { get { return currentState == MovementState.DROWNING; } }
	public bool isTurnedLeft { get { return transform.localScale.x == -1f; 		  	} }


    // Use this for initialization
    void Start () 
	{
		// find spawnposition
		GameObject spawn = GameObject.Find ("SPAWN POSITION");
		if (spawn == null) {
			Debug.LogError ("NO SPAWN POSITION OBJECT! Spawn at Camera center");
			spawnPosition = Camera.main.transform;
		} else {
			spawnPosition = spawn.transform;
		}

		// find spawn emitter
		foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>(false)) {
			if (p.name == "Spawn") {
				spawnEmitter = p;
				break;
			}
		}

		if (ground_layers == null)
			Debug.LogError ("GroundLayer not set in CharacterMovement component");

		if (water_layers == null)
			Debug.LogError ("WaterLayer not set in CharacterMovement component");
		
		// init
        currentState = MovementState.GROUNDED;
        _collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

		// spawn player
		Spawn ();
    }

	private void Spawn()
	{
		rigidbody.freezeRotation = true;
		transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, 0f);
		transform.rotation = Quaternion.identity;
		currentState = MovementState.GROUNDED;
		rigidbody.velocity = new Vector2 (0f, 0f);

		spawnEmitter.Play ();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{

		// get playerSprite borders
		Vector2 topLeft = _collider.bounds.min;
		Vector2 bottomRight = _collider.bounds.max;

		// ----------------------------- MOVEMENT STATES -----------------------------

		// --- DROWNING ---
		if (isDrowning) {
			// - respawn -
			if(Time.fixedTime > drownTimer+ drownTime) {
				spawnEmitter.Play ();
				currentState = MovementState.FALLING;
				Invoke ("Spawn", 1f);
			}
			// do nothing more
			return;
		}

		// --- GROUNDED ---
		else if (isGrounded) {
			// - jump -
			if (Input.GetButtonDown ("Jump")) {
				currentState = MovementState.JUMPING;
				rigidbody.AddForce (new Vector2 (0f, jumpingSpeed / 2f), ForceMode2D.Impulse);
			}

			// - crab walk -
			float sideSpeed = 0;
			// left
			if (Input.GetAxis ("LeftTrigger") > 0.5 || Input.GetButton("LeftTrigger")) {
				sideSpeed = -1f;
				transform.localScale = playerLeft;
			} 
			// right
			else if (Input.GetAxis ("RightTrigger") > 0.5 || Input.GetButton("RightTrigger")) {
				transform.localScale = Vector3.one;
				sideSpeed = 1f;
			}
			Vector2 characterVelocity = new Vector2 (sideSpeed * movementSpeed, rigidbody.velocity.y); // where y is gravity 
			GetComponent<Rigidbody2D> ().velocity = characterVelocity;
		} 

		// --- JUMPING ---
		else if (isJumping) {
			// - fall -
			if (!Physics2D.OverlapArea (topLeft, bottomRight, ground_layers))
				currentState = MovementState.FALLING;
		}

		// --- FALLING --
		else if (isFalling) {
			// - ground player -
			if(Physics2D.OverlapArea(topLeft, bottomRight, ground_layers))
            	currentState = MovementState.GROUNDED;
		}

		// THOUCH WATER --> DIE!!!!!!!
			if (Physics2D.OverlapArea (topLeft, bottomRight, water_layers) || (isFalling && rigidbody.velocity.y > 100f)) {
			currentState = MovementState.DROWNING;
			drownTimer = Time.fixedTime;
			rigidbody.freezeRotation = false;
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

