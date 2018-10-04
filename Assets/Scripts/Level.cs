[System.Serializable]
public class Level
{
    public string name;
    public string levelID;
    public string author;
    public int difficulty;
    public int collectable;
    public int maxCommands;
    public Board board;
}

[System.Serializable]
public class Board
{
    public DataBoard[] data;
}

[System.Serializable]
public class DataBoard
{
    public int positionX;
    public int positionY;
    public string objectType;
}
