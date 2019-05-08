using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStageManager : MonoBehaviour
{
    public GameObject[] UIDisplayStages; // 0 = start, 1 = game, 2 = score
    public GameObject UIClickBlocker; //boxes that prevent clicking in scene when UI is active

    public TimerManager timerManager;

    public StyleManager styleManager;

    public RadioScript radioScript;

    public GameObject colorPicker;
    private GameObject currrentUIDisplay;

    public enum GameStage { start, game, score };

    public AudioClip[] fanfareSounds; // 0 = win, 1 = lose
    public AudioSource fanfareSource; 
   

    private void Start()
    {
        SetGameStage((int)GameStage.start);
    }

    public void SetGameStage (int stage)
    {
        SetCurrentUIDisplay(stage);
        SetUIClickBlocker((GameStage)stage);
        UpdateStyleManager((GameStage)stage);
    }

    public void StartGame (bool timedModeOn)
    {
        timerManager.SetTimer(timedModeOn);

        if (timedModeOn)
        {
            radioScript.SetMusic(RadioScript.MusicType.Timed);
        }
        else
        {
            radioScript.SetMusic(RadioScript.MusicType.Untimed);
        }
        SetGameStage(1);
    }

    public void UpdateStyleManager (GameStage stage)
    {
        switch (stage)
        {
            case GameStage.game:
                {
                    styleManager.GenerateNewStyle();
                    break;
                }
            case GameStage.score:
                {
                    styleManager.GenerateScore();
                    radioScript.SetMusic(RadioScript.MusicType.Untimed);
                    break;
                }
            default:
                break;
        }
    }

    private void SetCurrentUIDisplay (int stage)
    {
        if (currrentUIDisplay != null)
        {
            currrentUIDisplay.SetActive(false);
        }

        if (colorPicker != null)
        {
            colorPicker.SetActive(false);
        }

        currrentUIDisplay = UIDisplayStages[(int)stage];
        currrentUIDisplay.SetActive(true);
    }

    private void SetUIClickBlocker (GameStage stage)
    {
        if (stage == GameStage.game)
        {
            UIClickBlocker.SetActive(false);
        }
        else
        {
            UIClickBlocker.SetActive(true);
        }
    }


    public void PlayFanfare (bool win)
    {
        if (win)
        {
            fanfareSource.clip = fanfareSounds[0];
        }
        else
        {
            fanfareSource.clip = fanfareSounds[1];
        }
        fanfareSource.Play();
    }


}
