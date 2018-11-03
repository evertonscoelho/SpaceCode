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
        response("loop,6,5,4,2,9,NEXT,star,right,right,left,left,3,NEXT,triangle,move,triangle,circle,move,8,NEXT,circle,move,star,loop,7", false);
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
            GameManager.instance.showErro(Messages.ERRO_NENHUM_COMANDO_RECONHECIDO, false, true);
            error = true;
        }
        response = response.ToUpper();
        string[] commands = response.Split(',');
        List<Function> functions= new List<Function>();
        int line = 1, indexCircle = -1, indexStar = -1, indexTriangle = -1;
        bool firstCommandInLine = true, refCircle = false, refStar = false, refTriangle = false;
        List<Command> commandsLine = new List<Command>();
        EnumCommand enumCommand;

        foreach (string commandString in commands)
        {
            if (!error)
            {
                enumCommand = getCommand(commandString);
                if (firstCommandInLine)
                {
                    error = firstCommandLineCheckError(enumCommand, ref indexCircle, ref indexStar, ref indexTriangle, line);
                    firstCommandInLine = false;
                    commandsLine.Add(title(enumCommand));
                }
                else if (commandString.Equals("NEXT"))
                {
                    if(line == 3)
                    {
                        GameManager.instance.showErro(String.Format(Messages.ERRO_LINHAS_INVALIDAS, line), false, true);
                        error = true;
                    }
                    line++;
                    functions.Add(new Function(commandsLine));
                    commandsLine = new List<Command>();
                    firstCommandInLine = true;
                }
                else if (enumCommand.Equals(EnumCommand.CIRCLE))
                {
                    refCircle = true;
                }
                else if (enumCommand.Equals(EnumCommand.TRIANGLE))
                {
                    refTriangle = true;
                }
                else if (enumCommand.Equals(EnumCommand.STAR))
                {
                    refStar = true;
                }
                else if (enumCommand.Equals("UNKNOW"))
                {
                    GameManager.instance.showErro(Messages.ERRO_AO_RECONHECER_COMANDO, false, true);
                    error = true;
                }
                else
                {
                    commandsLine.Add(new Command(enumCommand));
                }
            }
        }
        if (!error)
        {
            functions.Add(new Function(commandsLine));
            if (!checkErrorCommandUse(functions, line) && !checkErrorFucntionReference(indexCircle, indexTriangle, indexStar, refCircle, refStar, refTriangle))
            { 
                GameManager.instance.recognizeCommand(functions, indexCircle, indexStar, indexTriangle);
            }
        }
    }

    private bool checkErrorFucntionReference(int indexCircle, int indexTriangle, int indexStar, bool refCircle, bool refStar, bool refTriangle)
    {
        if (refCircle)
        {
            if(indexCircle == -1)
            {
                GameManager.instance.showErro(String.Format(Messages.ERRO_FUNCAO_NAO_IMPLEMENTADA, Messages.FUNCAO_CIRCULO), false, true);
                return true;
            }
        }
        if (refStar)
        {
            if (indexStar == -1)
            {
                GameManager.instance.showErro(String.Format(Messages.ERRO_FUNCAO_NAO_IMPLEMENTADA, Messages.FUNCAO_ESTRELA), false, true);
                return true;
            }
        }
        if (refTriangle)
        {
            if (indexTriangle == -1)
            {
                GameManager.instance.showErro(String.Format(Messages.ERRO_FUNCAO_NAO_IMPLEMENTADA, Messages.FUNCAO_TRIANGULO), false, true);
                return true;
            }
        }
        return false;
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
            GameManager.instance.showErro(String.Format(Messages.ERRO_QUANTIDADE_COMANDOS, maxCommandsUse, commands), false, true);
            return true;  
        }
        return false;
    }

    private Command title(EnumCommand enumCommand)
    {
        if (enumCommand.Equals(EnumCommand.CIRCLE))
        {
            return new Command(EnumCommand.CIRCLE_TITLE);
        }
        else if (enumCommand.Equals(EnumCommand.TRIANGLE))
        {
            return new Command(EnumCommand.TRIANGLE_TITLE);
        }
        else
        {
            return new Command(EnumCommand.STAR_TITLE);
        }
    }

    private bool firstCommandLineCheckError(EnumCommand enumCommand, ref int indexCircle, ref int indexStar, ref int indexTriangle, int line)
    {
        if (EnumCommand.CIRCLE.Equals(enumCommand))
        {
            if(indexCircle > -1)
            {
                GameManager.instance.showErro(String.Format(Messages.ERRO_FUNCAO_DEFINIDA_DUAS_VEZES, Messages.FUNCAO_CIRCULO), false, true);
                return true;
            }
            else
            {
                indexCircle = line;
            }
        }else if (EnumCommand.TRIANGLE.Equals(enumCommand))
        {
            if (indexTriangle > -1)
            {
                GameManager.instance.showErro(String.Format(Messages.ERRO_FUNCAO_DEFINIDA_DUAS_VEZES, Messages.FUNCAO_TRIANGULO), false, true);
                return true;
            }
            else
            {
                indexTriangle = line;
            }
        }
        else if (EnumCommand.STAR.Equals(enumCommand))
        {
            if (indexStar > -1)
            {
                GameManager.instance.showErro(String.Format(Messages.ERRO_FUNCAO_DEFINIDA_DUAS_VEZES, Messages.FUNCAO_ESTRELA), false, true);
                return true;
            }
            else
            {
                indexStar = line;
            }
        }
        else
        {
            GameManager.instance.showErro(String.Format(Messages.ERRO_PRIMEIRO_COMANDO_LINHA, line), false, true);
            return true;
        }
        return false;
    }

    public EnumCommand getCommand(string command)
    {
        switch (command)
        {
            case "CIRCLE":
                return EnumCommand.CIRCLE;
            case "STAR":
                return EnumCommand.STAR;
            case "TRIANGLE":
                return EnumCommand.TRIANGLE;
            case "LOOP":
                return EnumCommand.LOOP;
            case "LEFT":
                return EnumCommand.LEFT;
            case "RIGHT":
                return EnumCommand.RIGHT;
            case "MOVE":
                return EnumCommand.MOVE;
            default:
                return EnumCommand.UNKNOW;
        }
    }
}