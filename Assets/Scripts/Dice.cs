using UnityEngine;

public class Dice : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject playedPanel;
    void Start()
    {
        playedPanel = GameObject.Find("PlayerPlayed");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPressed()
    {
        transform.parent = playedPanel.transform;
    }
}
