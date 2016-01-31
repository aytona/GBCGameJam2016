using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

    public Text number;

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        number.text = player.mats.ToString();
	}
}
