using UnityEngine;
using System.Collections.Generic; 
using System.Collections;

public class EnemyStrategy
{
    [SerializeField] public Dice chosenDice;
    [SerializeField] public Enemy enemy;

    protected bool isUseInvert = false;
    protected bool isUseUpgrade = false;
    protected bool isUseReroll = false;

    protected List<int> enemyPowerups; // store chosen powerups for the match
    protected int powerupsUsed = 0;

    public EnemyStrategy()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        enemyPowerups = new List<int>();
    }

    public virtual void SelectDie()
    {
        Debug.Log("Base strategy: To be implemented");
    }

    public virtual void UsePowerUps()
    {
        Debug.Log("Base strategy: No powerups used");
    }
}

