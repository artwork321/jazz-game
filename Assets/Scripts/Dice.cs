using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    private GameObject playedPanel;
    public int diceValue;
    public bool isEnemy = false;
    public Character player; // Owner of the die
    private GameMan gm;

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

        if (!isEnemy)
            gameObject.GetComponent<Button>().onClick.AddListener(ButtonPressed);

    }

    // Called when a die is clicked then let's player handle the action
    public void ButtonPressed()
    {
        player.PlayDiceTurn(gameObject);
    }


    // Swap dice at idx in played dice from both players
    public void InvertDice(int dieIdx, Enemy enemy, Character player) {

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
    }

    // Increase die's value by 1
    public void IncreaseValue() {
        diceValue += 1;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = diceValue.ToString();

        gm.uiManager.DisablePlayerPlayedDice();
    }

    // Reroll the die's value
    public void RerollValue() {
        diceValue = Random.Range(1, 7);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = diceValue.ToString();

        // remove this listener
        gameObject.GetComponent<Button>().onClick.RemoveListener(RerollValue);

        // add play listener
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }
}
