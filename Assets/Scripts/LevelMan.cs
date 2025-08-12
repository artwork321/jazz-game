using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelMan : MonoBehaviour
{
    public List<Level> levels;
    public Sprite[] anim;
    private int currentLevelIndex = 0;
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;
    public GameMan gm;

    public Image enemySprite;

    public void Start()
    {
        gm = gameObject.GetComponent<GameMan>();
        enemySprite = GameObject.Find("EnemySprite").GetComponent<Image>();
    }

    public void StartNewLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
        {
            // either we start boss level -> maybe we put this on a new scene or smth im not sure
            // or this will trigger the end of the game maybe.
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen"); 
        }
        else
        {
            StartCoroutine(LevelTransition(levelIndex));
        }
    }

    private IEnumerator LevelTransition(int levelIndex)
    {
        // can add the tutorial for new powerups here
        fadePanel.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levels[levelIndex].levelDescription;
        yield return StartCoroutine(FadeOut());


        currentLevelIndex = levelIndex;
        Level level = levels[levelIndex];
        enemySprite.sprite = level.enemySprite; 
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


        gm.NewGame(levelIndex);
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeIn());
        // Trigger dialogue
        if (levelIndex == 5)
        {
            //yield return StartCoroutine(PlayEnemySpriteAnimation());
        }
        FindObjectOfType<Dialogue>().PlayLine(level.dialogueIndex);
    }
    private IEnumerator PlayEnemySpriteAnimation()
    {
        if (anim == null || anim.Length == 0)
            yield break;

        float frameDuration = 0.1f; // Adjust speed here
        for (int i = 0; i < anim.Length; i++)
        {
            enemySprite.sprite = anim[i];
            yield return new WaitForSeconds(frameDuration);
        }
        // Ends automatically on last frame of anim[]
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

    public string levelDescription;

    public Sprite enemySprite;
    // can also have more in regards to which powerups are available and stuff.
}

