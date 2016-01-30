using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	#region Public Variables

	[Tooltip("Walking speed")]
	public float walkSpeed;

	[Tooltip("The speed cap of the player")]
	public float maxWalkSpeed;

	[Tooltip("Amount of force for jump")]
	public float jumpForce;

	[Tooltip("The ground check object")]
	public Transform groundCheck;

	[Tooltip("The object that contains the character sprite")]
	public GameObject spriteContainer;

	#endregion Public Variables

	#region Private Variables

	/// <summary>
	/// Checker to see if the player is on the ground
	/// </summary>
	private bool isOnGround = false;

	/// <summary>
	/// The character's movement direction
	/// </summary>
	private Vector2 currentDirection = Vector2.one;

	/// <summary>
	/// Reference to the rigidbody
	/// </summary>
	private Rigidbody2D rb2d;

	#endregion Private Variables

	#region MonoBehaviour

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		CheckInput (Vector2.right * Input.GetAxis("Horizontal"));
	}

	void Update()
	{
		CheckGround ();
	}

	#endregion MonoBehaviour

	#region Public Methods

	#endregion Public Methods

	#region Basic Movement Methods

	/// <summary>
	/// Checks the input of the player to set the character Orientaton and movement.
	/// </summary>
	private void CheckInput(Vector2 direction)
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
			Debug.Log ("Jump");
			rb2d.AddForce (Vector2.up * jumpForce);
		}
	}

	/// <summary>
	/// Checks if the groundCheck collides with the ground
	/// </summary>
	private void CheckGround()
	{
		Collider2D collider = Physics2D.OverlapPoint(groundCheck.transform.position);
		isOnGround = (collider != null);
	}

	#endregion Basic Movement Methods
}