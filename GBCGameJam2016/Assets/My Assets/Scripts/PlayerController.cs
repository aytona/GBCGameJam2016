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

	[Tooltip("The object that contains the player sprite")]
	public GameObject spriteContainer;

	#endregion Public Variables

	#region Private Variables

	/// <summary>
	/// Checker to see if the player is on the ground
	/// </summary>
	private bool isOnGround = false;

	/// <summary>
	/// Array of bools to check if the player has each power
	/// </summary>
	private bool[] powers;

	/// <summary>
	/// The player's movement direction
	/// </summary>
	private Vector2 currentDirection = Vector2.one;

	/// <summary>
	/// Reference to the rigidbody
	/// </summary>
	private Rigidbody2D rb2d;

	/// <summary>
	/// The current amount of jumps
	/// </summary>
	private int jumpCount;

	#endregion Private Variables

	#region MonoBehaviour

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		powers = new bool[numOfPowers];
	}

	void FixedUpdate()
	{
		BasicMovement (Vector2.right * Input.GetAxis("Horizontal"));
	}

	void Update()
	{
		CheckGround ();
	}

	#endregion MonoBehaviour

	#region Basic Movement Methods

	/// <summary>
	/// Update for basic movement methods
	/// </summary>
	private void BasicMovement(Vector2 direction)
	{
		Walk (direction);
		Jump ();
	}

	/// <summary>
	/// Walk to the specified direction.
	/// </summary>
	private void Walk(Vector2 direction)
	{
		if (Input.GetButton("Fire3"))
			transform.Translate (direction * Time.deltaTime * maxWalkSpeed);
		else
			transform.Translate (direction * Time.deltaTime * walkSpeed);
	}

	private void Jump()
	{
		if (Input.GetKeyDown (KeyCode.Space) && isOnGround) 
		{
			rb2d.AddForce (Vector2.up * jumpForce);
			jumpCount++;
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
			jumpCount = 0;
	}

	#endregion Basic Movement Methods

	#region Power Movement Methods

	/// <summary>
	/// Update for power movement methods
	/// </summary>
	private void PowerMovement()
	{
		DoubleJump ();
	}

	/// <summary>
	/// Power index of 0
	/// Makes the character jump while in air
	/// </summary>
	private void DoubleJump()
	{
		if (Input.GetKeyDown (KeyCode.Space) && powers [0] && jumpCount < 2)
			rb2d.AddForce (Vector2.up * jumpForce);
	}

	/// <summary>
	/// Power index of 1
	/// Teleports the player towards the mouse
	/// </summary>
	private void Teleport()
	{

	}

	/// <summary>
	/// Power index of 2
	/// Makes the player enter or exit phase
	/// </summary>
	private void Phase()
	{

	}

	/// <summary>
	/// Power index of 3
	/// Makes the player fly
	/// </summary>
	private void Fly()
	{

	}
	#endregion Power Movement Methods
}