using UnityEngine;
using System.Collections;

public class OverworldEnemyAI : MonoBehaviour {
    public Transform leftCheck, rightCheck;
    public float speed = 1.0f;
    public float waitTime = 1.0f;
    public float walkTimeMin = 3.0f;
    public float walkTimeMax = 6.0f;

    private bool isIdle = true;
    private bool waiting = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(WalkAndWait());
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        if (transform.position.x > leftCheck.position.x - 0.1f && transform.position.x < rightCheck.position.x + 0.1f)
        {
            if (!waiting)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        if (isIdle)
        {
            if (transform.position.x <= leftCheck.position.x)
            {
                speed = Mathf.Abs(speed);
            }
            if (transform.position.x >= rightCheck.position.x)
            {
                speed = Mathf.Abs(speed) * -1;
            }
        }

    }

    /// <summary>
    /// If the player enters the enemy's radius.
    /// </summary>
    public void DetectPlayerDirection(Transform player)
    {
        //Debug.Log("Called Detect");
        isIdle = false;
        waiting = false;
        Vector2 direction = player.position - transform.position;
        if (direction.x < 0)
        {
            speed = Mathf.Abs(speed) * -1;
        }
        else if (direction.x > 0)
        {
            speed = Mathf.Abs(speed);
        }
        speed *= 1.5f;
    }

    public void PlayerGone()
    {
        //Debug.Log("Called");
        isIdle = true;
        speed /= 1.5f;
        StartCoroutine(WalkAndWait());
    }

    private IEnumerator WalkAndWait()
    {
        float walkTime = Random.Range(walkTimeMin, walkTimeMax);
        yield return new WaitForSeconds(walkTime);
        if (isIdle)
        {
            waiting = true;
            yield return new WaitForSeconds(waitTime);
            waiting = false;
            StartCoroutine(WalkAndWait());
        }
    }
}
