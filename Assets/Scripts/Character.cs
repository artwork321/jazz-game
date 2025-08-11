using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; 

public class Character : MonoBehaviour
{
    [SerializeField] public string playerName; // "Enemy {i}" or "Player"
    public List<Dice> playerDice;
    public GameObject playedPanel;
    public GameObject playerSlot;

    public GameObject enemyPlayed;
    public GameMan gm;

    // Delegate forfeit process to game manager
    public void Forfeit()
    {
        Debug.Log(playerName + " has forfeited!");
        gm.ExecuteForfeit(this);
    }

    // Play a round
    public void PlayDiceTurn(GameObject dice)
    {
        dice.transform.SetParent(playedPanel.transform, false); 
        dice.GetComponent<Button>().interactable = false;
        dice.GetComponent<Button>().onClick.RemoveAllListeners();

        playerDice.Remove(dice.GetComponent<Dice>());

        // temporarily switch turn here
        gm.SwitchTurn();
    }

    // Remove all methods or effects associated with played dice
    public void DisablePlayedDice() {
        Button[] playerPlayedDice = playedPanel.GetComponentsInChildren<Button>();

        for (int i = 0; i < playerPlayedDice.Length; i++) {
            Button btn = playerPlayedDice[i];
            btn.onClick.RemoveAllListeners();
            btn.interactable = false;
        }
    }
}
