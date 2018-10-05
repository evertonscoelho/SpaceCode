﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureManager : MonoBehaviour
{
    public static PictureManager instance = null;
    private CameraViewManager cameraViewManager;
    PhoneCamera phoneCamera;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void pictureClick()
    {
       // phoneCamera = new PhoneCamera(cameraViewManager.GetComponent<RawImage>());
        cameraViewManager.active();
    }

    public List<Function> takePictureClick()
    {
       // byte[] bytes = phoneCamera.TakePhoto();
        cameraViewManager.deactivate();
        byte[] bytes = null;
        return request(bytes);
    }

    public void setCameraViewManager(CameraViewManager cameraViewManager)
    {
        this.cameraViewManager = cameraViewManager;
    }

    public List<Function> request(byte[] bytes)
    {
        string request = RequestManager.request(bytes);
        List<Function> functions = convertToCommand(request);
        return functions;
    }

    private List<Function> convertToCommand(string response)
    {
        if (response.ToUpper().Equals("UNKNOW"))
        {
            throw new System.InvalidOperationException(Messages.NENHUM_COMANDO_RECONHECIDO);
        }
        response = response.ToUpper();
        string[] commands = response.Split(',');
        List<Function> functions= new List<Function>();
        int line = 1;
        bool firstCommandInLine = true;
        List<EnumCommand> commandsLine = new List<EnumCommand>(); 

        foreach (string command in commands)
        {
            if (firstCommandInLine)
            {
                firstCommandLineCheck(line, command);
                firstCommandInLine = false;
            }
            else if (command.Equals("NEXT"))
            {
                if(line == 3)
                {
                    throw new System.InvalidOperationException(Messages.LINHAS_INVALIDAS);
                }
                line = line + 1;
                functions.Add(new Function(commandsLine));
                commandsLine = new List<EnumCommand>();
                firstCommandInLine = true;
            }
            else
            {
                commandsLine.Add(getCommand(command));
            }
        }
        functions.Add(new Function(commandsLine));
        return functions;
    }

    private static void firstCommandLineCheck(int line, string command)
    {
        if (line == 1)
        {
            if (!command.Equals("A"))
            {
                throw new System.InvalidOperationException(Messages.PRIMEIRA_LINHA_INAVLIDA);
            }
        }
        else if (line == 2)
        {
            if (!command.Equals("B"))
            {
                throw new System.InvalidOperationException(Messages.SEGUNDA_LINHA_INAVLIDA);
            }
        }
        else if (line == 3)
        {
            if (!command.Equals("C"))
            {
                throw new System.InvalidOperationException(Messages.TERCEIRA_LINHA_INAVLIDA);
            }
        }
    }

    public EnumCommand getCommand(string command)
    {
        switch (command)
        {
            case "UP":
                return EnumCommand.UP;
            case "DOWN":
                return EnumCommand.DOWN;
            case "LEFT":
                return EnumCommand.LEFT;
            case "RIGHT":
                return EnumCommand.RIGHT;
            case "A":
                return EnumCommand.A;
            case "B":
                return EnumCommand.B;
            case "C":
                return EnumCommand.C;
            default:
                return EnumCommand.UNKNOW;
        }

    }
}