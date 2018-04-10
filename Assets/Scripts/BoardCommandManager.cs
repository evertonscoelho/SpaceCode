using System;
using UnityEngine;

public class BoardCommandManager : MonoBehaviour
{

    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;
    public GameObject boardCommand;

    public void printMovements(EnumMovement[] movements)
    {
        Transform transformBoardCommand = boardCommand.transform;
        for (int i = 0; i < movements.Length; i++)
        {
            GameObject toInstantiate = getObjectToInstantiate(movements[i]);
            GameObject instance = Instantiate(toInstantiate, getPositionInstance(0, i), Quaternion.identity) as GameObject;
            instance.transform.SetParent(transformBoardCommand);
        }
    }

    private Vector3 getPositionInstance(int x, int y)
    {
        float offsetX = Up.GetComponent<SpriteRenderer>().bounds.size.x;
        float offsetY = Up.GetComponent<SpriteRenderer>().bounds.size.y;
        return new Vector3(x * offsetX, y * offsetY, 0f);
    }

    public void animationMoviment(int i)
    {
      //TODO
    }

    private GameObject getObjectToInstantiate(EnumMovement movement)
    {
        switch (movement)
        {
            case EnumMovement.UP:
                return Up;
            case EnumMovement.DOWN:
                return Down;
            case EnumMovement.LEFT:
                return Left;
            case EnumMovement.RIGHT:
                return Right;
            default:
                return null;
        }
    }
}
