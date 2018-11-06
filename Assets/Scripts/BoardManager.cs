using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BoardManager : MonoBehaviour
{
    public GameObject Collectable, Floor, Obstacle, Player, Wall;

    public GameObject player, circle_title, star_title, triangle_title, circle, star, triangle, loop, left, right, move, _2, _3, _4, _5, _6, _7, _8, _9;

    public Sprite circleMark, starMark, triangleMark, loopMark, leftMark, rightMark, moveMark, _2Mark, _3Mark, _4Mark, _5Mark, _6Mark, _7Mark, _8Mark, _9Mark;

    private List<Function> functionsBoard;

    private float offsetXBoard, offsetYBoard, offsetXCommand, offsetYCommand;

    private GameManager gameManager;
    private Rigidbody2D playerBody;

    private Level level;
    private int commands = 0, collectables = 0, indexCircle, indexTriangle, indexStar;

    bool endGame = false;

    PlayerDirection playerDirection;

    Command previous = null;

    public void initValues()
    {
        offsetXCommand = circle.GetComponent<Image>().sprite.bounds.size.x;
        offsetYCommand = circle.GetComponent<Image>().sprite.bounds.size.y;
        offsetXBoard = Floor.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetYBoard = Floor.GetComponent<SpriteRenderer>().bounds.size.y;
        gameManager = GameManager.instance;
    }

    public void SetupScene(string idLevel)
    {
        commands = 0;
        collectables = 0;
        this.level = JsonUtility.FromJson<Level>(getJsonFileById(idLevel));
        boardSetup(level.board, level.playerDirection);
    }

    public void boardSetup(Board board, string playerDirection)
    {
        Transform boardHolder = GameObject.Find("Board").transform;
        GameObject floor, toInstantiate, instance;
        foreach (DataBoard dataBoard in board.data)
        {
            floor = Instantiate(Floor, getPositionBoardInstance(dataBoard.positionX, dataBoard.positionY), Quaternion.identity) as GameObject;
            floor.transform.SetParent(boardHolder);
            if (dataBoard.objectType != "Floor")
            {
                toInstantiate = getObjectToInstantiate(dataBoard.objectType);
                instance = Instantiate(toInstantiate, getPositionBoardInstance(dataBoard.positionX, dataBoard.positionY), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                if(dataBoard.objectType == "Player")
                {
                    playerBody = instance.GetComponent<Rigidbody2D>();
                    setPlayerDirection(instance, playerDirection);
                }
            }
        }
    }
 
    private void setPlayerDirection(GameObject instance, string direction)
    {
        //TODO
        switch (direction)
        {
            case "UP":
                playerDirection = PlayerDirection.UP;
                break;
            case "DOWN":
                playerDirection = PlayerDirection.DOWN;
                break;
            case "LEFT":
                playerDirection = PlayerDirection.LEFT;
                break;
            case "RIGHT":
                playerDirection = PlayerDirection.RIGHT;
                break;
        }
    }

    public StatusGame checkEndGame(int addCommand, int addCollectable)
    {
        commands += addCommand;
        collectables += addCollectable;
        if (commands >= level.maxCommands)
        {
            return StatusGame.DEFEAT;
        }
        else if(collectables >= level.collectable)
        {
            return StatusGame.VICTORY;
        }
        else
        {
            return StatusGame.CONTINUE;
        }      
    }

    public IEnumerator execute(List<Function> functions)
    {
        functionsBoard = functions;
        printActionsInBoard(functions);
        yield return StartCoroutine(DoFunction(functions[0]));
        if (!endGame)
        {
            gameManager.doDefeat();
        }
    }

    private IEnumerator DoFunction(Function function)
    {
        foreach (Command command in function.Commands)
        {
            animationCommand(command);
            if (!endGame)
            {
                endGame = gameManager.checkEndGameCommand();
                switch (command.EnumCommand)
                {
                    case EnumCommand.MOVE:
                        yield return StartCoroutine(Move());
                        break;
                    case EnumCommand.CIRCLE:
                        yield return new WaitForSeconds(1f);
                        animationCommand(null);
                        yield return StartCoroutine(DoFunction(functionsBoard[indexCircle]));
                        break;
                    case EnumCommand.STAR:
                        yield return new WaitForSeconds(1f);
                        animationCommand(null);
                        yield return StartCoroutine(DoFunction(functionsBoard[indexStar]));
                        break;
                    case EnumCommand.TRIANGLE:
                        yield return new WaitForSeconds(1f);
                        animationCommand(null);
                        yield return StartCoroutine(DoFunction(functionsBoard[indexTriangle]));
                        break;
                    case EnumCommand.LOOP:
                        yield return StartCoroutine(Loop(command));
                        break;
                    case EnumCommand.LEFT:
                        yield return StartCoroutine(Turn(PlayerDirection.LEFT));
                        break;
                    case EnumCommand.RIGHT:
                        yield return StartCoroutine(Turn(PlayerDirection.RIGHT));
                        break;
                    case EnumCommand.CIRCLE_TITLE:
                        commands--;
                        break;
                    case EnumCommand.STAR_TITLE:
                        commands--;
                        break;
                    case EnumCommand.TRIANGLE_TITLE:
                        commands--;
                        break;
                }
            }
            else
            {
                gameManager.doDefeat();
            }
        }
        animationCommand(null);
    }

    private IEnumerator Turn(PlayerDirection direction)
    {
        //TODO
        switch (playerDirection) { 
            case PlayerDirection.LEFT:
                yield return StartCoroutine(MoveLeft());
                break;
            case PlayerDirection.RIGHT:
                yield return StartCoroutine(MoveRight());
                break;
        }   
    }

    private IEnumerator Loop(Command command)
    {
        Function function = new Function(command.loop);
        for(int x = 0; x < command.numRepeatLoop; x++)
        {
            yield return StartCoroutine(DoFunction(function));
        }
    }

    private IEnumerator Move()
    {
        switch (playerDirection)
        {     
            case PlayerDirection.UP:
                yield return StartCoroutine(MoveUp());
                break;
            case PlayerDirection.DOWN:
                yield return StartCoroutine(MoveDown());
                break;
            case PlayerDirection.LEFT:
                yield return StartCoroutine(MoveLeft());
                break;
            case PlayerDirection.RIGHT:
                yield return StartCoroutine(MoveRight());
                break;
        }
    }

    private IEnumerator MoveRight()
    {
        Vector2 target = playerBody.position - new Vector2(offsetYBoard, 0);
        float diff = playerBody.position.x - target.x - 20, attempts = 0;
        target = playerBody.position + new Vector2(offsetYBoard, 0);
        diff = playerBody.position.x - target.x + 20;
        while (playerBody.position.x < target.x && attempts < diff)
        {
            playerBody.MovePosition(new Vector2(playerBody.position.x + 0.1f, playerBody.position.y));
            attempts += 0.1f;
            yield return null;
        }
        if (attempts >= diff)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator MoveLeft()
    {
        Vector2 target = playerBody.position - new Vector2(offsetYBoard, 0);
        float diff = playerBody.position.x - target.x - 20, attempts = 0;
        while (playerBody.position.x > target.x && attempts > diff)
        {
            playerBody.MovePosition(new Vector2(playerBody.position.x - 0.1f, playerBody.position.y));
            attempts -= 0.1f;
            yield return null;
        }
        if (attempts <= diff)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator MoveDown()
    {
        Vector2 target = playerBody.position - new Vector2(0, offsetXBoard);
        float diff = playerBody.position.y - target.y - 20, attempts = 0;
        while (playerBody.position.y > target.y && attempts > diff)
        {
            playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y - 0.1f));
            attempts -= 0.1f;
            yield return null;
        }
        if (attempts <= diff)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        { 
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator MoveUp()
    {
        Vector2 target = playerBody.position + new Vector2(0, offsetXBoard);
        float diff = playerBody.position.y - target.y + 20, attempts = 0;
        while (playerBody.position.y < target.y && attempts < diff)
        {
            playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y + 0.1f));
            attempts += 0.1f;
            yield return null;
        }
        if (attempts >= diff)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
    }

    private void animationCommand(Command command)
    {
        if (previous != null)
        {
            GameObject instance =  getObjectToInstantiate(previous.EnumCommand);
            previous.gameObject.GetComponent<SpriteRenderer>().sprite = instance.GetComponent<SpriteRenderer>().sprite;
        }
        if(command != null)
        {
            command.gameObject.GetComponent<SpriteRenderer>().sprite = getSpriteCommandMark(command.EnumCommand);
        }
        previous = command;
    }

    public void printActionsInBoard(List<Function> functions)
    {
        Transform transformBoardCommand = GameObject.Find("BoardCommand").transform;
        for (int y = 0; y < functions.Count; y++)
        {
            int positionBoard = 0;
            for (int x = 0; x < functions[y].Commands.Count; x++)
            {
                printCommandOnBoard(functions[y].Commands[x], ref positionBoard, y, transformBoardCommand.transform, false);
                if (EnumCommand.LOOP.Equals(functions[y].Commands[x].EnumCommand))
                {
                    for (int a = 0; a < functions[y].Commands[x].loop.Count; a++)
                    {
                        printCommandOnBoard(functions[y].Commands[x].loop[a], ref positionBoard, y, transformBoardCommand.transform, false);
                    }
                    printCommandOnBoard(functions[y].Commands[x], ref positionBoard, y, transformBoardCommand.transform, true);
                }

            }
        }
    }

    private void printCommandOnBoard(Command command, ref int positionX, int positionY, Transform transform, bool numberRepeat)
    {
        GameObject toInstantiate, instance;
        if (numberRepeat) { 
            toInstantiate = getNumberLoop(command.numRepeatLoop);
        }
        else
        {
            toInstantiate = getObjectToInstantiate(command.EnumCommand);
        }
        instance = Instantiate(toInstantiate, transform, true) as GameObject;
        instance.transform.localPosition = getPositionCommandInstance(positionX - 3, (positionY * -1) + 1);
        command.gameObject = instance;
        positionX++;
    }

    public IEnumerator terminateMovement(Vector2 positionCollectable)
    {
        while (!playerBody.position.Equals(positionCollectable))
        {
            playerBody.position = Vector3.MoveTowards(playerBody.position, positionCollectable, 0.1f);
            yield return null;
        }
    }



    private GameObject getObjectToInstantiate(string objectType)
    {
        switch (objectType)
        {
            case "Collectable":
                level.collectable++;
                return Collectable;
            case "Floor":
                return Floor;
            case "Obstacle":
                return Obstacle;
            case "Player":
                return Player;
            case "Wall":
                return Wall;
            default:
                return Floor;
        }
    }

    private Vector3 getPositionBoardInstance(int x, int y)
    {
        return new Vector3(x * offsetXBoard, y * offsetYBoard, 0f);
    }

    private Vector3 getPositionCommandInstance(int x, float y)
    {
        return new Vector3(x * offsetXCommand, y * offsetYCommand, 0f);
    }

    private Sprite getSpriteCommandMark(EnumCommand enumCommand)
    {
        switch (enumCommand)
        {
            case EnumCommand.CIRCLE:
                return circleMark;
            case EnumCommand.LOOP:
                return loopMark;
            case EnumCommand.LEFT:
                return leftMark;
            case EnumCommand.RIGHT:
                return rightMark;
            case EnumCommand.MOVE:
                return moveMark;
            default:
                return null;
        }
    }

    public Sprite getNumberMarkLoop(int number)
    {
        switch (number)
        {
            case 2:
                return _2Mark;
            case 3:
                return _3Mark;
            case 4:
                return _4Mark;
            case 5:
                return _5Mark;
            case 6:
                return _6Mark;
            case 7:
                return _7Mark;
            case 8:
                return _8Mark;
            case 9:
                return _9Mark;
            default:
                return null;
        }
    }

    public GameObject getNumberLoop(int number)
    {
        switch (number)
        {
            case 2:
                return _2;
            case 3:
                return _3;
            case 4:
                return _4;
            case 5:
                return _5;
            case 6:
                return _6;
            case 7:
                return _7;
            case 8:
                return _8;
            case 9:
                return _9;
            default:
                return null;
        }
    }

    public GameObject getObjectToInstantiate(EnumCommand command)
    {
        switch (command)
        {
            case EnumCommand.CIRCLE:
                return circle;
            case EnumCommand.CIRCLE_TITLE:
                return circle_title;
            case EnumCommand.STAR:
                return star;
            case EnumCommand.STAR_TITLE:
                return star_title;
            case EnumCommand.TRIANGLE:
                return triangle;
            case EnumCommand.TRIANGLE_TITLE:
                return triangle_title;
            case EnumCommand.LOOP:
                return loop;
            case EnumCommand.LEFT:
                return left;
            case EnumCommand.RIGHT:
                return right;
            case EnumCommand.MOVE:
                return move;
            default:
                return null;
        }
    }

    public Level getLevel()
    {
        return level;
    }

    private string getJsonFileById(string idLevel)
    {
        TextAsset file = Resources.Load("Level-" + idLevel) as TextAsset;
        string json = file.ToString();
        return json;
    }

    public string getCommandsRemaining()
    {
        return string.Format(Messages.LABEL_MOVIMENTOS, commands, level.maxCommands);
    }

    public void setIndex(int indexCircle, int indexStar, int indexTriangle)
    {
        this.indexCircle = indexCircle;
        this.indexStar = indexStar;
        this.indexTriangle = indexStar;
    }
}