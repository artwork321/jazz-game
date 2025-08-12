using UnityEngine;
using System.Collections.Generic; 
using System.Collections;

// enemy strategy simple
// if more big than little, play little first to bait
// if more little than big, play big to bluff
// never forfeits
public class EnemyStrategyB : EnemyStrategy
{

    public override void UsePowerUps() {
        ChoosePowerupsForMatch();
        
        Dice[] playedDice = enemy.playedPanel.GetComponentsInChildren<Dice>();
        Dice[] playerPlayedDice = enemy.enemyPlayed.GetComponentsInChildren<Dice>();


        // Try to use powerups first (in order chosen at match start)
        if (powerupsUsed < enemyPowerups.Count)
        {
            foreach (int powerup in enemyPowerups)
            {
                if (TryUsePowerup(powerup, playedDice, playerPlayedDice))
                {
                    powerupsUsed++;
                    break; // only use one powerup per turn
                }
            }
        }
    }

    private void ChoosePowerupsForMatch()
    {
        List<int> all = new List<int> { 1, 2, 3 }; // 1 = Reroll, 2 = Upgrade, 3 = Invert
        enemyPowerups = new List<int>();

        // Pick 2 distinct ones
        for (int i = 0; i < 2; i++)
        {
            int pick = all[Random.Range(0, all.Count)];
            enemyPowerups.Add(pick);
            all.Remove(pick);
        }

        powerupsUsed = 0;
    }

    public override void SelectDie()
    {

        // Then choose dice to play
        int bigCount = 0;
        foreach (Dice die in enemy.playerDice)
            if (die.diceValue > 3) bigCount++;

        bool playBig = bigCount <= enemy.playerDice.Count / 2;
        List<Dice> candidates = enemy.playerDice.FindAll(d => playBig ? d.diceValue > 3 : d.diceValue <= 3);

        if (candidates.Count == 0) candidates = enemy.playerDice;
        int lost = 0;
        int cnt = enemy.playedPanel.transform.childCount;
        for (int i = 0; i < cnt; i++)
            if (enemy.playedPanel.transform.GetChild(i).gameObject.GetComponent<Dice>().diceValue < enemy.enemyPlayed.transform.GetChild(i).gameObject.GetComponent<Dice>().diceValue) lost++;

        if (lost >= 2 && cnt != 5)
        {
            enemy.Forfeit();
        }

        Dice playedDie = candidates[Random.Range(0, candidates.Count)];
        Debug.Log($"Enemy chose to play {playedDie.diceValue}");
        chosenDice = playedDie;
    }


    // Ensure enemy only uses powerups if they bring advantages
    private bool TryUsePowerup(int type, Dice[] playedDice, Dice[] playerPlayedDice)
    {
        // Only reroll a low-value die
        if (type == 1 && !isUseReroll && enemy.gm.scoreManager.opponentPts >= 0)
        {
            foreach (Dice die in enemy.playerDice)
            {
                if (die.diceValue < 3)
                {
                    isUseReroll = true;
                    die.RerollValue();
                    return true;
                }
            }
        }
        // Only increase a played die's value if the value < opponent
        else if (type == 2 && !isUseUpgrade && enemy.gm.scoreManager.opponentPts >= 0)
        {
            for (int i = 0; i < playedDice.Length; i++)
            {
                if (playedDice[i].diceValue == playerPlayedDice[i].diceValue ||
                    playedDice[i].diceValue == playerPlayedDice[i].diceValue - 1)
                {
                    isUseUpgrade = true;
                    playedDice[i].IncreaseValue();
                    return true;
                }
            }
        }
        // Only invert col that has player die value > enemy
        else if (type == 3 && !isUseInvert && enemy.gm.scoreManager.opponentPts >= 0)
        {
            for (int i = 0; i < playedDice.Length; i++)
            {
                if (playedDice[i].diceValue < playerPlayedDice[i].diceValue)
                {
                    isUseInvert = true;
                    playedDice[i].InvertDice(i);
                    return true;
                }
            }
        }

        return false; // couldn’t use this powerup this turn
    }
}
