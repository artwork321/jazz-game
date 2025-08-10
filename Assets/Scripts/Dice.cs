using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    private GameObject playedPanel;
    public int diceValue;
    public bool isEnemy = false;
    public Character player; // Owner of the die

    void Awake()
    {
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

    public void ButtonPressed()
    {
        player.PlayDiceTurn(gameObject); // Pass clicked dice to Player script
    }

    public void InvertDice(int dieIdx, Enemy enemy, Character player) {

        // swap playedDice in Character and the UI
        Debug.Log(dieIdx);
        Dice playerInvertedDice = player.playedPanel.GetComponentsInChildren<Dice>()[dieIdx];
        Dice enemyInvertedDice = enemy.playedPanel.GetComponentsInChildren<Dice>()[dieIdx];

        playerInvertedDice.gameObject.transform.SetParent(enemy.playedPanel.transform, false); 
        enemyInvertedDice.gameObject.transform.SetParent(player.playedPanel.transform, false); 

        playerInvertedDice.gameObject.transform.SetSiblingIndex(dieIdx);
        enemyInvertedDice.gameObject.transform.SetSiblingIndex(dieIdx);

        playerInvertedDice.isEnemy = true;
        enemyInvertedDice.isEnemy = false;

        player.DisablePlayedDice();
        enemy.DisablePlayedDice();
    }

    public void IncreaseValue() {
        diceValue += 1;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = diceValue.ToString();

        player.DisablePlayedDice();
    }

    public void RerollValue() {
        diceValue = Random.Range(1, 7);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = diceValue.ToString();

        player.DisablePlayedDice();
    }
}
