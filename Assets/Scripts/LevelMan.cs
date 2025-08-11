using System.Collections.Generic;
using UnityEngine;

public class LevelMan : MonoBehaviour
{
    public List<Level> levels;
    private int currentLevelIndex = -1;
    public GameMan gm;

    public void Start()
    {
        gm = gameObject.GetComponent<GameMan>();
    }

    public void StartNewLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
        {
            // either we start boss level -> maybe we put this on a new scene or smth im not sure
            // or this will trigger the end of the game maybe.
            return;
        }

        currentLevelIndex = levelIndex;
        Level level = levels[levelIndex];

        // Example: Play music for this level
        /*
        if (level.bgMusic != null)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.clip = level.bgMusic;
                audioSource.Play();
            }
        }
        */

        // Trigger dialogue
        FindObjectOfType<Dialogue>().PlayLine(level.dialogueIndex);
        gm.NewGame();
    }

    public void GoToNextLevel()
    {
        StartNewLevel(currentLevelIndex + 1);
    }

    public void RepeatLevel()
    {
        StartNewLevel(currentLevelIndex);
    }

    public Level GetCurrentLevel()
    {
        if (currentLevelIndex >= 0 && currentLevelIndex < levels.Count)
            return levels[currentLevelIndex];
        return null;
    }
}

[System.Serializable]
public class Level
{
    public int lvl;            // level number
    public int dialogueIndex;  // starting dialogue index
    public AudioClip bgMusic;  // background music

    // can also have more in regards to which powerups are available and stuff.
}

