using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class LevelMan : MonoBehaviour
{
    public List<Level> levels;
    private int currentLevelIndex = 0;
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;
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
        }
        StartCoroutine(LevelTransition(levelIndex));
    }

    private IEnumerator LevelTransition(int levelIndex)
    {
        // can add the tutorial for new powerups here
        fadePanel.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Let's play level " + (levelIndex + 1).ToString();
        yield return StartCoroutine(FadeOut());


        currentLevelIndex = levelIndex;
        Level level = levels[levelIndex];

        // Example: Play music for this level

        if (level.bgMusic != null)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.clip = level.bgMusic;
                audioSource.Play();
            }
        }


        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeIn());
        gm.NewGame();
        // Trigger dialogue
        FindObjectOfType<Dialogue>().PlayLine(level.dialogueIndex);
    }

    private IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadePanel.alpha = t / fadeDuration;
            yield return null;
        }
        fadePanel.alpha = 1;
    }

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadePanel.alpha = 1 - (t / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = 0;
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
    public int enemyDifficulty; // 1 - easy, 2 - medium, 3 - hard

    // can also have more in regards to which powerups are available and stuff.
}

