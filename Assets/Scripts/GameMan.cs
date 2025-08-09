using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameMan : MonoBehaviour
{
    // game manager handles game states
    public bool playerTurn = false;
    public ScoreManager scoreManager;

    private int currGameNumber = 1;

    public GameObject dicePrefab;

    public Player player;
    public Player enemyPlayer;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        NewGame();
    }

    void Update()
    {

    }

    // Game = first to 12 points win the game
    // Match = the actual play with all five dice
    void NewGame()
    {
        scoreManager.resetPlayerGamePts();
        scoreManager.resetOpponentGamePts();
        scoreManager.UpdateScorePanel();
        NewMatch();
    }

    void NewMatch()
    {
        // Check if end game
        if (IsEndGame()) {
            EndGame();
        }

        // clear played dice
        foreach (Transform child in player.playedPanel.transform) Destroy(child.gameObject);
        foreach (Transform child in enemyPlayer.playedPanel.transform) Destroy(child.gameObject);

        // clear slots
        foreach (Transform child in player.playerSlot.transform) Destroy(child.gameObject);
        foreach (Transform child in enemyPlayer.playerSlot.transform) Destroy(child.gameObject);

        // spawn player's dice
        for (int i = 0; i < 5; i++)
        {
            GameObject die = Instantiate(dicePrefab, player.playerSlot.transform);
            die.GetComponent<Dice>().isEnemy = false;
            die.GetComponent<Dice>().player = player;
        }

        // spawn enemy's dice
        for (int i = 0; i < 5; i++)
        {
            GameObject die = Instantiate(dicePrefab, enemyPlayer.playerSlot.transform);
            die.GetComponent<Dice>().isEnemy = true;
            die.GetComponent<Dice>().player = enemyPlayer;
        }

        // Let Enemy start first for now
        playerTurn = false;
        EnemyTurn();
    }

    public void SwitchTurn() {
        if (IsEndMatch())
        {
            EndMatch();
        }
        else {
            playerTurn = !playerTurn;

            if (playerTurn)
            {
                PlayerTurn();
            }
            else
            {
                EnemyTurn();
            }
        }
    }
    
    // Make player button interactable
    public void PlayerTurn()
    {
        foreach (Transform child in player.playerSlot.transform)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = true;
            }
        }

        foreach (Transform child in enemyPlayer.playerSlot.transform)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = false;
            }
        }
    }

    // Disable player button's interaction
    public void EnemyTurn()
    {
        foreach (Transform child in player.playerSlot.transform)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = false;
            }
        }

        foreach (Transform child in enemyPlayer.playerSlot.transform)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = true;
            }
        }
    }
    

    // Calculate points and decide who has higher points in a match
    public void EndMatch()
    {
        int playerPtsRound = 0;
        int opponentPtsRound = 0;

        for (int i = 0; i < 5; i++)
        {
            if (enemyPlayer.playerDice[i].diceValue < player.playerDice[i].diceValue)
            {
                playerPtsRound++;
            }
            else if (enemyPlayer.playerDice[i].diceValue > player.playerDice[i].diceValue)
            {
                opponentPtsRound++;
            }
        }

        if (playerPtsRound > opponentPtsRound)
        {
            scoreManager.IncreasePlayerPtsGame(6);
        }
        else if (playerPtsRound < opponentPtsRound)
        {
            scoreManager.IncreaseOpponentPtsGame(6);
        }

        scoreManager.UpdateScorePanel();

        NewMatch();
    }

    public bool IsEndGame() {
        return scoreManager.isEnemyWinTheGame() || scoreManager.isPlayerWinTheGame();
    }


    public void EndGame() {
        if (scoreManager.isEnemyWinTheGame()) {
            Debug.Log("Opponent Wins the Game");
            NewGame();
        }
        else if (scoreManager.isPlayerWinTheGame()) {
            Debug.Log("Player Wins the Game");
            NewGame();
        }
    }

    public bool IsEndMatch()
    {
        // Check PlayerPlayed and EnemyPlayed each have 5 dice
        return (player.playedPanel.transform.childCount == 5 && enemyPlayer.playedPanel.transform.childCount == 5);
    }
}
