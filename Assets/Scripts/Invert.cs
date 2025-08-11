using UnityEngine;
using UnityEngine.UI;

public class Invert : PowerUp
{
    protected override void Awake()
    {
        base.Awake();
        cost = 0;
    }

    // Activate played dice and invert dice of a column if clicked
    protected override void ApplyEffect(Character calledPlayer)
    {

        Button[] playedDiceButtons = calledPlayer.playedPanel.GetComponentsInChildren<Button>();

        if (playedDiceButtons.Length == 0) {
            Debug.Log("No dice to invert");
            isUsed = false;
            return;
        }

        gm.uiManager.DisablePlayerRemainingDice(); // disable remaining dice

        for (int i = 0; i < playedDiceButtons.Length; i++)
        {
            int idx = i;
            Button btn = playedDiceButtons[i];

            btn.interactable = true;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => btn.gameObject.GetComponent<Dice>().InvertDice(idx, gm.enemyPlayer, gm.player));
        }
    }
}

