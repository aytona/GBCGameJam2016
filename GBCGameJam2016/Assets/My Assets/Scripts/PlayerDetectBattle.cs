using UnityEngine;
using System.Collections;

public class PlayerDetectBattle : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            FindObjectOfType<RPGBattle>().SetEnemy(other.GetComponent<RPGEnemy>());
        }
    }
}
