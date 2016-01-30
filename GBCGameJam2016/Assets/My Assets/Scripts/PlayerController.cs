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

	[Tooltip("The ground check object")]
	public Transform groundCheck;

	[Tooltip("The object that contains the character sprite")]
	public GameObject spriteContainer;

	#endregion Public Variables

	#region Private Variables

	/// <summary>
	/// Checker to see if the player is on the ground
	/// </summary>
	private bool isOnGround;

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
		
	}

	#endregion MonoBehaviour

	#region Public Methods

	#endregion Public Methods

	#region Private Methods

	/// <summary>
	/// Checks the input of the player to set the character Orientaton and movement.
	/// </summary>
	private void CheckInput(Vector2 direction)
	{
		//OrientateCharacter (direction);
		Walk (direction);
	}

	/// <summary>
	/// Orientates the character towards the direction they are goign to.
	/// </summary>
//	private void OrientateCharacter(Vector2 direction)
//	{
//		Vector3 spriteScale = spriteContainer.transform.localScale;
//		if (direction.x > 0)
//		{
//			spriteScale.x = 1;
//		}
//		else if (direction.x < 0)
//		{
//			spriteScale.x = -1;
//		}
//		spriteContainer.transform.localScale = spriteScale; 
//	}

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

	/// <summary>
	/// Checks if the groundCheck collides with the ground
	/// </summary>
	private void CheckGround()
	{
		Collider2D collider = Physics2D.OverlapPoint(groundCheck.transform.position);
		isOnGround = (collider != null);
	}

	#endregion Private Methods
}