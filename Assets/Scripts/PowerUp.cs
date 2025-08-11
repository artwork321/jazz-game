using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUp : MonoBehaviour
{
    public int cost;
    protected GameMan gm;
    public bool isUsed = false;

    protected virtual void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameMan>();
    }

    // Called when a power up is clicked on
    public void ActivatePowerUp(Character calledPlayer)
    {
        if (!CanActivate(calledPlayer)) return;

        gm.uiManager.DisablePowerUps(); // Disable all powerups if one has been selected
        DeductCost(calledPlayer); // Execute the effect
        ApplyEffect(calledPlayer);
    }

    // Allow players to use if players have enough point or the skill hasn't been used
    protected bool CanActivate(Character calledPlayer)
    {
        int availablePts = calledPlayer.playerName == "Player" ? gm.scoreManager.playerPts : gm.scoreManager.opponentPts;

        if (availablePts < cost || isUsed==true)
        {
            Debug.Log("Not enough points to use this power-up or Already Used");
            return false;
        }
        
        isUsed = true;
        return true;
    }

    // Reduce player's points according to the power up cost
    protected void DeductCost(Character calledPlayer)
    {
        gm.scoreManager.IncreasePtsGameByCharacter(-cost, calledPlayer);
        gm.scoreManager.UpdateScorePanel();
    }

    // Child classes implement this to do their specific effect
    protected abstract void ApplyEffect(Character calledPlayer);
}

