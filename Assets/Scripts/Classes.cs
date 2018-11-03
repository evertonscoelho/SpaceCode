using System.Collections.Generic;
using UnityEngine;

public enum EnumCommand
{
    CIRCLE, 
    CIRCLE_TITLE,
    STAR,
    STAR_TITLE,
    TRIANGLE,
    TRIANGLE_TITLE,
    LOOP,
    LEFT,
    RIGHT,
    MOVE,
    UNKNOW
};  

public enum PlayerDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public enum StatusGame
{
    VICTORY,
    DEFEAT,
    CONTINUE
}

public class Function
{
    public List<Command> Commands;

    public Function(List<Command> commands)
    {
        this.Commands = commands;
    }
}

public class Command
{
    public EnumCommand EnumCommand;
    public GameObject gameObject;
    public List<Command> loop;
    public int numRepeatLoop;

    public Command(EnumCommand command)
    {
        this.EnumCommand = command;
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
    public int maxCommandsUse;
    public string playerDirection;
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
