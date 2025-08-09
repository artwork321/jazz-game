using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int playerPts = 0;
    private int opponentPts = 0;
    private int playerPtsMatch = 0;
    private int opponentPtsMatch = 0;
    private int WINNING_MATCH_SCORE = 12; // Missing semicolon fixed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetPlayerGamePts();
        resetOpponentGamePts();
        resetPlayerPtsMatch();
        resetOpponentPtsMatch();
    }

    // Update is called once per frame
    void Update()
    {
        // For testing — you can remove or replace with actual game events
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreasePlayerPtsMatch(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            IncreaseOpponentPtsMatch(1);
        }
    }

    public int IncreasePlayerPtsMatch(int bonus_pts)
    {
        playerPtsMatch += bonus_pts;
        Debug.Log("Player Match Points: " + playerPtsMatch);

        if (playerPtsMatch >= WINNING_MATCH_SCORE)
        {
            Debug.Log("Player Wins the Match!");
            resetPlayerPtsMatch();
            resetOpponentPtsMatch();
        }

        return playerPtsMatch;
    }

    public int IncreaseOpponentPtsMatch(int bonus_pts)
    {
        opponentPtsMatch += bonus_pts;
        Debug.Log("Opponent Match Points: " + opponentPtsMatch);

        if (opponentPtsMatch >= WINNING_MATCH_SCORE)
        {
            Debug.Log("Opponent Wins the Match!");
            resetPlayerPtsMatch();
            resetOpponentPtsMatch();
        }

        return opponentPtsMatch;
    }

    public void resetPlayerGamePts()
    {
        playerPts = 0;
    }

    public void resetOpponentGamePts()
    {
        opponentPts = 0;
    }

    public void resetPlayerPtsMatch()
    {
        playerPtsMatch = 0;
    }

    public void resetOpponentPtsMatch()
    {
        opponentPtsMatch = 0;
    }
}
