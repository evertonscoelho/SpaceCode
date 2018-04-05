using UnityEngine;
using System.IO;

public class BoardManager : MonoBehaviour
{
    public GameObject Collectable;
    public GameObject Floor;
    public GameObject Obstacle;
    public GameObject Player;
    public GameObject Wall;

    void BoardSetup(Board board)
    {
        Transform boardHolder = new GameObject("Board").transform;
        DataBoard dataBoard;
        for (int x = 0; x < board.data.Length; x++)
        {
            dataBoard = board.data[x];
            GameObject floor = Instantiate(Floor, getPositionInstance(dataBoard.positionX, dataBoard.positionY), Quaternion.identity) as GameObject;
            floor.transform.SetParent(boardHolder);
            if (dataBoard.objectType != "Floor")
            {
                GameObject toInstantiate = getObjectToInstantiate(dataBoard.objectType);
                GameObject instance = Instantiate(toInstantiate, getPositionInstance(dataBoard.positionX, dataBoard.positionY), Quaternion.identity) as GameObject;
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
        Level levelObject = JsonUtility.FromJson<Level>(getJsonFileById(idLevel));
        BoardSetup(levelObject.board);
    }

    private string getJsonFileById(string idLevel)
    {
        string path = "Assets/Level/Level-" + idLevel + ".json";
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();
        return json;
    }
}
