using UnityEngine;
using System.Collections;

public class OverworldEnemyAI : MonoBehaviour {
    public Transform leftCheck, rightCheck;
    public float speed = 1.0f;
    public float waitTime = 1.0f;
    public float walkTimeMin = 3.0f;
    public float walkTimeMax = 6.0f;

    public bool canFollowPlayer = false;
    public float followDistance = 1.5f;

    private bool isIdle = true;
    private bool waiting = false;

    private PlayerController _player;
    private Collider2D _collider;

	// Use this for initialization
	void Start () {
        StartCoroutine(WalkAndWait());
        _player = FindObjectOfType<PlayerController>();
        _collider = GetComponentInChildren<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!canFollowPlayer)
        {
            Move();
        }
        else
        {
            FollowPlayer();
        }
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

    private void FollowPlayer()
    {
        Vector2 direction = _player.transform.position - transform.position;
        _collider.isTrigger = true;
        if (direction.magnitude > followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, Mathf.Abs(speed) * Time.deltaTime);
        }
    }
}
