using UnityEngine;
using TMPro;

public class Dice : MonoBehaviour
{
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   private GameObject playedPanel;
   public int diceValue;
   public bool isEnemy = false;
    public GameMan gm;
   void Start()
   {
      if (isEnemy)
      {
         playedPanel = GameObject.Find("EnemyPlayed");
      }
      else
      {
         playedPanel = GameObject.Find("PlayerPlayed");
      }
      gm = GameObject.Find("GameManager").GetComponent<GameMan>();
      diceValue = Random.Range(1, 7);
      transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = diceValue.ToString();


   }

   // Update is called once per frame
   void Update()
   {

   }

    public void ButtonPressed()
    {
        transform.parent = playedPanel.transform;
        gm.playerTurn = !gm.playerTurn;
        if (gm.playerTurn)
        {
            gm.PlayerTurn();
        }
        else
        {
            gm.EnemyTurn();
        }
    }
}
