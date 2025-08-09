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

    private int playerPtsRound = 0;
    private int opponentPtsRound = 0;

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
        NewMatch();
    }

    void NewMatch()
    {
        playerPtsRound = 0;
        opponentPtsRound = 0;

        // reset match score
        scoreManager.resetPlayerPtsMatch();
        scoreManager.resetOpponentPtsMatch();

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
        if (IsEndRound())
        {
            DecideWinnerRounds();
            DecideWinnerMatch();
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
    }

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
    }

    public void DecideWinnerRounds()
    {
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
    }

    public void DecideWinnerMatch()
    {
        if (playerPtsRound > opponentPtsRound)
        {
            scoreManager.IncreasePlayerPtsMatch(6);
        }
        else if (playerPtsRound < opponentPtsRound)
        {
            scoreManager.IncreaseOpponentPtsMatch(6);
        }
    }

    public void PlayMatch(Player opponent, Player human)
    {
        NewMatch();

        if (IsEndRound())
        {
            DecideWinnerRounds();
            DecideWinnerMatch();
        }
    }

    public bool IsEndRound()
    {
        // Check PlayerPlayed and EnemyPlayed each have 5 dice
        return (player.playedPanel.transform.childCount == 5 && enemyPlayer.playedPanel.transform.childCount == 5);
    }
}
