using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGAttackDictionary{
    public Dictionary<string, int> attacks;

    public RPGAttackDictionary()
    {
        attacks.Add("Hit", 5);
        attacks.Add("Stab", 10);
        attacks.Add("Slash", 15);
        attacks.Add("Energy Blast", 20);
        attacks.Add("Destruction", 45);
        attacks.Add("Rest", -5);
        attacks.Add("Heal", -10);
        attacks.Add("Magic Heal", -20);
        attacks.Add("Tackle", 7);
        attacks.Add("Headbutt", 10);
        attacks.Add("Scare", 5);
        attacks.Add("Devil's Attack", 50);
        attacks.Add("Spirit Rend", 75);
        attacks.Add("Soul Rest", -5);
        attacks.Add("Angel's Heal", -50);
    }

}
