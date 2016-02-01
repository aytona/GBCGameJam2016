using UnityEngine;
using System.Collections;

public class AltarInventory : MonoBehaviour {

	#region Variables

	/// <summary>
	/// The amount of mats required to complete this altar
	/// </summary>
	public int matGoal;

	/// <summary>
	/// The current amount mats that has been stored
	/// </summary>
	[SerializeField]
	private int currentMats;

	/// <summary>
	/// The left over mats after receiving
	/// </summary>
	private int leftOver;

	#endregion Variables

	#region MonoBehaviour

	void Update()
	{
		if (currentMats == matGoal)
		{
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	#endregion MonoBehaviour

	#region Public Methods

	/// <summary>
	/// Function for the altar to receive all the mats from the player's inventory
	/// </summary>
	public int ReceiveMats(int amount)
	{
		if (currentMats + amount <= matGoal)
		{
			currentMats += amount;
			leftOver = 0;
		}
		else if (currentMats + amount > matGoal)
		{
			leftOver = currentMats + amount - matGoal;
			currentMats += amount - leftOver;
		}
        PlayerPrefs.SetInt("mats", leftOver);
		PlayerPrefs.SetInt(gameObject.name, currentMats);
		return leftOver;
	}

	#endregion Public Methods
}
