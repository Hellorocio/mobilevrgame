using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerDisplay;
    public GameObject timerUIObj;
    public GameStageManager gameStageManager;

    public int timerMinutes = 1;

    public void SetTimer (bool timerOn)
    {
        if (timerOn)
        {
            timerUIObj.SetActive(true);
            StartCoroutine(CountdownTimer());
        }
        else
        {
            timerUIObj.SetActive(false);
        }
    }

    public void StopTimer ()
    {
        StopAllCoroutines();
    }


    IEnumerator CountdownTimer()
    {
        int timerVal = timerMinutes * 60;
        timerDisplay.text = DisplayTime(timerVal);

        while (timerVal >= 0)
        {
            yield return new WaitForSeconds(1);
            timerVal--;
            timerDisplay.text = DisplayTime(timerVal);
        }
        

        gameStageManager.SetGameStage(2);

    }

    private string DisplayTime(int time)
    {
        int min = time / 60;
        int sec = time % 60;

        string formatSec = "";
        if (sec < 10)
        {
            formatSec = "0";
        }
        return min + ":" + formatSec + sec;
    }

}
