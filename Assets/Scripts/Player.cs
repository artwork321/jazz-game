using UnityEngine;
using System.Collections.Generic; 
using System; 

public class Player : MonoBehaviour
{
    [SerializeField] public string playerName; // "enemy" or "player"
    public List<Dice> playerDice;
    public GameObject playedPanel;
    public GameObject playerSlot; // Assign PlayerSlot or EnemySlot in Inspector
    public GameMan gm;

    void Start()
    {
        // Get all Dice components inside the assigned slot
        playerDice = new List<Dice>(playerSlot.GetComponentsInChildren<Dice>());
    }

    public void Forfeit()
    {
        Debug.Log(playerName + " has forfeited!");
        // TODO: Add logic for ending the game or declaring winner
    }


    public void PlayDiceTurn(GameObject dice)
    {
        dice.transform.SetParent(playedPanel.transform, false); // Move under playedPanel

        // temporarily switch turn here
        gm.SwitchTurn();
    }

}
