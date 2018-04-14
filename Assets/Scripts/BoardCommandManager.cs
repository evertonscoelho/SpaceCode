using System;
using UnityEngine;

public class BoardCommandManager : MonoBehaviour
{

    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;
    public GameObject F1;
    public GameObject F2;
    public GameObject F3;

    public GameObject boardCommand;

    float offsetX;
    float offsetY;

    public void testClass()
    {
        Function[] functions = { new Function(), new Function(), new Function() };
        functions[0].Commands = new EnumCommand[]{ EnumCommand.UP, EnumCommand.UP} ;
        functions[1].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.UP, EnumCommand.UP };
        functions[2].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.UP };

        doCommands(functions);
    }


    public void doCommands(Function[] functions)
    {
        BoardCommandManager boardCommandManager = boardCommand.GetComponent<BoardCommandManager>();
        offsetX = Up.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = Up.GetComponent<SpriteRenderer>().bounds.size.y;

        printActionsInBoard(functions);
        for (int i = 0; i < functions.Length; i++)
        {
            doFunction(functions[i]);
        }
    }

    public void doFunction(Function function)
    {
        EnumCommand command;
        for (int i = 0; i < function.Commands.Length; i++)
        {
            command = function.Commands[i];
            animationMoviment(i);
            this.action(command);
            new WaitForSeconds(1);
        }
    }


    public void printActionsInBoard(Function[] functions)
    {
        Transform transformBoardCommand = boardCommand.transform;
        
        float positionY;
        for (int y= 0; y < functions.Length; y++)
        {
            for (int x = 0; x < functions[y].Commands.Length; x++)
            {
                GameObject toInstantiate = getObjectToInstantiate(functions[y].Commands[x]);
                GameObject instance = Instantiate(toInstantiate, transformBoardCommand.transform, true) as GameObject;
                instance.transform.localPosition = getPositionInstance(x - 3, (y* -1)+1);
            }
        }
    }

    private Vector3 getPositionInstance(int x, float y)
    {
        return new Vector3(x * offsetX, y * offsetY, 0f);
    }

    public void animationMoviment(int i)
    {
        //TODO
    }

    private void action(EnumCommand command)
    {
        switch (command)
        {
            case EnumCommand.UP:
                int teste = 1 + 1;
                break;
            default:
                teste = 2 + 2;
                break;
        }
    }

    private GameObject getObjectToInstantiate(EnumCommand movement)
    {
        switch (movement)
        {
            case EnumCommand.UP:
                return Up;
            case EnumCommand.DOWN:
                return Down;
            case EnumCommand.LEFT:
                return Left;
            case EnumCommand.RIGHT:
                return Right;
            case EnumCommand.F1:
                return F1;
            case EnumCommand.F2:
                return F2;
            case EnumCommand.F3:
                return F3;
            default:
                return null;
        }
    }
}
