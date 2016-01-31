using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhaseFilter : MonoBehaviour {

	public float transitionSpeed;
	
	private PlayerController _player;
	private RectTransform filterTransform;
	private float rectSize;

	void Awake()
	{
		_player = FindObjectOfType<PlayerController> ();
		filterTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
		if (_player.currentState == PlayerController.PlayerState.Phase)
		{
			if (filterTransform.sizeDelta.x < Camera.current.pixelWidth)
				rectSize += (Time.deltaTime * transitionSpeed);
			filterTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectSize);
			filterTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectSize);
		} 
		else if (_player.currentState != PlayerController.PlayerState.Phase)
		{
			if (filterTransform.sizeDelta.x > 0)
				rectSize -= (Time.deltaTime * transitionSpeed);
			filterTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,rectSize);
			filterTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectSize);
		}
	}
}
