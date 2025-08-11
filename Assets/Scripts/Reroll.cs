using UnityEngine;
using UnityEngine.UI;

// Increase a die value by 1
public class Reroll : PowerUp
{
    protected override void Awake()
    {
        base.Awake();
        cost = 0;
    }

    // Activate remaining dice and reroll the selected one
    protected override void ApplyEffect(Character calledPlayer)
    {
        for (int i = 0; i < calledPlayer.playerDice.Count; i++)
        {
            Dice die = calledPlayer.playerDice[i];
            Button btn = die.gameObject.GetComponent<Button>(); 

            btn.onClick.RemoveAllListeners(); // remove playing listener
            die.gameObject.GetComponent<Button>().onClick.AddListener(die.RerollValue); // Add play dice listener later
        }
    }
}
