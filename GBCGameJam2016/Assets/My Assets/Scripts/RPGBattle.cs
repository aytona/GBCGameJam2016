using UnityEngine;
using System.Collections;

public class RPGBattle : MonoBehaviour {

    public RPGPlayer _player;
    public RPGEnemy _enemy;

	void Awake () {
        DontDestroyOnLoad(transform.gameObject);
	}

    public void SetEnemy(RPGEnemy e)
    {
        if (transform.childCount > 0)
        {
            transform.DetachChildren();
        }
        _enemy = e;
        e.transform.parent = transform;
        Application.LoadLevel(0);
    }
}
