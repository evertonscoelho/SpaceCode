using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BoardManager : MonoBehaviour
{
    public GameObject Collectable, Floor, Obstacle, Player, Wall;

    public GameObject player, circle_title, star_title, triangle_title, circle, star, triangle, loop, left, right, move, _2, _3, _4, _5, _6, _7, _8, _9;

    public Sprite circleMark, starMark, triangleMark, loopMark, leftMark, rightMark, moveMark, _2Mark, _3Mark, _4Mark, _5Mark, _6Mark, _7Mark, _8Mark, _9Mark, loopExecute, playerCrashedSprite;

    private List<Function> functionsBoard;

    private float offsetXBoard, offsetYBoard, offsetXCommand, offsetYCommand, speed = 0.01f;

    private GameManager gameManager;
    private GameObject playerObjectLevel;
    private Rigidbody2D playerBody;
    private Transform playerTransform;

    private Level level;
    private int commands = 0, collectables = 0, indexCircle, indexTriangle, indexStar;

    bool endGame = false;

    PlayerDirection playerDirection;

    Command previous = null;

    public void initValues()
    {
        offsetXCommand = circle.GetComponent<Image>().sprite.bounds.size.x;
        offsetYCommand = circle.GetComponent<Image>().sprite.bounds.size.y;
        offsetXBoard = Floor.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        offsetYBoard = Floor.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
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
                if (dataBoard.objectType == "Player")
                {
                    playerObjectLevel = instance;
                    playerBody = instance.GetComponent<Rigidbody2D>();
                    playerTransform = instance.transform;
                    setPlayerDirection(playerDirection);
                }
            }
        }
    }

    private void setPlayerDirection(string direction)
    {
        direction = direction.ToUpper();
        switch (direction)
        {
            case "UP":
                playerTransform.Rotate(new Vector3(0,0,0));
                playerDirection = PlayerDirection.UP;
                break;
            case "DOWN":
                playerDirection = PlayerDirection.DOWN;
                playerTransform.Rotate(new Vector3(0, 0, -180));
                break;
            case "LEFT":
                playerDirection = PlayerDirection.LEFT;
                playerTransform.Rotate(new Vector3(0, 0, 90));
                break;
            case "RIGHT":
                playerDirection = PlayerDirection.RIGHT;
                playerTransform.Rotate(new Vector3(0, 0, -90));
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
        else if (collectables >= level.collectable)
        {
            return StatusGame.VICTORY;
        }
        else
        {
            return StatusGame.CONTINUE;
        }
    }

    public int getDifficultHelp()
    {
        return level.difficulty;
    }

    public IEnumerator execute(List<Function> functions)
    {
        functionsBoard = functions;
        gameManager.setCommands(functions, GameObject.Find("BoardCommand").transform, 40, 40, -7f, 0.8f, false);
        yield return StartCoroutine(DoFunction(functions[0]));
        if (!endGame)
        {
            StartCoroutine(gameManager.doDefeat(false));
        }
    }

    private IEnumerator DoFunction(Function function)
    {
        previous = null;
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
                        yield return new WaitForSeconds(1f);
                        commands--;
                        break;
                    case EnumCommand.STAR_TITLE:
                        yield return new WaitForSeconds(1f);
                        animationCommand(null);
                        commands--;
                        break;
                    case EnumCommand.TRIANGLE_TITLE:
                        yield return new WaitForSeconds(1f);
                        animationCommand(null);
                        commands--;
                        break;
                }
            }
            else
            {
                StartCoroutine(gameManager.doDefeat(false));
            }
        }
        animationCommand(null);
    }

    private IEnumerator Turn(PlayerDirection direction)
    {
        if (PlayerDirection.LEFT.Equals(direction))
        {
            setPlayerDirectionTurnLeft();
            yield return StartCoroutine(rotationLeft());
        }
        else if (PlayerDirection.RIGHT.Equals(direction))
        {
            setPlayerDirectionTurnRight();
            yield return StartCoroutine(rotationRight());
        }
        yield return new WaitForSeconds(1f);

    }

    private IEnumerator rotationRight()
    {
        var fromAngle = playerTransform.rotation;
        var toAngle = Quaternion.Euler(playerTransform.eulerAngles + new Vector3(0, 0, -90));
        for (var t = 0f; t < 1; t += Time.deltaTime / 1)
        {
            playerTransform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    private IEnumerator rotationLeft()
    {
        var fromAngle = playerTransform.rotation;
        var toAngle = Quaternion.Euler(playerTransform.eulerAngles + new Vector3(0, 0, 90));
        for (var t = 0f; t < 1; t += Time.deltaTime / 1)
        {
            playerTransform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    private void setPlayerDirectionTurnRight()
    {
        if (PlayerDirection.UP.Equals(playerDirection))
        {
            playerDirection = PlayerDirection.RIGHT;
        }
        else if (PlayerDirection.DOWN.Equals(playerDirection))
        {
            playerDirection = PlayerDirection.LEFT;
        }
        else if (PlayerDirection.LEFT.Equals(playerDirection))
        {
            playerDirection = PlayerDirection.UP;
        }
        else if (PlayerDirection.RIGHT.Equals(playerDirection))
        {
            playerDirection = PlayerDirection.DOWN;
        }
    }

    private void setPlayerDirectionTurnLeft()
    {
        if (PlayerDirection.UP.Equals(playerDirection))
        {
            playerDirection = PlayerDirection.LEFT;
        }
        else if (PlayerDirection.DOWN.Equals(playerDirection))
        {
            playerDirection = PlayerDirection.RIGHT;
        }
        else if (PlayerDirection.LEFT.Equals(playerDirection))
        {
            playerDirection = PlayerDirection.DOWN;
        }
        else if (PlayerDirection.RIGHT.Equals(playerDirection))
        {
            playerDirection = PlayerDirection.UP;
        }
    }

    private IEnumerator Loop(Command command)
    {
        yield return new WaitForSeconds(1f);
        animationCommand(null);
        Function function = new Function(command.loop);
        animationLoop(command, false);
        for (int x = 0; x < command.numRepeatLoop; x++)
        {
            yield return StartCoroutine(DoFunction(function));
        }
        animationLoop(command, true);
    }

    private void animationLoop(Command command, bool endAnimation)
    {
        if (endAnimation)
        {
            command.gameObject.GetComponent<Image>().sprite = loop.GetComponent<Image>().sprite;
            command.numRepeatLoopGameObject.GetComponent<Image>().sprite = getNumberLoop(command.numRepeatLoop).GetComponent<Image>().sprite;       
        }
        else
        {
            command.gameObject.GetComponent<Image>().sprite = loopExecute;
            command.numRepeatLoopGameObject.GetComponent<Image>().sprite = getNumberMarkLoop(command.numRepeatLoop);
        }
        previous = command;
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
        target = playerBody.position + new Vector2(offsetYBoard, 0);
        while (playerBody.position.x < target.x)
        {
            playerBody.MovePosition(new Vector2(playerBody.position.x + speed, playerBody.position.y));
            yield return null;
        }
        yield return new WaitForSeconds(1);    
    }

    private IEnumerator MoveLeft()
    {
        Vector2 target = playerBody.position - new Vector2(offsetYBoard, 0);
        while (playerBody.position.x > target.x)
        {
            playerBody.MovePosition(new Vector2(playerBody.position.x - speed, playerBody.position.y));
            yield return null;
        }
        yield return new WaitForSeconds(1);
    }

    private IEnumerator MoveDown()
    {
        Vector2 target = playerBody.position - new Vector2(0, offsetXBoard);
        while (playerBody.position.y > target.y)
        {
            playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y - speed));
            yield return null;
        }
        yield return new WaitForSeconds(1);
        
    }

    private IEnumerator MoveUp()
    {
        Vector2 target = playerBody.position + new Vector2(0, offsetYBoard);
        while (playerBody.position.y < target.y)
        {
            playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y + speed));
            yield return null;
        }
        yield return new WaitForSeconds(1);
    }

    private void animationCommand(Command command)
    {
        if (previous != null)
        {
            GameObject instance = getObjectToInstantiate(previous.EnumCommand);
            previous.gameObject.GetComponent<Image>().sprite = instance.GetComponent<Image>().sprite;
        }
        if (command != null)
        {
            command.gameObject.GetComponent<Image>().sprite = getSpriteCommandMark(command.EnumCommand);
        }
        previous = command;
    }

    public IEnumerator terminateMovement(Vector2 positionCollectable)
    {
        while (!playerBody.position.Equals(positionCollectable))
        {
            playerBody.position = Vector3.MoveTowards(playerBody.position, positionCollectable, speed);
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
        float temp = y + 0.4f;
        return new Vector3(x * offsetXBoard, temp * offsetYBoard, 0f);
    }

    public Vector3 getPositionCommandInstance(int x, float y)
    {
        return new Vector3(x * offsetXCommand, y * offsetYCommand, 0f);
    }

    private Sprite getSpriteCommandMark(EnumCommand enumCommand)
    {
        switch (enumCommand)
        {


            case EnumCommand.CIRCLE:
                return circleMark;
            case EnumCommand.CIRCLE_TITLE:
                return circleMark;
            case EnumCommand.STAR:
                return starMark;
            case EnumCommand.STAR_TITLE:
                return starMark;
            case EnumCommand.TRIANGLE:
                return triangleMark;
            case EnumCommand.TRIANGLE_TITLE:
                return triangleMark;
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


    private string getJsonFileById(string idLevel)
    {
        TextAsset file = Resources.Load("Level-" + idLevel) as TextAsset;
        string json = file.ToString();
        return json;
    }

    public string getCommandsRemaining()
    {
        return string.Format(GameManager.instance.messages.getLabelMovimentos(), commands, level.maxCommands);
    }

    public void setIndex(int indexCircle, int indexStar, int indexTriangle)
    {
        this.indexCircle = indexCircle;
        this.indexStar = indexStar;
        this.indexTriangle = indexTriangle;
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

    public int getMaxPiece()
    {
        return level.maxCommandsUse;
    }

    public void playerCrashed()
    {
        playerObjectLevel.GetComponent<SpriteRenderer>().sprite = playerCrashedSprite;
    }
}