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
        // forfeits if lost twice
        int lost = 0;
        int cnt = enemy.playedPanel.transform.childCount;
        for (int i = 0; i<cnt; i++)
            if (enemy.playedPanel.transform.GetChild(i).gameObject.GetComponent<Dice>().diceValue < enemy.enemyPlayed.transform.GetChild(i).gameObject.GetComponent<Dice>().diceValue) lost++;

        if (lost >= 2 && cnt != 5)
        {
            enemy.Forfeit();
        }

        int max_index = 0; int max_value = 0;
        for (int i = 0; i < enemy.playerDice.Count; i++)
        {
            Dice die = enemy.playerDice[i];
            if (die.diceValue > max_value)
            {
                max_value = die.diceValue;
                max_index = i;
            }
        }
        
        int chosenDiceIndex  = max_index;
        Dice playedDie = enemy.playerDice[chosenDiceIndex];
        
        Debug.Log(chosenDiceIndex);
        Debug.Log("Enemy chose to play {playedDie}");
        
        chosenDice = playedDie;
    }
}
