using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public GameObject boardCommand;

    public void testClass()
    {
        EnumMovement[] movements = { EnumMovement.UP, EnumMovement.RIGHT, EnumMovement.LEFT, EnumMovement.DOWN };
        doMovementCommand(movements);
    }

    public void doMovementCommand(EnumMovement[] movements)
    {
        EnumMovement movement;
        BoardCommandManager boardCommandManager = boardCommand.GetComponent<BoardCommandManager>(); ;

        boardCommandManager.printMovements(movements);
        for (int i = 0; i < movements.Length; i++)
        {
            movement = movements[i];
            boardCommandManager.animationMoviment(i);
            this.doMovement(movement);
            new WaitForSeconds(1);
        }
    }

    private void doMovement(EnumMovement movement)
    {
        switch (movement)
        {
            case EnumMovement.UP:
                int teste = 1 + 1;
                break;
            default:
                teste = 2 + 2;
                break;
        }
    }
}
