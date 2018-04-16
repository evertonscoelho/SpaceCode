using System;
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
        Function[] functions = { new Function(), new Function(), new Function() };

        //functions[0].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.LEFT, EnumCommand.RIGHT, EnumCommand.RIGHT, EnumCommand.RIGHT };
        //functions[0].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.DOWN, EnumCommand.F2, EnumCommand.F1 };
        // functions[1].Commands = new EnumCommand[] { EnumCommand.DOWN, EnumCommand.F3, EnumCommand.LEFT };
        // functions[2].Commands = new EnumCommand[] { EnumCommand.RIGHT, EnumCommand.UP };

        functions[0].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.LEFT, EnumCommand.RIGHT, EnumCommand.RIGHT, EnumCommand.RIGHT };
        functions[1].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.UP};
        functions[2].Commands = new EnumCommand[] { EnumCommand.LEFT, EnumCommand.DOWN};
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
        gameManager.boardComamandManager = this;
    }

    public IEnumerator terminateMovement(Vector2 positionCollectable)
    {
        while (!playerBody.position.Equals(positionCollectable))
        {
            playerBody.position = Vector3.MoveTowards(playerBody.position, positionCollectable, 0.1f);
            yield return null;
        }
    }

    public void doCommands(Function[] functions)
    {
        functionsBoard = functions;
        printActionsInBoard(functions);
        StartCoroutine(DoFunction(functions[0]));
    }

    private IEnumerator DoFunction(Function function)
    {
        bool endGame;
        EnumCommand comando;
        for (int i = 0; i < function.Commands.Length; i++)
        {
            comando = function.Commands[i];
            endGame = gameManager.checkEndGameCommand();
            if (endGame)
            {
                break;
            }
            switch (comando)
            {
                case EnumCommand.UP:
                    Vector2 targetUp = playerBody.position + new Vector2(0, offsetXPlayer);
                    for(float aux = playerBody.position.y; aux < targetUp.y; aux += 0.1f)
                    {
                        playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y + 0.1f));
                        yield return null;
                    }
                    break;
                case EnumCommand.DOWN:
                    Vector2 targetDown = playerBody.position - new Vector2(0, offsetXPlayer);
                    for (float aux = playerBody.position.y; aux > targetDown.y; aux -= 0.1f)
                    {
                        playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y - 0.1f));
                        yield return null;
                    }
                    break;
                case EnumCommand.LEFT:
                    Vector2 targetLeft = playerBody.position - new Vector2(offsetYPlayer, 0);
                    for (float aux = playerBody.position.x; aux > targetLeft.x; aux -= 0.1f)
                    {
                        playerBody.MovePosition(new Vector2(playerBody.position.x - 0.1f, playerBody.position.y));
                        yield return null;
                    }
                    break;
                case EnumCommand.RIGHT:
                    Vector2 targetRight = playerBody.position + new Vector2(offsetYPlayer, 0);
                    for (float aux = playerBody.position.x; aux < targetRight.x; aux += 0.1f)
                    {
                        playerBody.MovePosition(new Vector2(playerBody.position.x + 0.1f, playerBody.position.y));
                        yield return null;
                    }
                    break;
                case EnumCommand.F1:
                    yield return StartCoroutine(DoFunction(functionsBoard[0]));
                    break;
                case EnumCommand.F2:
                    yield return StartCoroutine(DoFunction(functionsBoard[1]));
                    break;
                case EnumCommand.F3:
                    yield return StartCoroutine(DoFunction(functionsBoard[2]));
                    break;
            }
            if (!endGame)
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
            playerBody.MovePosition(new Vector2(0, playerBody.position.y + 0.1f));
            yield return null;
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
