using UnityEngine;
using System.Collections;

public class RPGPlayer : MonoBehaviour {

    public int baseHP = 10;
    public int baseSpeed = 4;
    public int baseAttack = 6;
    public int baseDefence = 6;

    public Stats playerStats;

    public int currentHP;

    // minimum experience needed to level up
    public int baseExp;

    public int exp;

    public int currentLevel = 1;
    public int maxLevel;

    public string[] playerAttacks;

	void Start () {
        UpdateStats();
        currentHP = playerStats.HP;
	}

    private void UpdateStats()
    {
        playerStats.HP = baseHP * currentLevel;
        playerStats.speed = baseSpeed * currentLevel;
        playerStats.attack = baseAttack * currentLevel;
        playerStats.defence = baseDefence * currentLevel;
    }

    private void IncreaseLevel()
    {
        currentLevel += 1;
        if (currentLevel > maxLevel)
        {
            currentLevel = maxLevel;
        }
        UpdateStats();
    }

    public void GainExperience(int e)
    {
        int expNeeded = baseExp * currentLevel + baseHP + baseSpeed + baseAttack + baseDefence;
        exp += e;
        while (exp > expNeeded)
        {
            IncreaseLevel();
            exp = exp - expNeeded;
            expNeeded = baseExp * currentLevel + baseHP + baseSpeed + baseAttack + baseDefence;
        }
    }

    public void SetHP(int inc)
    {
        currentHP += inc;
        if (currentHP > playerStats.HP)
        {
            currentHP = playerStats.HP;
        }
        else if (currentHP <= 0)
        {
            currentHP = 0;
            //DEAD
        }
    }
}
