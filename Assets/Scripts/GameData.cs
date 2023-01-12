using UnityEngine;


[System.Serializable]
public class GameData
{
    public long lastTimeUpdate;

    public int playerMoney;

    public Vector2 playerPosition;

    public GameData()
    {
        this.playerMoney = 0;
        this.playerPosition = Vector2.zero;
    }

}
