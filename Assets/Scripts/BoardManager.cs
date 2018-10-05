using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject Collectable, Floor, Obstacle, Player, Wall;

    private Level level;
    private int commands = 0, collectables = 0;

    void BoardSetup(Board board)
    {
        Transform boardHolder = new GameObject("Board").transform;
        GameObject floor, toInstantiate, instance;
        foreach (DataBoard dataBoard in board.data)
        {
            floor = Instantiate(Floor, getPositionInstance(dataBoard.positionX, dataBoard.positionY), Quaternion.identity) as GameObject;
            floor.transform.SetParent(boardHolder);
            if (dataBoard.objectType != "Floor")
            {
                toInstantiate = getObjectToInstantiate(dataBoard.objectType);
                instance = Instantiate(toInstantiate, getPositionInstance(dataBoard.positionX, dataBoard.positionY), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    private Vector3 getPositionInstance(int x, int y)
    {
        float offsetX = Floor.GetComponent<SpriteRenderer>().bounds.size.x;
        float offsetY = Floor.GetComponent<SpriteRenderer>().bounds.size.y;
        return new Vector3(x * offsetX, y * offsetY, 0f);
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

    public void SetupScene(string idLevel)
    {
        commands = 0;
        collectables = 0;
        this.level = JsonUtility.FromJson<Level>(getJsonFileById(idLevel));
        BoardSetup(level.board);
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
        if (commands > level.maxCommands)
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
}