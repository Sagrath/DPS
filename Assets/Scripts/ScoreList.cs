using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreList 
{
    public string player = "";
    public int points = 0;

    public ScoreList(string newPlayer, int newPoints)
    {
        player = newPlayer;
        points = newPoints;
    }

}
