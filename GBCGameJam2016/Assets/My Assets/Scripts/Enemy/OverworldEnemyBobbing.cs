using UnityEngine;
using System.Collections;

public class OverworldEnemyBobbing : MonoBehaviour {
    public float bobSpeed = 1.0f;
    public float maxY, minY;

    void Update()
    {
        if (transform.position.y > maxY)
        {
            bobSpeed = Mathf.Abs(bobSpeed) * -1;
        }
        else if (transform.position.y < minY)
        {
            bobSpeed = Mathf.Abs(bobSpeed);
        }
        transform.Translate(Vector2.up * bobSpeed * Time.deltaTime);
    }
}
