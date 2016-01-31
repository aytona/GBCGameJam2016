using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPGController : MonoBehaviour {
    private RPGBattle _battle;
    private RPGPlayer _player;
    private RPGEnemy _enemy;

    private RPGAttackDictionary _attacks;

    public Text[] playerAttackText;
    public Text[] enemyAttackText;

    public Text textBox;
    public Text playerHealth, enemyHealth;

    private bool canAttack = true;

	void Start () {
        _battle = FindObjectOfType<RPGBattle>();
        _player = _battle._player;
        _enemy = _battle._enemy;
	}
	
	// Update is called once per frame
	void Update () {
        DisplayText();
	}

    public void PlayerAttack(int aNum)
    {
        if (canAttack)
        {
            if (aNum < _player.currentLevel)
            {
                int damage;
                string key = _player.playerAttacks[aNum];
                _attacks.attacks.TryGetValue(key, out damage);
                if (damage > 0)
                {
                    damage += _player.playerStats.HP;
                    _enemy.SetHP(-damage);
                    canAttack = false;
                    StartCoroutine(WaitForEnemyAttack());
                    textBox.text = "The Player uses " + _player.playerAttacks[aNum] + " and attacks the enemy for " + damage + " damage.";
                }
                else
                {
                    _player.SetHP(-damage);
                    canAttack = false;
                    StartCoroutine(WaitForEnemyAttack());
                    textBox.text = "The Player uses " + _player.playerAttacks[aNum] + " and heals " + damage + " HP.";
                }
            }
        }
    }

    private IEnumerator WaitForEnemyAttack()
    {
        yield return new WaitForSeconds(2);
        int randomAttack = Random.Range(0, _enemy.enemyAttacks.Length);
        EnemyAttack(randomAttack);
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }

    public void EnemyAttack(int aNum)
    {
        int damage;
        _attacks.attacks.TryGetValue(_enemy.enemyAttacks[aNum], out damage);
        if (damage > 0)
        {
            damage += _enemy.enemyStats.HP;
            _player.SetHP(-damage);
            textBox.text = "The Enemy uses " + _enemy.enemyAttacks[aNum] + " and attacks the player for " + damage + " damage.";
        }
        else
        {
            _enemy.SetHP(-damage);
            textBox.text = "The Enemy uses " + _enemy.enemyAttacks[aNum] + " and heals " + damage + " HP.";
        }
    }

    private void DisplayText()
    {
        for (int i = 0; i < playerAttackText.Length; i++)
        {
            if (i < _player.currentLevel)
            {
                playerAttackText[i].text = _player.playerAttacks[i];
            }
        }
        for (int i = 0; i < enemyAttackText.Length; i++)
        {
            enemyAttackText[i].text = _enemy.enemyAttacks[i];
        }
        playerHealth.text = "Player health: " + _player.currentHP + "/" + _player.playerStats.HP;
        enemyHealth.text = "Enemy health: " + _enemy.currentHP + "/" + _enemy.enemyStats.HP;
    }

}
