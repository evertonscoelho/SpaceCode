using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecognizeCommandManager : MonoBehaviour
{
    public static RecognizeCommandManager instance = null;
    private CameraViewModalManager cameraViewManager;
    private int maxCommandsUse;
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
       //phoneCamera = new PhoneCamera(cameraViewManager.GetComponent<RawImage>());
       cameraViewManager.active();
    }

    public void takePictureClick(int maxCommandsUse)
    {
        cameraViewManager.loading();
        //byte[] bytes = phoneCamera.TakePhoto();
        this.maxCommandsUse = maxCommandsUse;
        response("A,LEFT,DOWN,B,NEXT,B,DOWN,UP,C,DOWN,C,UP,UP,UP,UP,UP,UP", false);
        //StartCoroutine(RequestManager.Request(bytes, this));
    }

    public void setCameraViewManager(CameraViewModalManager cameraViewManager)
    {
        this.cameraViewManager = cameraViewManager;
    }


    public void response(string response, bool error)
    {
        cameraViewManager.deactivate();
        if (!error)
        {
            convertToCommand(response);
        }
        else
        {
            GameManager.instance.showErro(response, false, true);
        }
    }

    public void convertToCommand(string response)
    {
        bool error = false;
        if (response.ToUpper().Equals("UNKNOWN"))
        {
            GameManager.instance.showErro(Messages.NENHUM_COMANDO_RECONHECIDO, false, true);
            error = true;
        }
        response = response.ToUpper();
        string[] commands = response.Split(',');
        List<Function> functions= new List<Function>();
        int line = 1;
        bool firstCommandInLine = true;
        List<Command> commandsLine = new List<Command>();

        foreach (string command in commands)
        {
            if (!error)
            {
                if (firstCommandInLine)
                {
                    error = firstCommandLineCheck(line, command);
                    firstCommandInLine = false;
                    commandsLine.Add(title(line));
                }
                else if (command.Equals("NEXT"))
                {
                    if(line == 3)
                    {
                        GameManager.instance.showErro(Messages.LINHAS_INVALIDAS, false, true);
                        error = true;
                    }
                    line = line + 1;
                    functions.Add(new Function(commandsLine));
                    commandsLine = new List<Command>();
                    firstCommandInLine = true;
                }
                else
                {
                    commandsLine.Add(new Command(getCommand(command)));
                }
            }
        }
        if (!error)
        {
            functions.Add(new Function(commandsLine));
            if (!checkErrorCommandUse(functions, line))
            { 
            GameManager.instance.recognizeCommand(functions);
            }
        }
    }

    private bool checkErrorCommandUse(List<Function> functions, int line)
    {
        int commands = line * -1;
        for(int x = 0; x < line; x++)
        {
            commands += functions[x].Commands.Count;
        }
        if (maxCommandsUse < commands)
        { 
            string message = String.Format(Messages.ERRO_QUANTIDADE_COMANDOS, maxCommandsUse, commands);
            GameManager.instance.showErro(message, false, true);
            return true;  
        }
        return false;
    }

    private Command title(int line)
    {
        if (line == 1)
        {
            return new Command(EnumCommand.A_TITLE);
        }
        else if (line == 2)
        {
            return new Command(EnumCommand.B_TITLE);
        }
        else
        {
            return new Command(EnumCommand.C_TITLE);
        }
    }

    private bool firstCommandLineCheck(int line, string command)
    {
        if (line == 1)
        {
            if (!command.Equals("A"))
            {
                GameManager.instance.showErro(Messages.PRIMEIRA_LINHA_INAVLIDA, false, true);
                return true;
            }
        }
        else if (line == 2)
        {
            if (!command.Equals("B"))
            {
                GameManager.instance.showErro(Messages.SEGUNDA_LINHA_INAVLIDA, false, true);
                return true;
            }
        }
        else if (line == 3)
        {
            if (!command.Equals("C"))
            {
                GameManager.instance.showErro(Messages.TERCEIRA_LINHA_INAVLIDA, false, true);
                return true;
            }
        }
        return false;
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