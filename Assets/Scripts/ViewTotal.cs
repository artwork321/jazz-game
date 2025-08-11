using UnityEngine;
using UnityEngine.UI;

// Increase a die value by 1
public class ViewTotal : PowerUp
{
    protected override void Awake()
    {
        base.Awake();
        cost = 0;
    }

    // Show Enemy total value of dice
    protected override void ApplyEffect(Character calledPlayer)
    {
        gm.uiManager.ShowTotal();

    }
}
