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
            GameObject floor = Instantiate(Floor, new Vector3(dataBoard.positionX, dataBoard.positionY, 0f), Quaternion.identity) as GameObject;
            floor.transform.SetParent(boardHolder);

            if (dataBoard.objectType != "Floor")
            {
                GameObject toInstantiate = getObjectToInstantiate(dataBoard.objectType);

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance = Instantiate(toInstantiate, new Vector3(dataBoard.positionX, dataBoard.positionY, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }
        }

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
