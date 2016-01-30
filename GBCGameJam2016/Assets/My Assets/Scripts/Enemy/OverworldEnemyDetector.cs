using UnityEngine;
using System.Collections;

public class OverworldEnemyDetector : MonoBehaviour {

    public OverworldEnemyAI _AI;

    void Start()
    {
        _AI = GetComponentInChildren<OverworldEnemyAI>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Plsyer detected");
            _AI.DetectPlayerDirection(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Plsyer gone");
            _AI.PlayerGone();
        }
    }
}
