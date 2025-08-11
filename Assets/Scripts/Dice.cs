using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class Dice : MonoBehaviour
{
    private GameObject playedPanel;
    public int diceValue;
    public bool isEnemy = false;
    public Character player; // Owner of the die
    private GameMan gm;


    public List<Sprite> diceFaces;
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameMan>();

        if (isEnemy)
        {
            playedPanel = GameObject.Find("EnemyPlayed");
            player = GameObject.Find("Enemy").GetComponent<Enemy>(); // Added GetComponent
        }
        else
        {
            playedPanel = GameObject.Find("PlayerPlayed");
            player = GameObject.Find("Player").GetComponent<Character>();
        }

        // Assign random dice value
        diceValue = Random.Range(1, 7);

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = diceValue.ToString();
        GetComponent<Image>().sprite = diceFaces[diceValue - 1];
        if (!isEnemy)
            gameObject.GetComponent<Button>().onClick.AddListener(ButtonPressed);

    }

    // Called when a die is clicked then let's player handle the action
    public void ButtonPressed()
    {
        player.PlayDiceTurn(gameObject);
    }


    // Swap dice at idx in played dice from both players
    public void InvertDice(int dieIdx) {

        Character player = gm.player;
        Enemy enemy = gm.enemyPlayer;

        Debug.Log(dieIdx);
        Dice playerInvertedDice = player.playedPanel.GetComponentsInChildren<Dice>()[dieIdx];
        Dice enemyInvertedDice = enemy.playedPanel.GetComponentsInChildren<Dice>()[dieIdx];

        playerInvertedDice.gameObject.transform.SetParent(enemy.playedPanel.transform, false); // Move UI
        enemyInvertedDice.gameObject.transform.SetParent(player.playedPanel.transform, false); 

        playerInvertedDice.gameObject.transform.SetSiblingIndex(dieIdx); // Ensure order
        enemyInvertedDice.gameObject.transform.SetSiblingIndex(dieIdx);

        playerInvertedDice.isEnemy = true; // Update property
        enemyInvertedDice.isEnemy = false;

        gm.uiManager.DisablePlayerPlayedDice(); // Disable effect after finish
        gm.uiManager.DisableEnemyPlayedDice();
        gm.uiManager.EnablePlayerRemainingDice();
    }

    // Increase die's value by 1
    public void IncreaseValue() {
        diceValue += 1;
        GetComponent<Image>().sprite = diceFaces[diceValue - 1];
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = diceValue.ToString();

        gm.uiManager.DisablePlayerPlayedDice();
        gm.uiManager.EnablePlayerRemainingDice();
    }

    // Reroll the die's value
    public void RerollValue() {
        diceValue = Random.Range(1, 7);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = diceValue.ToString();

        GetComponent<Image>().sprite = diceFaces[diceValue - 1];
        // remove this listener
        gm.uiManager.RemoveAllDiceListeners();

        // add play listener
        gm.uiManager.AddDicePlayListener();
    }
}
