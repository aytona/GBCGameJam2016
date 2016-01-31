using UnityEngine;
using System.Collections;

public enum DebuffType
{
    HP, Speed, Attack, Defence
}

public class RPGAttacks {
    public string attackName;
    public int attackDamage;
    public float debuff;
    public DebuffType playerDebuff;
    public DebuffType enemyDebuff;
    public int targetNum;
}
