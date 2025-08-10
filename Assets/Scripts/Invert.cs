using UnityEngine;
using UnityEngine.UI;

public class Invert : PowerUp
{
    protected override void Awake()
    {
        base.Awake();
        cost = 0;
    }

    protected override void ApplyEffect(Character calledPlayer)
    {

        Button[] playedDiceButtons = calledPlayer.playedPanel.GetComponentsInChildren<Button>();

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

