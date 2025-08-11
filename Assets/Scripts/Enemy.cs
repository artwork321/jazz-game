using UnityEngine;
using System.Collections.Generic; 
using System.Collections;

public class Enemy : Character
{
    public int diceTotal;

    public void PlayDiceTurn()
    {
        int chosenDiceIndex  = Random.Range(0, playerDice.Count);
        Dice playedDice = playerDice[chosenDiceIndex];

        Debug.Log(chosenDiceIndex);
        Debug.Log("Enemy chose to play {playedDice}");
        
        PlayDiceTurn(playedDice.gameObject);
    }


    // enemy strategy simple
    // if more big than little, play little first to bait
    // if more little than big, play big to bluff.
    // never forfeits, and never uses powerups
    public void PlayDiceTurnB()
    {
        int bigCount = 0;
        foreach (Dice die in playerDice)
            if (die.diceValue > 3) bigCount++;

        bool playBig = bigCount <= playerDice.Count / 2; 

        List<Dice> candidates = playerDice.FindAll(d => playBig ? d.diceValue > 3 : d.diceValue <= 3);

        if (candidates.Count == 0) candidates = playerDice;

        Dice chosenDie = candidates[Random.Range(0, candidates.Count)];

        Debug.Log($"Enemy chose to play {chosenDie.diceValue}");
        PlayDiceTurn(chosenDie.gameObject);
    }

    // same as play dice turn b but also forfeits when they have bad dice at the start.
    // still no powerups
    public void PlayDiceTurnC()
    {
        int bigCount = 0;
        int bigCountPlayed = 0;
        int lost = 0;
        foreach (Dice die in playerDice)
            if (die.diceValue > 3) bigCount++;

        for (int i = 0; i < playedPanel.transform.childCount; i++)
        {
            if (child.gameObject.GetComponent<Dice>().diceValue > 3) bigCountPlayed++;
            if (playedPanel.transform.GetChild(i).gameObject.GetComponent<Dice>().diceValue < enemyPlayed.transform.GetChild(i).gameObject.GetComponent<Dice>().diceValue) lost++;
        }

        bool playBig = bigCount <= playerDice.Count / 2;

        if (bigCount + bigCountPlayed < 2) Forfeit();
        if (lost == 2) Forfeit();

        List<Dice> candidates = playerDice.FindAll(d => playBig ? d.diceValue > 3 : d.diceValue <= 3);

        if (candidates.Count == 0) candidates = playerDice;

        Dice chosenDie = candidates[Random.Range(0, candidates.Count)];

        Debug.Log($"Enemy chose to play {chosenDie.diceValue}");
        PlayDiceTurn(chosenDie.gameObject);
    }

    public IEnumerator EnemyPlayWithDelay()
    {
        yield return new WaitForSeconds(1.5f); // delay for 1.5 seconds
        PlayDiceTurnC();
    }
}
