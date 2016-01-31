using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	#region Enums

	/// <summary>
	/// The type of movement states the player can be in
	/// </summary>
	public enum PlayerState
	{
		Normal,
		Flying,
		Phase
	}

	#endregion Enums

	#region Public Variables

	[Tooltip("Walking speed")]
	public float walkSpeed;

	[Tooltip("The speed cap of the player")]
	public float maxWalkSpeed;

	[Tooltip("Amount of force for jump")]
	public float jumpForce;

	[Tooltip("The amount of powers there are in total")]
	public int numOfPowers;

	[Tooltip("The ground check object")]
	public Transform groundCheck;

	[Tooltip("The max length for teleportation")]
	public float maxTeleLength;

	[Tooltip("The object that contains the player sprite")]
	public GameObject spriteContainer;

	[Tooltip("The slider to where the character would teleport to")]
	public GameObject teleSlider;

	[Tooltip("The arrow tip")]
	public GameObject arrowTip;

	[Tooltip("The current state of the player")]
	public PlayerState currentState;

	[Tooltip("Inventory of the player")]
	public int mats;

	#endregion Public Variables

	#region Private Variables

	/// <summary>
	/// Checker to see if the player is on the ground
	/// </summary>
	private bool isOnGround = false;

	/// <summary>
	/// Array of bools to check if the player has each power
	/// </summary>
    [SerializeField]
	private bool[] powers;

	/// <summary>
	/// Reference to the rigidbody
	/// </summary>
	private Rigidbody2D rb2d;

	/// <summary>
	/// The current amount of jumps
	/// </summary>
	private int jumpCount;

	/// <summary>
	/// The anims of the player
	/// </summary>
	private Animator anims;

	/// <summary>
	/// Trigger to check if player is on an altar
	/// </summary>
	private bool onAltar;

	/// <summary>
	/// The name of the altar
	/// </summary>
	private string altarName = "";

	#endregion Private Variables

	#region MonoBehaviour

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		powers = new bool[numOfPowers];
		currentState = PlayerState.Normal;
		anims = GetComponentInChildren<Animator>();
	}

	void Update ()
	{
		BasicMovement (Vector2.right * Input.GetAxis ("Horizontal"));
		PowerMovement ();
		CheckGround ();
		WalkingAnim ();
		if (onAltar)
		{
			if (Input.GetKeyDown (KeyCode.JoystickButton3))
			{
				if (mats > 0)
				{
					anims.SetTrigger ("Interact");
					mats = GameObject.Find (altarName).GetComponent<AltarInventory> ().ReceiveMats (mats);
				}
				else
				{
					anims.SetTrigger("Empty");
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Interactable")
		{
			altarName = other.name;
			onAltar = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Interactable")
		{
			altarName = "";
			onAltar = false;
		}
	}

	#endregion MonoBehaviour

	#region Basic Movement Methods

	/// <summary>
	/// Update for basic movement methods
	/// </summary>
	private void BasicMovement(Vector2 direction)
	{
		if (currentState == PlayerState.Normal)
		{
			Walk (direction);
			Jump ();
		}
		OrientPlayer ();
	}

	/// <summary>
	/// Walk to the specified direction.
	/// </summary>
	private void Walk (Vector2 direction)
	{
		if (Input.GetKey (KeyCode.JoystickButton5) || Input.GetKey (KeyCode.JoystickButton7))
		{
			transform.Translate (direction * Time.deltaTime * maxWalkSpeed);
			anims.SetBool("Running", true);
			anims.SetBool("Moving", false);
		}
		else
		{
			anims.SetBool("Running", false);
			anims.SetBool("Moving", true);
			transform.Translate (direction * Time.deltaTime * walkSpeed);
		}
	}

	private void Jump()
	{
		if (Input.GetKeyDown (KeyCode.JoystickButton0) && isOnGround) 
		{
			rb2d.AddForce (Vector2.up * jumpForce);
			jumpCount++;
			anims.SetTrigger("Jump");
		}
	}

	/// <summary>
	/// Checks if the groundCheck collides with the ground
	/// </summary>
	private void CheckGround()
	{
		Collider2D collider = Physics2D.OverlapPoint(groundCheck.transform.position);
		isOnGround = (collider != null);
		if (isOnGround)
		{
			jumpCount = 0;
			anims.SetBool("Jumping", isOnGround);
			if (currentState == PlayerState.Flying)
			{
				if (Input.GetAxis ("Vertical") < 0)
				{
					currentState = PlayerState.Normal;
				}
			}
		}
		else if (!isOnGround)
		{
			anims.SetBool("Jumping", isOnGround);
		}
	}

	/// <summary>
	/// Orients the player to the direction he is going towards to
	/// </summary>
	private void OrientPlayer()
	{
		if (Input.GetAxis ("Horizontal") < 0)
			transform.localScale = Vector2.one;
		if (Input.GetAxis ("Horizontal") > 0)
			transform.localScale = Vector2.left + Vector2.up;
	}

	#endregion Basic Movement Methods

	#region Power Movement Methods

	/// <summary>
	/// Update for power movement methods
	/// </summary>
	private void PowerMovement()
	{
		if (powers[0])
			DoubleJump ();
		if (powers[1])
			Teleport ();
		if (powers[2])
			PhaseMode ();
		if (powers[3])
		{
			FlyMode ();
			if (currentState == PlayerState.Flying)
				Fly ();
		}
	}

	/// <summary>
	/// Power index of 0
	/// Makes the character jump while in air
	/// </summary>
	private void DoubleJump()
	{
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && powers[0] && jumpCount < 1)
        {
			anims.SetTrigger("Jump");
            rb2d.AddForce(Vector2.up * jumpForce);
            jumpCount++;
        }
	}

	/// <summary>
	/// Power index of 1
	/// Teleports the player towards the targeted spot
	/// </summary>
	private void Teleport()
	{
		if (Input.GetKey (KeyCode.JoystickButton1)) 
		{
			if (teleSlider.transform.localScale.x < maxTeleLength)
				teleSlider.transform.localScale += new Vector3 (0.01f, 0, 0);
			else if (teleSlider.transform.localScale.x >= maxTeleLength)
				teleSlider.transform.localScale = new Vector2 (maxTeleLength, 1);
		}
		if (Input.GetKeyUp(KeyCode.JoystickButton1))
		{
			transform.position = arrowTip.transform.position;
			teleSlider.transform.localScale = Vector2.up;
		}
	}

	/// <summary>
	/// Power Index of 2
	/// Initialize phase mode and deactivates it
	/// </summary>
	private void PhaseMode()
	{
		if (Input.GetKeyDown (KeyCode.JoystickButton4))
		{
			if (currentState == PlayerState.Normal)
			{
				currentState = PlayerState.Phase;
				Physics2D.IgnoreLayerCollision (0, 8, true);
			} 
			else if (currentState == PlayerState.Phase)
			{
				currentState = PlayerState.Normal;
				Physics2D.IgnoreLayerCollision (0, 8, false);
			}
			anims.SetTrigger("Phase");
		}
		if (currentState == PlayerState.Phase)
		{
			Walk (Vector2.right * Input.GetAxis ("Horizontal"));
		}
	}

	/// <summary>
	/// Power index of 3
	/// Makes the player fly
	/// </summary>
	private void Fly()
	{
		if (Input.GetAxis("Right X-Axis") != 0 || Input.GetAxis("Right Y-Axis") != 0)
			transform.Translate (Input.GetAxis ("Right X-Axis") * walkSpeed * Time.deltaTime, Input.GetAxis ("Right Y-Axis") * walkSpeed * Time.deltaTime, 0);
	}

	/// <summary>
	/// Initialize fly mode and deactivates it
	/// </summary>
	private void FlyMode()
	{
		Vector2 currentPosition = transform.position;
		if (Input.GetKeyDown(KeyCode.JoystickButton6) && currentState == PlayerState.Normal)
		{
			rb2d.velocity = Vector2.zero;
			currentState = PlayerState.Flying;
			transform.position = new Vector2(currentPosition.x, currentPosition.y + 0.1f);
			rb2d.gravityScale = 0;
			anims.SetBool("Flying", true);
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton6) && currentState == PlayerState.Flying) 
		{
			rb2d.gravityScale = 1;
			currentState = PlayerState.Normal;
			anims.SetBool("Flying", false);
		}
	}
	#endregion Power Movement Methods

	#region Anims

	private void WalkingAnim()
	{
		if (Input.GetAxis("Horizontal") != 0)
			anims.SetBool("Moving", true);
		else
			anims.SetBool("Moving", false);
	}

	#endregion Anims
}