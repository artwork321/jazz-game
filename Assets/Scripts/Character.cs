using UnityEngine;
using System.Collections.Generic; 
using System; 

public class Character : MonoBehaviour
{
    [SerializeField] public string playerName; // "Enemy {i}" or "Player"
    public List<Dice> playerDice;
    public GameObject playedPanel;
    public GameObject playerSlot;
    public GameMan gm;


    public void Forfeit()
    {
        Debug.Log(playerName + " has forfeited!");
        gm.ExecuteForfeit(this);
    }


    public void PlayDiceTurn(GameObject dice)
    {
        dice.transform.SetParent(playedPanel.transform, false); 
        playerDice.Remove(dice.GetComponent<Dice>());

        // temporarily switch turn here
        gm.SwitchTurn();
    }

    public void PlayCardTurn() {

    }
}
