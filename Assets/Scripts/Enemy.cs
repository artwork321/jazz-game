using UnityEngine;
using System.Collections.Generic; 
using System.Collections;

public class Enemy : Character
{
    public EnemyStrategy strategy;

    public IEnumerator EnemyPlayWithDelay()
    {
        yield return new WaitForSeconds(3f); // delay for 1.5 seconds
        strategy.UsePowerUps();
        strategy.SelectDie();
        PlayDiceTurn(strategy.chosenDice.gameObject);
    }

    public void SetStrategy(int enemyDifficulty) {
        if (enemyDifficulty == 1)
            strategy = new EnemyStrategyA();
        else if (enemyDifficulty == 2)
            strategy = new EnemyStrategyB();
        else
            strategy = new EnemyStrategyC();
    }

}
