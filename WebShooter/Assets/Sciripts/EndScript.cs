using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScript : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Start()
    {
        TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>();

        foreach(TextMeshProUGUI text in allTexts)
        {
            if (text.tag == "EndScoreText")
                text.SetText("Your score is {0}", MapControllerScript.Score());
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
