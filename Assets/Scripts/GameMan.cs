using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameMan : MonoBehaviour
{
    // game manager handles game states
    public bool playerTurn = false;
    public ScoreManager scoreManager;

    private int currGameNumber = 1;

    public GameObject dicePrefab;

    public Character player;
    public Enemy enemyPlayer;

    // gameplay-related UI components
    public GameObject foifeitButton;
    public GameObject oppTotalUI;
    public PowerUp[] skillButtons;

    private LevelMan lm;

    void Start()
    {
        NewGame();
        lm = gameObject.GetComponent<LevelMan>();
    }

    void Update()
    {

    }

    // Game = first to 12 points win the game
    // Match = the actual play with all five dice
    public void NewGame()
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

        // clear player internal dice
        player.playerDice.Clear();
        enemyPlayer.playerDice.Clear();

        // spawn player's dice
        for (int i = 0; i < 5; i++)
        {
            GameObject die = Instantiate(dicePrefab, player.playerSlot.transform);
            die.GetComponent<Dice>().isEnemy = false;
            die.GetComponent<Dice>().player = player;
            player.playerDice.Add(die.GetComponent<Dice>());
        }

        // spawn enemy's dice
        int oppTotal = 0;

        for (int i = 0; i < 5; i++)
        {
            GameObject die = Instantiate(dicePrefab, enemyPlayer.playerSlot.transform);
            Dice diceObject = die.GetComponent<Dice>();

            diceObject.isEnemy = true;
            diceObject.player = enemyPlayer;

            die.GetComponent<Button>().interactable = false;
            enemyPlayer.playerDice.Add(diceObject);

            oppTotal += diceObject.diceValue; // store total value of all dice
        }
        enemyPlayer.diceTotal = oppTotal;
        oppTotalUI.GetComponent<TextMeshProUGUI>().text = oppTotal.ToString();

        // Let Enemy start first for now
        playerTurn = false;
        EnemyTurn();
    }
    

    // Calculate points and decide who has higher points in a match
    public void EndMatch(bool isForfeit = false)
    {   

        int playerPtsRound = 0;
        int opponentPtsRound = 0;

        // Calculate scores for every round
        if (!isForfeit) {

            Dice[] enemyPlayedDice = enemyPlayer.playedPanel.GetComponentsInChildren<Dice>();
            Dice[] playerPlayedDice = player.playedPanel.GetComponentsInChildren<Dice>();


            for (int i = 0; i < 5; i++)
            {
                if (enemyPlayedDice[i].diceValue < playerPlayedDice[i].diceValue)
                {
                    playerPtsRound++;
                }
                else if (enemyPlayedDice[i].diceValue > playerPlayedDice[i].diceValue)
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
        }

        // Update score and play new match
        scoreManager.UpdateScorePanel();
        NewMatch();
        
    }

    public bool IsEndGame() {
        return scoreManager.isEnemyWinTheGame() || scoreManager.isPlayerWinTheGame();
    }


    public void EndGame() {
        if (scoreManager.isEnemyWinTheGame())
        {
            Debug.Log("Opponent Wins the Game");
            // NewGame();
            lm.RepeatLevel();
        }
        else if (scoreManager.isPlayerWinTheGame())
        {
            Debug.Log("Player Wins the Game");
            NewGame();
            lm.GoToNextLevel();
        }
    }

    public bool IsEndMatch()
    {
        // Check PlayerPlayed and EnemyPlayed each have 5 dice
        return (player.playedPanel.transform.childCount == 5 && enemyPlayer.playedPanel.transform.childCount == 5);
    }

    public void ExecuteForfeit(Character character) {
        int numberOfDicePlayed = character.playedPanel.transform.childCount;
        Character gainedChar = (character.playerName == "Player") ? enemyPlayer : player;
        
        // Give points to the opponent
        if (numberOfDicePlayed == 0) {
            scoreManager.IncreasePtsGameByCharacter(2, gainedChar);
        }
        else if (numberOfDicePlayed == 1) {
            scoreManager.IncreasePtsGameByCharacter(3, gainedChar);
        }
        else if (numberOfDicePlayed == 2) {
            scoreManager.IncreasePtsGameByCharacter(4, gainedChar);
        }
        else if (numberOfDicePlayed == 3) {
            scoreManager.IncreasePtsGameByCharacter(5, gainedChar);
        }
        else if (numberOfDicePlayed == 4) {
            scoreManager.IncreasePtsGameByCharacter(6, gainedChar);
        }

        // New Match
        EndMatch(true);        
    }


    public void SwitchTurn() {
        if (IsEndMatch())
        {
            EndMatch();
        }
        else {
            playerTurn = !playerTurn;

            if (playerTurn)
                PlayerTurn();
            else
                EnemyTurn();
        }
    }
    
    // Make player buttons interactable
    public void PlayerTurn()
    {
        Debug.Log("Player Turn!");

        foreach (Transform child in player.playerSlot.transform)
        {
            Button btn = child.GetComponent<Button>();
            btn.interactable = true;
        }

        EnablePowerUps();

        foifeitButton.GetComponent<Button>().interactable = true;
    }


    public void EnemyTurn()
    {
        Debug.Log("Enemy Turn!");

        foreach (Transform child in player.playerSlot.transform)
        {
            Button btn = child.GetComponent<Button>();
            btn.interactable = false;
        }

        DisablePowerUps();

        foifeitButton.GetComponent<Button>().interactable = false;

        // fake some thinking time
        StartCoroutine(enemyPlayer.EnemyPlayWithDelay());
    }


    public void DisablePowerUps() {
        foreach (PowerUp skill in skillButtons) {
            Button skillBtn = skill.gameObject.GetComponent<Button>();
            skillBtn.interactable = false;
        }
    }


    public void EnablePowerUps() {
        foreach (PowerUp skill in skillButtons) {
            // Only show skills that haven't been used
            if (skill.isUsed == false) {
                Button skillBtn = skill.gameObject.GetComponent<Button>();
                skillBtn.interactable = true;
            }

        }
    }
}
