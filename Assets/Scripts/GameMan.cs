using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour
{
    // game manager handles game states??
    public bool playerTurn = false;
    private int playerPts = 0;
    private int opponentPts = 0;

    // depending on how many games we are expecting to play
    private int currGameNumber = 1;

    public GameObject dicePrefab;
    public GameObject playerSlot;
    public GameObject playedPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playedPanel = GameObject.Find("PlayerPlayed");
        playerSlot = GameObject.Find("PlayerSlot");
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // game is the whole thing, so first to 12 points win the game
    // match is the actual thing where you start with all five of your dice and play it
    // 
    void NewGame()
    {
        // maybe begins a new scene could be some animations to signify it being a new game  
        playerPts = 0;
        opponentPts = 0;
        NewMatch();
    }
    void NewMatch()
    {
        // return dice to each player
        // randomly assign values to each of the die
        foreach (Transform child in playedPanel.transform) Destroy(child.gameObject);
        foreach (Transform child in playerSlot.transform) Destroy(child.gameObject);
        for (int i = 0; i < 5; i++)
        {
            GameObject die = Instantiate(dicePrefab, Vector3.zero, Quaternion.identity);
            die.transform.parent = playerSlot.transform;
        }

        playerTurn = false;
        EnemyTurn();

    }

    public void PlayerTurn()
    {
        // set player dice to interactable
        foreach (Transform child in playerSlot.transform)
        {
            child.gameObject.GetComponent<Button>().interactable = true;
        }
    }


    public void EnemyTurn()
    {

        foreach (Transform child in playerSlot.transform)
        {
            child.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    void Forfeit()
    {

    }

    void EndRound()
    {

    }
 
}
