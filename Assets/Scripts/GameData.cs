using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[System.Serializable]
public class GameData
{
    public int playerMoney;

    public Vector2 playerPosition;

    public GameData()
    {
        this.playerMoney = 0;
        this.playerPosition = Vector2.zero;
    }

}
