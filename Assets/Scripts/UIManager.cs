using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // gameplay-related UI components
    public GameObject foifeitButton;
    public GameObject oppTotalUI;
    public PowerUp[] skillButtons;
    public GameMan gm;

    public void ShowTotal() {
        oppTotalUI.GetComponent<TextMeshProUGUI>().text = gm.enemyPlayer.diceTotal.ToString();
    }

    public void DisablePowerUps() {
        foreach (PowerUp skill in skillButtons) {
            Button skillBtn = skill.gameObject.GetComponent<Button>();
            skillBtn.interactable = false;
        }
    }


    public void EnablePowerUps() {
        foreach (PowerUp skill in skillButtons) {
            // Only show skills that haven't been used
            if (skill.isUsed == false) {
                Button skillBtn = skill.gameObject.GetComponent<Button>();
                skillBtn.interactable = true;
            }

        }
    }

    // Remove all methods or effects associated with played dice of player
    public void DisablePlayerPlayedDice() {
        Button[] playerPlayedDice = gm.player.playedPanel.GetComponentsInChildren<Button>();

        for (int i = 0; i < playerPlayedDice.Length; i++) {
            Button btn = playerPlayedDice[i];
            btn.onClick.RemoveAllListeners();
            btn.interactable = false;
        }
    }
}
