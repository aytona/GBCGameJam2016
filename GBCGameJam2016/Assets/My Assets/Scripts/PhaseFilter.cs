using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhaseFilter : MonoBehaviour {

	#region Public Variables

	public float transitionSpeed;

	#endregion Public Variables

	#region Private Variables

	private PlayerController _player;
	private RectTransform filterTransform;
	private float rectSize;

	#endregion Private Variables

	#region MonoBehaviour

	void Awake()
	{
		_player = FindObjectOfType<PlayerController> ();
		filterTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
		if (_player.currentState == PlayerController.PlayerState.Phase)
			if (filterTransform.sizeDelta.x < Camera.main.pixelWidth)
				rectSize += (Time.deltaTime * transitionSpeed);
		if (_player.currentState != PlayerController.PlayerState.Phase)
			if (filterTransform.sizeDelta.x > 0)
				rectSize -= (Time.deltaTime * transitionSpeed);

		filterTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectSize);
		filterTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectSize);
	}

	#endregion MonoBehaviour
}
