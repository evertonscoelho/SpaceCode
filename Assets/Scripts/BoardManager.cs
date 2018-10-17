using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public GameObject Collectable, Floor, Obstacle, Player, Wall;

    public GameObject Up, Down, Left, Right, A, B, C, boardCommand, player, A_title, B_title, C_title;

    public Sprite UpMark, rightMark, leftMark, downMark, AMark, BMark, CMark, A_titleMark, B_titleMark, C_TitleMark;

    private List<Function> functionsBoard;

    private float offsetXBoard, offsetYBoard, offsetXCommand, offsetYCommand;

    private GameManager gameManager;
    private Rigidbody2D playerBody;

    private Level level;
    private int commands = 0, collectables = 0;

    public void initValues()
    {
        offsetXCommand = Up.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetYCommand = Up.GetComponent<SpriteRenderer>().bounds.size.y;
        offsetXBoard = Floor.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetYBoard = Floor.GetComponent<SpriteRenderer>().bounds.size.y;
        gameManager = GameManager.instance;
    }

    public void boardSetup(Board board)
    {
        Transform boardHolder = new GameObject("Board").transform;
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
                }
            }
        }
    }

    public void SetupScene(string idLevel)
    {
        commands = 0;
        collectables = 0;
        this.level = JsonUtility.FromJson<Level>(getJsonFileById(idLevel));
        boardSetup(level.board);
    }

    private string getJsonFileById(string idLevel)
    {
        TextAsset file = Resources.Load("Level-" + idLevel) as TextAsset;
        string json = file.ToString();
        return json;
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

    public string getCommandsRemaining()
    {
        return (level.maxCommands - commands).ToString();
    }

    public void doCommands(List<Function> functions)
    {
        functionsBoard = functions;
        printActionsInBoard(functions);
        StartCoroutine(DoFunction(functions[0]));
    }

    private IEnumerator DoFunction(Function function)
    {
        bool endGame = false, functionCalled = false;
        Command previous = null;
        foreach (Command command in function.Commands)
        {
            bool blocked = false;
            float diff, attempts = 0;
            Vector2 target;
            animationCommand(ref previous, command);
            if (!endGame)
            {
                endGame = gameManager.checkEndGameCommand();
                switch (command.EnumCommand)
                {
                    case EnumCommand.UP:
                        target = playerBody.position + new Vector2(0, offsetXBoard);
                        diff = playerBody.position.y - target.y + 20;
                        while (playerBody.position.y < target.y && attempts < diff)
                        {
                            playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y + 0.1f));
                            attempts += 0.1f;
                            yield return null;
                        }
                        if (attempts >= diff)
                        {
                            blocked = true;
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;
                    case EnumCommand.DOWN:
                        target = playerBody.position - new Vector2(0, offsetXBoard);
                        diff = playerBody.position.y - target.y - 20;
                        while (playerBody.position.y > target.y && attempts > diff)
                        {
                            playerBody.MovePosition(new Vector2(playerBody.position.x, playerBody.position.y - 0.1f));
                            attempts -= 0.1f;
                            yield return null;
                        }
                        if (attempts <= diff)
                        {
                            blocked = true;
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;
                    case EnumCommand.LEFT:
                        target = playerBody.position - new Vector2(offsetYBoard, 0);
                        diff = playerBody.position.x - target.x - 20;
                        while (playerBody.position.x > target.x && attempts > diff)
                        {
                            playerBody.MovePosition(new Vector2(playerBody.position.x - 0.1f, playerBody.position.y));
                            attempts -= 0.1f;
                            yield return null;
                        }
                        if (attempts <= diff)
                        {
                            blocked = true;
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;
                    case EnumCommand.RIGHT:
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
                            blocked = true;
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;
                    case EnumCommand.A:
                        yield return new WaitForSeconds(1f);
                        animationCommand(ref previous, null);
                        functionCalled = true;
                        yield return StartCoroutine(DoFunction(functionsBoard[0]));
                        break;
                    case EnumCommand.B:
                        yield return new WaitForSeconds(1f);
                        animationCommand(ref previous, null);
                        functionCalled = true;
                        if (functionsBoard.Count > 1)
                        {
                            yield return StartCoroutine(DoFunction(functionsBoard[1]));
                        }
                        else
                        {
                            this.StopAllCoroutines();
                            gameManager.showErro(Messages.ERRO_FUNCAO_B_NAO_EXISTE, true, false);
                            endGame = true;
                        }
                        break;
                    case EnumCommand.C:
                        yield return new WaitForSeconds(1f);
                        animationCommand(ref previous, null);
                        functionCalled = true;
                        if (functionsBoard.Count > 2)
                        {
                            yield return StartCoroutine(DoFunction(functionsBoard[2]));
                        }
                        else
                        {
                            this.StopAllCoroutines();
                            gameManager.showErro(Messages.ERRO_FUNCAO_C_NAO_EXISTE, true, false);
                            endGame = true;
                        }
                        break;
                    case EnumCommand.A_TITLE:
                        functionCalled = true;
                        commands--;
                        break;
                    case EnumCommand.B_TITLE:
                        functionCalled = true;
                        commands--;
                        break;
                    case EnumCommand.C_TITLE:
                        functionCalled = true;
                        commands--;
                        break;
                }
                if (!endGame || functionCalled || !blocked)
                {
                    yield return new WaitForSeconds(1);
                }
            }
            else
            {
                gameManager.doDefeat();
            }
        }
        if (!endGame)
        {
            gameManager.doDefeat();
        }
    }

    private void animationCommand(ref Command previous, Command command)
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

    private Sprite getSpriteCommandMark(EnumCommand enumCommand)
    {
        switch (enumCommand)
        {
            case EnumCommand.UP:
                return UpMark;
            case EnumCommand.DOWN:
                return downMark;
            case EnumCommand.LEFT:
                return leftMark;
            case EnumCommand.RIGHT:
                return rightMark;
            case EnumCommand.A:
                return AMark;
            case EnumCommand.B:
                return BMark;
            case EnumCommand.C:
                return CMark;
            case EnumCommand.A_TITLE:
                return A_titleMark;
            case EnumCommand.B_TITLE:
                return B_titleMark;
            case EnumCommand.C_TITLE:
                return C_TitleMark;
            default:
                return null;
        }
    }

    public void printActionsInBoard(List<Function> functions)
    {
        boardCommand = GameObject.Find("BoardCommand");
        Transform transformBoardCommand = boardCommand.transform;
        GameObject toInstantiate, instance;
        for (int y = 0; y < functions.Count; y++)
        {
            for (int x = 0; x < functions[y].Commands.Count; x++)
            {
                toInstantiate = getObjectToInstantiate(functions[y].Commands[x].EnumCommand);
                instance = Instantiate(toInstantiate, transformBoardCommand.transform, true) as GameObject;
                instance.transform.localPosition = getPositionCommandInstance(x - 3, (y * -1) + 1);
                functions[y].Commands[x].gameObject = instance;
            }
        }
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
            case EnumCommand.A_TITLE:
                return A_title;
            case EnumCommand.B_TITLE:
                return B_title;
            case EnumCommand.C_TITLE:
                return C_title;
            default:
                return null;
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

}