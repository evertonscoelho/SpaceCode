using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardCommandManager : MonoBehaviour
{

    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject boardCommand;

    private List<Function> functionsBoard;

    private float offsetX;
    private float offsetY;
    private float offsetXPlayer;
    private float offsetYPlayer;
    private GameObject player;
    private GameManager gameManager;
    private Rigidbody2D playerBody;

    public void initValues()
    {
        offsetX = Up.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = Up.GetComponent<SpriteRenderer>().bounds.size.y;
        player = GameObject.Find("Player(Clone)");
        playerBody = player.GetComponent<Rigidbody2D>();
        GameObject floor = GameObject.Find("Floor(Clone)");
        offsetXPlayer = floor.GetComponent<SpriteRenderer>().bounds.size.y;
        offsetYPlayer = floor.GetComponent<SpriteRenderer>().bounds.size.y;
        gameManager = GameManager.instance;
        gameManager.boardComamandManager = this;
    }

    public void doCommands(List<Function> functions)
    {
        functionsBoard = functions;
        printActionsInBoard(functions);
        StartCoroutine(DoFunction(functions[0]));
    }

    private IEnumerator DoFunction(Function function)
    {
        bool endGame = false;
        foreach (EnumCommand command in function.Commands)
        {
            if (endGame)
            {
                break;
            }
            endGame = gameManager.checkEndGameCommand();
            switch (command)
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
                case EnumCommand.A:
                    yield return StartCoroutine(DoFunction(functionsBoard[0]));
                    break;
                case EnumCommand.B:
                    yield return StartCoroutine(DoFunction(functionsBoard[1]));
                    break;
                case EnumCommand.C:
                    yield return StartCoroutine(DoFunction(functionsBoard[2]));
                    break;
            }
            if (!endGame)
            {
                yield return new WaitForSeconds(1);
            }
        }
        if (!endGame)
        {
            gameManager.doDefeat();
        }
    }

    public void printActionsInBoard(List<Function> functions)
    {
        Transform transformBoardCommand = boardCommand.transform;
        GameObject toInstantiate, instance;
        for (int y = 0; y < functions.Count; y++)
        {
            for (int x = 0; x < functions[y].Commands.Count; x++)
            {
                toInstantiate = getObjectToInstantiate(functions[y].Commands[x]);
                instance = Instantiate(toInstantiate, transformBoardCommand.transform, true) as GameObject;
                instance.transform.localPosition = getPositionInstance(x - 3, (y * -1) + 1);
            }
        }
    }

    private Vector3 getPositionInstance(int x, float y)
    {
        return new Vector3(x * offsetX, y * offsetY, 0f);
    }

    public IEnumerator terminateMovement(Vector2 positionCollectable)
    {
        while (!playerBody.position.Equals(positionCollectable))
        {
            playerBody.position = Vector3.MoveTowards(playerBody.position, positionCollectable, 0.1f);
            yield return null;
        }
    }

    public GameObject getObjectToInstantiate(EnumCommand command)
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
            case EnumCommand.A:
                return A;
            case EnumCommand.B:
                return B;
            case EnumCommand.C:
                return C;
            default:
                return null;
        }
    }
}