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

    public IEnumerator EnemyPlayWithDelay()
    {
        yield return new WaitForSeconds(3f); // delay for 1.5 seconds
        PlayDiceTurn(); // your enemy logic here
    }
}
