using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingRackManager : MonoBehaviour
{
    public ClothingRackObject[] pagableRacks;
    private ClothingRackObject[] allClothingRacks;

    private int currentRackPage = 0;

    void Start()
    {
        allClothingRacks = gameObject.GetComponentsInChildren<ClothingRackObject>();
    }

    /// <summary>
    /// Called when reset button is pressed
    /// </summary>
    public void ResetClothingRacks ()
    {
        foreach (ClothingRackObject c in allClothingRacks)
        {
            print(c.name + " reset");
            c.ResetRack();
        }
    }

    /// <summary>
    /// Called by arrow buttons to page to a different rack
    /// If nextRack is true, go the the next one. Otherwise, go the the previous rack
    /// TODO: see if we want to check that the clothing is actually on the rack before turning all of it off
    /// </summary>
    public void ChangeRack (bool nextRack)
    {
        int newRackPage = GetNextRackPageNum(nextRack);
        pagableRacks[currentRackPage].gameObject.SetActive(false);
        pagableRacks[newRackPage].gameObject.SetActive(true);
        currentRackPage = newRackPage;
    }

    /// <summary>
    /// Calculates what the next current rack page will be
    /// </summary>
    /// <param name="nextRack"></param>
    private int GetNextRackPageNum (bool nextRack)
    {
        int newRackPage = currentRackPage;
        if (nextRack)
        {
            if (newRackPage + 1 < pagableRacks.Length)
            {
                newRackPage++;
            }
            else
            {
                newRackPage = 0;
            }
        }
        else
        {
            if (newRackPage - 1 >= 0)
            {
                newRackPage--;
                
            }
            else
            {
                newRackPage = pagableRacks.Length - 1;
            }
            //print("current = " + currentRackPage + " previous page = " + newRackPage);
        }
        return newRackPage;
    }
}
