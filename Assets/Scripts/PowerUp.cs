using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUp : MonoBehaviour
{
    protected int cost;
    protected GameMan gm;

    protected virtual void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameMan>();
    }

    public void ActivatePowerUp(Character calledPlayer)
    {
        if (!CanActivate(calledPlayer)) return;

        DeductCost(calledPlayer);
        ApplyEffect(calledPlayer);
    }

    protected bool CanActivate(Character calledPlayer)
    {
        int availablePts = calledPlayer.playerName == "Player" ? gm.scoreManager.playerPts : gm.scoreManager.opponentPts;

        if (availablePts < cost)
        {
            Debug.Log("Not enough points to use this power-up");
            return false;
        }
        return true;
    }

    protected void DeductCost(Character calledPlayer)
    {
        gm.scoreManager.IncreasePtsGameByCharacter(-cost, calledPlayer);
        gm.scoreManager.UpdateScorePanel();
    }

    // Child classes implement this to do their specific effect
    protected abstract void ApplyEffect(Character calledPlayer);
}

