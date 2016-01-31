using UnityEngine;
using System.Collections;

public class RPGEnemy : MonoBehaviour {

    public int minBaseHP = 5;
    public int maxBaseHP = 8;
    public int minBaseSpeed = 2;
    public int maxBaseSpeed = 5;
    public int minBaseAttack = 3;
    public int maxBaseAttack = 6;
    public int minBaseDefence = 3;
    public int maxBaseDefence = 6;

    public Stats enemyStats;

    public int currentHP;

    public int minLevel, maxLevel;

    public int currentLevel;

    public string[] enemyAttacks;

	// Use this for initialization
	void Start () {
        SetStats();
        
	}

    private void SetStats()
    {
        currentLevel = Random.Range(minLevel, maxLevel);
        enemyStats.HP = Random.Range(minBaseHP, maxBaseHP) * currentLevel;
        enemyStats.speed = Random.Range(minBaseSpeed, maxBaseSpeed) * currentLevel;
        enemyStats.attack = Random.Range(minBaseAttack, maxBaseAttack) * currentLevel;
        enemyStats.defence = Random.Range(minBaseDefence, maxBaseDefence) * currentLevel;
        currentHP = enemyStats.HP;
    }

    public void SetHP(int inc)
    {
        currentHP += inc;
        if (currentHP > enemyStats.HP)
        {
            currentHP = enemyStats.HP;
        }
        else if (currentHP <= 0)
        {
            currentHP = 0;
            //DEAD
        }
    }
}
