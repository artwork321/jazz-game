using UnityEngine;
using UnityEngine.UI;

// Increase a die value by 1
public class Upgrade : PowerUp
{
    protected override void Awake()
    {
        base.Awake();
        cost = 0;
    }

    // Activate played dice and increase their value if clicked
    protected override void ApplyEffect(Character calledPlayer)
    {

        Button[] playedDiceButtons = calledPlayer.playedPanel.GetComponentsInChildren<Button>();

        if (playedDiceButtons.Length == 0) {
            Debug.Log("No dice to upgrade");
            isUsed = false;
            return;
        }

        gm.uiManager.DisablePlayerRemainingDice(); // disable remaining dice

        for (int i = 0; i < playedDiceButtons.Length; i++)
        {
            Button btn = playedDiceButtons[i];

            btn.interactable = true;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => btn.gameObject.GetComponent<Dice>().IncreaseValue());
        }
    }
}
