using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	#region Variables

	/// <summary>
	/// The animator reference.
	/// </summary>
	[SerializeField] private Animator animator = null;

	/// <summary>
	/// The sprite container reference.
	/// </summary>
	[SerializeField] private GameObject spriteContainer = null;

	/// <summary>
	/// The walk speed.
	/// </summary>
	public float walkSpeed;

	/// <summary>
	/// The walk speed cap.
	/// </summary>
	public float walkSpeedCap;

	/// <summary>
	/// The character's movement direction.
	/// </summary>
	[SerializeField] private Vector2 direction = Vector2.zero;

	#endregion Variables

	#region MonoBehaviour

	void Update ()
	{
		ProcessInput();
		//OrientCharacter(direction);
	}

	#endregion MonoBehaviour


	#region Input

	private void ProcessInput ()
	{
		ProcessWalking();
	}

	/// <summary>
	/// Processes the input for walking.
	/// </summary>
	private void ProcessWalking ()
	{
		float translation = Input.GetAxis ("Horizontal") * walkSpeed;
		translation *= Time.deltaTime;
		transform.Translate (Vector2.right);
	}

	#endregion Input


	#region Activities

	/// <summary>
	/// Orients the character horizontally left or right based on the provided direction.
	/// </summary>
	/// <param name="direction">Direction.</param>
	/*private void OrientCharacter  (Vector2 direction)
	{
		Vector3 spriteScale = spriteContainer.transform.localScale;
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
		spriteContainer.transform.localScale = spriteScale; 
	}*/

	private void Death ()
	{
		// Play the death animation.
		// animator.SetTrigger("Death");

		// Disable this script to prevent further movement.
		enabled = false;
	}
	#endregion Activities
}
