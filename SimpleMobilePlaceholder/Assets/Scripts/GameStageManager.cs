﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStageManager : MonoBehaviour
{
    public GameObject[] UIDisplayStages; // 0 = start, 1 = game, 2 = score
    public GameObject UIClickBlocker; //boxes that prevent clicking in scene when UI is active

    public StyleManager styleManager;

    private GameObject currrentUIDisplay;

    public enum GameStage { start, game, score };
   

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


}
