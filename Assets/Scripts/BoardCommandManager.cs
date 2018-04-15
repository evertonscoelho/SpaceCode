﻿using System;
using UnityEngine;
using System.Collections;

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

    private Function[] functionsBoard;

    private float offsetX;
    private float offsetY;
    private float offsetXPlayer;
    private float offsetYPlayer;
    private GameObject player;
    private GameManager gameManager;
    private Rigidbody2D playerBody;

    public void testClass()
    {
        Function[] functions = {new Function()};

        functions[0].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.LEFT, EnumCommand.RIGHT, EnumCommand.RIGHT, EnumCommand.RIGHT };
        //functions[0].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.DOWN, EnumCommand.F2, EnumCommand.F1 };
       // functions[1].Commands = new EnumCommand[] { EnumCommand.DOWN, EnumCommand.F3, EnumCommand.LEFT };
       // functions[2].Commands = new EnumCommand[] { EnumCommand.RIGHT, EnumCommand.UP };
        doCommands(functions);
    }

    public void initValues()
    {
        offsetX = Up.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = Up.GetComponent<SpriteRenderer>().bounds.size.y;
        player = GameObject.Find("Player(Clone)");
        playerBody = player.GetComponent<Rigidbody2D>();
        GameObject floor = GameObject.Find("Floor(Clone)");
        offsetXPlayer = floor.GetComponent<SpriteRenderer>().bounds.size.y;
        offsetYPlayer = floor.GetComponent<SpriteRenderer>().bounds.size.y;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void doCommands(Function[] functions)
    {
        functionsBoard = functions;
        printActionsInBoard(functions);
        doFunction(functions[0]);
    }

    public void doFunction(Function function)
    {
        StartCoroutine(this.testeMovimento(function));
        /*
        EnumCommand command;
        Boolean endGame = false;
        int i = 0;
        while (i < function.Commands.Length && !endGame)
        {
            command = function.Commands[i];
            this.action(command);
            endGame = gameManager.checkEndGame(1, 0);
            i++;
        }*/
    }

    private IEnumerator testeMovimento(Function function)
    {
        for (int i = 0; i < function.Commands.Length; i++)
        {
            EnumCommand comando = function.Commands[i];
            switch (comando)
            {
                case EnumCommand.UP:
                    Vector2 targetUp = playerBody.position + new Vector2(0, offsetXPlayer);
                    while (playerBody.position.y < targetUp.y)
                    {
                        playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y + 0.1f));
                        yield return null;
                    }
                    break;
                case EnumCommand.DOWN:
                    Vector2 targetDown = playerBody.position - new Vector2(0, offsetXPlayer);
                    while (playerBody.position.y > targetDown.y)
                    {
                        playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y - 0.1f));
                        yield return null;
                    }
                    break;
                case EnumCommand.LEFT:
                    Vector2 targetLeft = playerBody.position - new Vector2(offsetYPlayer, 0);
                    while (playerBody.position.x > targetLeft.x)
                    {
                        playerBody.MovePosition(new Vector2(playerBody.position.x - 0.1f, playerBody.position.y));
                        yield return null;
                    }
                    break;
                case EnumCommand.RIGHT:
                    Vector2 targetRight = playerBody.position + new Vector2(offsetYPlayer, 0);
                    while (playerBody.position.x < targetRight.x)
                    {
                        playerBody.MovePosition(new Vector2(playerBody.position.x + 0.1f, playerBody.position.y));
                        yield return null;
                    }
                    break;
            }
           
            bool endGame = gameManager.checkEndGame(1, 0);
            if (endGame)
            {
                break;
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
           
        }
    }

    public void printActionsInBoard(Function[] functions)
    {
        Transform transformBoardCommand = boardCommand.transform;

        for (int y = 0; y < functions.Length; y++)
        {
            for (int x = 0; x < functions[y].Commands.Length; x++)
            {
                GameObject toInstantiate = getObjectToInstantiate(functions[y].Commands[x]);
                GameObject instance = Instantiate(toInstantiate, transformBoardCommand.transform, true) as GameObject;
                instance.transform.localPosition = getPositionInstance(x - 3, (y * -1) + 1);
            }
        }
    }

    private Vector3 getPositionInstance(int x, float y)
    {
        return new Vector3(x * offsetX, y * offsetY, 0f);
    }

    private IEnumerator animationMoviment(Vector2 target)
    {
        Vector2 playerPosition = playerBody.position;
        while (playerBody.position.y < target.y)
        {
            playerBody.MovePosition(new Vector2(0, playerBody.position.y+0.1f));
            yield return null;
        }
            
    }

    private void action(EnumCommand command)
    {
        switch (command)
        {
            case EnumCommand.UP:
                StartCoroutine(this.animationMoviment(playerBody.position + new Vector2(0, offsetXPlayer))); 
                //player.transform.position += new Vector3(0, offsetXPlayer, 0);
                break;
            case EnumCommand.DOWN:
                player.transform.position -= new Vector3(0, offsetXPlayer, 0);
                break;
            case EnumCommand.LEFT:
                player.transform.position -= new Vector3(offsetYPlayer, 0, 0);
                break;
            case EnumCommand.RIGHT:
                player.transform.position += new Vector3(offsetYPlayer, 0, 0);
                break;
            case EnumCommand.F1:
                doFunction(functionsBoard[0]);
                break;
            case EnumCommand.F2:
                doFunction(functionsBoard[1]);
                break;
            case EnumCommand.F3:
                doFunction(functionsBoard[2]);
                break;
            default:
                break;
        }
    }

    private GameObject getObjectToInstantiate(EnumCommand command)
    {
        switch (command)
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
