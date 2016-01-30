using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
	/// <summary>
	/// The animator reference.
	/// </summary>
	[SerializeField] private Animator animator = null;

	/// <summary>
	/// The sprite container reference.
	/// </summary>
	[SerializeField] private GameObject spriteContainer = null;

	/// <summary>
	/// The walk force applied to the character when walking.
	/// </summary>
	[SerializeField] private float walkForce = 10f;	

	/// <summary>
	/// The max walk speed enforced for the character.
	/// </summary>
	[SerializeField] private float maxWalkSpeed = 4f;

	/// <summary>
	/// The character's movement direction.
	/// </summary>
	[SerializeField] private Vector2 direction = Vector2.zero;

	/// <summary>
	/// The bomb prefab.
	/// </summary>
	[SerializeField] private GameObject bombPrefab = null;

	/// <summary>
	/// The bomb count.
	/// </summary>
	[SerializeField] private int bombCount = 0;

	/// <summary>
	/// The transform of where the bomb will spawn.
	/// </summary>
	[SerializeField] private Transform bombSpawn = null;

	#region MonoBehaviour

	void FixedUpdate ()
	{
		OrientCharacter(this.direction);
		Walk(this.direction);
	}

	void Update ()
	{
		ProcessInput();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag("MovingPlatform"))
		{
			this.transform.parent = other.transform;
		}

		if (other.CompareTag("FirePit"))
		{
			Death();
		}

		if (other.gameObject.tag == "Door") {
			Application.LoadLevel("MainMenu");
		}

		if (other.gameObject.tag == "BombCrate")
		{
			bombCount = 5;
			Data.Instance.Bomb = 5;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag("MovingPlatform"))
		{
			this.transform.parent = null;
		}
	}

	#endregion MonoBehaviour


	#region Input

	private void ProcessInput ()
	{
		ProcessWalking();
		ProcessBomb();
	}

	/// <summary>
	/// Processes the input for walking.
	/// </summary>
	private void ProcessWalking ()
	{
		this.direction = Vector2.zero;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			this.direction += new Vector2(-1, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			this.direction += new Vector2(1, 0);
		}
	}

	private void ProcessBomb ()
	{
		if (Input.GetKeyDown (KeyCode.B) && bombCount > 0)
		{
			GameObject bomb = Instantiate(this.bombPrefab) as GameObject;
			bomb.transform.position = this.bombSpawn.transform.position;
			bomb.transform.rotation = this.bombSpawn.transform.rotation;
			bombCount--;
			Data.Instance.Bomb--;
		}
	}

	#endregion Input


	#region Activities

	/// <summary>
	/// Orients the character horizontally left or right based on the provided direction.
	/// </summary>
	/// <param name="direction">Direction.</param>
	private void OrientCharacter  (Vector2 direction)
	{
		Vector3 spriteScale = this.spriteContainer.transform.localScale;
		if (direction.x > 0)
		{
			// Positive horizontal scale for the character.
			spriteScale.x = Mathf.Abs(spriteScale.x);
		}
		else if (direction.x < 0)
		{
			// Negative horizontal scale for the character.
			spriteScale.x = Mathf.Abs(spriteScale.x) * -1;
		}
		this.spriteContainer.transform.localScale = spriteScale; 
	}

	/// <summary>
	/// Moves the player in the specified direction via physics forces.
	/// </summary>
	/// <param name="direction">Direction.</param>
	private void Walk (Vector2 direction)
	{
		this.GetComponent<Rigidbody2D>().AddForce(direction * this.walkForce);
		float horizontalSpeed = Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x);

		if (Mathf.Abs(horizontalSpeed) > this.maxWalkSpeed)
		{
			Vector2 newVelocity = this.GetComponent<Rigidbody2D>().velocity;
			float multiplier = (this.GetComponent<Rigidbody2D>().velocity.x > 0) ? 1 : -1;

			// Limit the character's speed to maxWalkSpeed.
			newVelocity.x = this.maxWalkSpeed * multiplier;
			this.GetComponent<Rigidbody2D>().velocity = newVelocity;
		}

		this.animator.SetFloat("HorizontalSpeed", horizontalSpeed);
	}

	private void Death ()
	{
		// Play the death animation.
		this.animator.SetTrigger("Death");

		// Disable this script to prevent further movement.
		this.enabled = false;
	}
	#endregion Activities
}
