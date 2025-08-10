using UnityEngine;
using TMPro;

public class Dice : MonoBehaviour
{
    private GameObject playedPanel;
    public int diceValue;
    public bool isEnemy = false;
    public Character player;

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

        // Update dice display (assumes first child has a TextMeshProUGUI component)
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = diceValue.ToString();
    }

    public void ButtonPressed()
    {
        player.PlayDiceTurn(gameObject); // Pass clicked dice to Player script
    }
}
