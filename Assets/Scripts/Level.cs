using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Player human; 
    [SerializeField] private Player enemy; 
    [SerializeField] private GameMan gameManager; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Example: set up the starting player
        gameManager.playerTurn = true;
        gameManager.PlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        // Could be used for debug key testing
        if (Input.GetKeyDown(KeyCode.F))
        {
            human.Forfeit();
        }
    }
}
