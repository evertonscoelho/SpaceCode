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