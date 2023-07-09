using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public TextMeshProUGUI textbox1;
    public string tutorialMouse = "Move the mouse to \ndirect the bullet!";
    public string tutorialSlowMouse = "Left click to \nslow time!";
    public string tutorialWASD = "Use WASD to direct \nthe bullet!";
    public string tutorialSlowWASD = "Press Space to \nslow time!";
    public string tutorialPause = "Press ESC to pause!";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tutorial());
    }
    private IEnumerator Tutorial()
    {
        if (PlayerPrefs.GetInt("Controls", 0) == 1)
        {
            StartCoroutine(Typer(textbox1, tutorialWASD, 0.1f));
            yield return new WaitForSeconds(5);
            StartCoroutine(Typer(textbox1, tutorialSlowWASD, 0.1f));
            yield return new WaitForSeconds(5);
            StartCoroutine(Typer(textbox1, tutorialPause, 0.1f));
            yield return new WaitForSeconds(5);
            textbox1.text = "";
        }
        else
        {
            StartCoroutine(Typer(textbox1, tutorialMouse, 0.1f));
            yield return new WaitForSeconds(5);
            StartCoroutine(Typer(textbox1, tutorialSlowMouse, 0.1f));
            yield return new WaitForSeconds(5);
            StartCoroutine(Typer(textbox1, tutorialPause, 0.1f));
            yield return new WaitForSeconds(5);
            textbox1.text = "";
        }
    }
    private IEnumerator Typer(TextMeshProUGUI tmp, string words, float time)
    {
        tmp.text = "";
        foreach (char c in words)
        {
            tmp.text += c;
            yield return new WaitForSeconds(time);
        }
    }
}
