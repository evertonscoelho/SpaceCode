using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecognizeCommandManager : MonoBehaviour
{
    public static RecognizeCommandManager instance = null;
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

    public void takePictureClick()
    {
        cameraViewManager.loading();
        //byte[] bytes = phoneCamera.TakePhoto();
        byte[] bytes = null;
        request(bytes);
    }

    public void setCameraViewManager(CameraViewManager cameraViewManager)
    {
        this.cameraViewManager = cameraViewManager;
    }

    public void request(byte[] bytes)
    {
        StartCoroutine(RequestManager.Request(bytes, this));
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
            GameManager.instance.showErro(response);
        }
    }

    public void convertToCommand(string response)
    {
        if (response.ToUpper().Equals("UNKNOW"))
        {
            GameManager.instance.showErro(Messages.NENHUM_COMANDO_RECONHECIDO);
        }
        response = response.ToUpper();
        string[] commands = response.Split(',');
        List<Function> functions= new List<Function>();
        int line = 1;
        bool firstCommandInLine = true;
        List<Command> commandsLine = new List<Command>();
        bool error = false;

        foreach (string command in commands)
        {
            if (error)
            {
                break;
            }
            else
            {
                if (firstCommandInLine)
                {
                    error = firstCommandLineCheck(line, command);
                    firstCommandInLine = false;
                    if (!error)
                    {
                        if(line == 1)
                        {
                            commandsLine.Add(new Command(EnumCommand.A_TITLE));
                        }
                        else if(line == 2)
                        {
                            commandsLine.Add(new Command(EnumCommand.B_TITLE));
                        }
                        else
                        {
                            commandsLine.Add(new Command(EnumCommand.C_TITLE));
                        }
                    }
                }
                else if (command.Equals("NEXT"))
                {
                    if(line == 3)
                    {
                        GameManager.instance.showErro(Messages.LINHAS_INVALIDAS);
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
            GameManager.instance.recognizeCommand(functions);
        }
    }

    private bool firstCommandLineCheck(int line, string command)
    {
        if (line == 1)
        {
            if (!command.Equals("A"))
            {
                GameManager.instance.showErro(Messages.PRIMEIRA_LINHA_INAVLIDA);
                return true;
            }
        }
        else if (line == 2)
        {
            if (!command.Equals("B"))
            {
                GameManager.instance.showErro(Messages.SEGUNDA_LINHA_INAVLIDA);
                return true;
            }
        }
        else if (line == 3)
        {
            if (!command.Equals("C"))
            {
                GameManager.instance.showErro(Messages.TERCEIRA_LINHA_INAVLIDA);
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