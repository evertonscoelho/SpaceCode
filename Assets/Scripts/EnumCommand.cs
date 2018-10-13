using System.Collections.Generic;

public enum EnumCommand
{
    UP, 
    DOWN,
    LEFT,
    RIGHT,
    A,
    B,
    C,
    A_TITLE,
    B_TITLE,
    C_TITLE,
    UNKNOW
};  

public enum StatusGame
{
    VICTORY,
    DEFEAT,
    CONTINUE
}

public class Function
{
    public List<EnumCommand> Commands;

    public Function(List<EnumCommand> commands)
    {
        this.Commands = commands;
    }
}

[System.Serializable]
public class Level
{
    public string name;
    public string levelID;
    public string author;
    public int difficulty;
    public int collectable;
    public int maxCommands;
    public Board board;
}

[System.Serializable]
public class Board
{
    public DataBoard[] data;
}

[System.Serializable]
public class DataBoard
{
    public int positionX;
    public int positionY;
    public string objectType;
}
