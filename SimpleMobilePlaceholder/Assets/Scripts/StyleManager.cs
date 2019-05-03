using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleManager : MonoBehaviour
{
    public Text currentStyleText;

    public Text scoreTallyText;
    public Text finalScoreText;
    public Text scoreTitleText;
    public ClothingGrabber clothingGrabber;
    public GameStageManager gameStageManager;

    public enum Style
    {
        Casual = 1,
        Formal = 2,
    }

    private int numStyles;
    private Style currentStyle;

    private void Start()
    {
        numStyles = System.Enum.GetValues(typeof(Style)).Length;
    }



    public Style GenerateNewStyle()
    {
        int newStyle = Random.Range(1, numStyles + 1);
        if (newStyle == (int)currentStyle)
        {
            newStyle = GetNextStyle(newStyle);
        }
        currentStyle = (Style)newStyle;

        SetCurrentStyleText();

        return currentStyle;
    }

    private void SetCurrentStyleText()
    {
        currentStyleText.text = currentStyle.ToString();
    }

    /// <summary>
    /// Used to get a different style if the new style is a repeat
    /// </summary>
    /// <param name="newStyle"></param>
    /// <returns></returns>
    private int GetNextStyle(int newStyle)
    {
        if (newStyle < numStyles)
        {
            newStyle++;
        }
        else
        {
            newStyle = 1;
        }

        return newStyle;
    }

    /// <summary>
    /// Gets the score, then sets the scoring UI
    /// </summary>
    public int GenerateScore()
    {
        int score = 0;
        string tally = "";

        string[] matchingTally = clothingGrabber.GetMatchingStyleScore(currentStyle);
        score += int.Parse(matchingTally[0]);
        tally += matchingTally[1];


        int maxColor = clothingGrabber.GetMaxMatchingColors() * 10;
        if (maxColor > 10)
        {
            tally += "+" + maxColor + " matching color bonus\n";
            score += maxColor;
        }


        scoreTallyText.text = tally;
        scoreTitleText.text = GenerateTitleText(score);
        finalScoreText.text = "Total: " + score;
        return score;
    }

    /// <summary>
    /// Says if you pass or fail based on score
    /// </summary>
    /// <returns></returns>
    private string GenerateTitleText(int score)
    {
        string result = "";
        if (score > CalculateMinScore())
        {
            result = "Nice style!";
            gameStageManager.PlayFanfare(true);
        }
        else
        {
            result = "Needs work";
            gameStageManager.PlayFanfare(false);

        }
        return result;
    }

    private int CalculateMinScore ()
    {
        int minScore = 0;
        switch (currentStyle)
        {
            case StyleManager.Style.Casual:
                {
                    minScore = 30;
                    break;
                }
            case StyleManager.Style.Formal:
                {
                    minScore = 50;
                    break;
                }
            default:
                break;
        }
        return minScore;
    }
}
