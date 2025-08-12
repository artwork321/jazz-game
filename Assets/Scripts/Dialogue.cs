using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public List<DialogueLine> lines;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.03f;  

    private int currPartIndex = 0;
    private DialogueLine currLine;
    private Coroutine typingEffect;
    public AudioSource dialogueAS;

    void Start()
    {
        dialogueText = GameObject.Find("dialogue").GetComponent<TextMeshProUGUI>();
        dialogueAS = GetComponent<AudioSource>();
        // for testing, otherwise usually the play line should be called at the start of rounds, or  
        // other trigger moments, e.g reactions to wins or losses
    }

    public void PlayLine(int lineIndex)
    {
        currLine = lines[lineIndex];
        currPartIndex = 0;

        if (typingEffect != null) StopCoroutine(typingEffect);
        typingEffect = StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        dialogueAS.clip = currLine.voice[0];
        dialogueAS.Play();
        while (currPartIndex < currLine.parts.Count)
        {
            dialogueText.text = "";
            // todo: play the audio voice lines
            foreach (char c in currLine.parts[currPartIndex])
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            // for now, later will change depending on when the voice line ends.
            yield return new WaitForSeconds(2f);

            currPartIndex++;
        }

        // all parts typd, reset text
        dialogueText.text = "";
    }
}

[System.Serializable]
public class DialogueLine
{
    // each dialogue line is split into parts according to how much can be displayed on the screen at one time
    public List<string> parts; 
    public List<AudioClip> voice; 
}
