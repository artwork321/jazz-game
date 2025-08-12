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

    public void HideTotal() {
        oppTotalUI.GetComponent<TextMeshProUGUI>().text = "?";
    }

    public void DisableForfeit() {
        foifeitButton.GetComponent<Button>().interactable = false;
    }

    public void EnableForfeit() {
        foifeitButton.GetComponent<Button>().interactable = true;
    }


    public void DisablePowerUps() {
        foreach (PowerUp skill in skillButtons) {
            Button skillBtn = skill.gameObject.GetComponent<Button>();
            skillBtn.interactable = false;
        }
    }

    public void EnablePowerUps() {
        foreach (PowerUp skill in skillButtons) {
            Button skillBtn = skill.gameObject.GetComponent<Button>();

            // Only show skills that haven't been used
            if (skill.isUsed == false) 
                skillBtn.interactable = true;
            else
                skillBtn.interactable = false;
        }
    }

    public void ResetIsUsed(int level)
    {
        int disable = Mathf.Max(0, 3 - level);
        foreach (PowerUp skill in skillButtons)
        {
            Button skillBtn = skill.gameObject.GetComponent<Button>();
            skill.isUsed = false;
            skillBtn.interactable = true;
            skillBtn.gameObject.SetActive(true);
        }
        foreach (PowerUp skill in skillButtons)
        {
            if (disable == 0) break;
            Button skillBtn = skill.gameObject.GetComponent<Button>();
            skill.isUsed = true;
            skillBtn.gameObject.SetActive(false);
            disable -= 1;
        }
    }

    // Remove all methods or effects associated with played dice of player
    public void DisablePlayerPlayedDice()
    {
        Button[] playerPlayedDice = gm.player.playedPanel.GetComponentsInChildren<Button>();

        for (int i = 0; i < playerPlayedDice.Length; i++)
        {
            Button btn = playerPlayedDice[i];
            btn.onClick.RemoveAllListeners();
            btn.interactable = false;
        }
    }

    // Remove all methods or effects associated with played dice of enemy
    public void DisableEnemyPlayedDice() {
        Button[] playerPlayedDice = gm.enemyPlayer.playedPanel.GetComponentsInChildren<Button>();

        for (int i = 0; i < playerPlayedDice.Length; i++) {
            Button btn = playerPlayedDice[i];
            btn.onClick.RemoveAllListeners();
            btn.interactable = false;
        }
    }

    public void DisablePlayerRemainingDice() {
        foreach (Dice die in gm.player.playerDice) {
            Button btn = die.gameObject.GetComponent<Button>();
            btn.interactable = false;
        }
    }

    
    public void EnablePlayerRemainingDice() {
        foreach (Dice die in gm.player.playerDice) {
            Button btn = die.gameObject.GetComponent<Button>();
            btn.interactable = true;
        }
    }

    public void RemoveAllDiceListeners() {
        foreach (Dice die in gm.player.playerDice) {
            die.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void AddDicePlayListener() {
        foreach (Dice die in gm.player.playerDice) {
            die.gameObject.GetComponent<Button>().onClick.AddListener(die.ButtonPressed);
        }
    }
}
