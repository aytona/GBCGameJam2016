using UnityEngine;
using System.Collections;

public class RPGPlayer : MonoBehaviour {

    public int baseHP = 10;
    public int baseSpeed = 4;
    public int baseAttack = 6;
    public int baseDefence = 6;

    public Stats playerStats;

    // minimum experience needed to level up
    public int baseExp;

    public int exp;

    public int currentLevel;
    public int maxLevel;

    public RPGAttacks[] playerAttacks;

	void Start () {
        UpdateStats();
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
}
