using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public Character player;
    public Enemy enemy;
    
    // invert dice effect
    public void InvertEffect() {
        // make played dice interactable with on click effect
        Button[] playedDiceButtons = player.playedPanel.GetComponentsInChildren<Button>();

        for (int i = 0; i < playedDiceButtons.Length; i++) {
            int idx = i; 
            Button btn = playedDiceButtons[i];
            btn.interactable = true;

            btn.onClick.RemoveAllListeners();

            btn.onClick.AddListener(() => Invert(idx));
        }
    }

    public void Invert(int dieIdx) {

        if (enemy.playedPanel.GetComponentsInChildren<Button>().Length < dieIdx) {
            Debug.Log("Cannot invert this round!");
            return;
        }

        // revert playedDice in Character and the UI
        Debug.Log(dieIdx);
        Dice playerInvertedDice = player.playedPanel.GetComponentsInChildren<Dice>()[dieIdx];
        Dice enemyInvertedDice = enemy.playedPanel.GetComponentsInChildren<Dice>()[dieIdx];

        playerInvertedDice.gameObject.transform.SetParent(enemy.playedPanel.transform, false); 
        enemyInvertedDice.gameObject.transform.SetParent(player.playedPanel.transform, false); 

        playerInvertedDice.gameObject.transform.SetSiblingIndex(dieIdx);
        enemyInvertedDice.gameObject.transform.SetSiblingIndex(dieIdx);

        // Remove Listener after selection
        playerInvertedDice.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        playerInvertedDice.gameObject.GetComponent<Button>().interactable = false;

        Button[] playedDiceButtons = player.playedPanel.GetComponentsInChildren<Button>();

        for (int i = 0; i < playedDiceButtons.Length; i++) {
            Button btn = playedDiceButtons[i];
            btn.onClick.RemoveAllListeners();
            btn.interactable = false;
        }

    }


}
