using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int playerPts = 0;
    public int opponentPts = 0;

    private int WINNING_GAME_SCORE = 12; // Missing semicolon fixed

    // UI objects
    public GameObject scorePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetPlayerGamePts();
        resetOpponentGamePts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreasePlayerPtsGame(int bonus_pts)
    {
        playerPts += bonus_pts;
        Debug.Log("Player Wins the Match and Game Points: " + playerPts);
    }

    public void IncreaseOpponentPtsGame(int bonus_pts)
    {
        opponentPts += bonus_pts;
        Debug.Log("Opponent Win the Match and Current Game Points: " + opponentPts);
    }

    public void IncreasePtsGameByCharacter(int bonus_pts, Character character) {
        if (character.playerName == "Player") {
            IncreasePlayerPtsGame(bonus_pts);
        }
        else {
            IncreaseOpponentPtsGame(bonus_pts);
        }
    }

    public bool isEnemyWinTheGame() {
        return opponentPts >= WINNING_GAME_SCORE;
    }
    
    public bool isPlayerWinTheGame() {
        return playerPts >= WINNING_GAME_SCORE;
    }
    

    public void resetPlayerGamePts()
    {
        playerPts = 0;
    }

    public void resetOpponentGamePts()
    {
        opponentPts = 0;
    }


    public void UpdateScorePanel()
    {
        Debug.Log($"Score: {playerPts} - {opponentPts}");
        scorePanel.GetComponent<TextMeshProUGUI>().text = $"Score: {playerPts} - {opponentPts}";
    }
}
