using UnityEngine;
using System.Collections.Generic; 
using System.Collections;

// enemy strategy simple
// if more big than little, play little first to bait
// if more little than big, play big to bluff
// never forfeits
public class EnemyStrategyA : EnemyStrategy
{
    public override void SelectDie()
    {
        
        int chosenDiceIndex  = Random.Range(0, enemy.playerDice.Count);
        Dice playedDie = enemy.playerDice[chosenDiceIndex];

        Debug.Log(chosenDiceIndex);
        Debug.Log("Enemy chose to play {playedDie}");
        
        chosenDice = playedDie;
    }
}
