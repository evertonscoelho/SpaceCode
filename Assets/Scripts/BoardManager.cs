using UnityEngine;
using System.Collections.Generic;       //Allows us to use Lists.
using System.IO;

public class BoardManager : MonoBehaviour
{
    public GameObject Collectable;              //Prefab to spawn for Collectable.
    public GameObject Floor;                    //Prefab to spawn for Floor.
    public GameObject Obstacle;                 //Prefab to spawn for Obstacle.
    public GameObject Player;                   //Prefab to spawn for Player.
    public GameObject Wall;                     //Prefab to spawn for Wall.

    private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.

    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup(Board board)
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;

        DataBoard dataBoard;

        for (int x = 0; x < board.data.Length; x++)
        {
            dataBoard = board.data[x];
            GameObject floor = Instantiate(Floor, getPositionInstance(dataBoard.positionX, dataBoard.positionY), Quaternion.identity) as GameObject;
            // floor.transform.SetParent(boardHolder);

            if (dataBoard.objectType != "Floor")
            {
                GameObject toInstantiate = getObjectToInstantiate(dataBoard.objectType);

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance = Instantiate(toInstantiate, getPositionInstance(dataBoard.positionX, dataBoard.positionY), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
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
          //      float width = Floor.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
            //    float height = Floor.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f;
              //  Vector3 scale = new Vector3(width, height, 0f);
              //  Player.GetComponent<SpriteRenderer>().transform.localScale = scale;
                return Player;
            case "Wall":
                return Wall;
            default:
                return Floor;
        }
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(string idLevel)
    {
        Level levelObject = JsonUtility.FromJson<Level>(getJsonFileById(idLevel));
        //Creates the outer walls and floor.

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
