﻿using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastTimeUpdate;

    public int playerMoney;

    public Vector2 playerPosition;

    public GameData()
    {
        this.playerMoney = 500;
        this.playerPosition = new Vector2(-65.23f, -38.45f);
    }

}




