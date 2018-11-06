﻿using System;
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
        response("star,loop,move,star,7,next,circle,loop,move,2,NEXT,triangle,right", false);
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
        bool error = false, firstCommandInLine = true, refCircle = false, refStar = false, refTriangle = false, loop = false; 
        if (response.ToUpper().Equals("UNKNOWN"))
        {
            GameManager.instance.showErro(Messages.ERRO_NENHUM_COMANDO_RECONHECIDO, false, true);
            error = true;
        }
        response = response.ToUpper();
        string[] commands = response.Split(',');
        List<Function> functions= new List<Function>();
        int line = 0, indexCircle = -1, indexStar = -1, indexTriangle = -1;
        Command commandLoop = new Command(EnumCommand.LOOP);
        List<Command> commandsLine = new List<Command>(), commandsLoop = new List<Command>();
        Command command;

        foreach (string commandString in commands)
        {
            if (!error)
            {
                command = getCommand(commandString);
                if (firstCommandInLine)
                {
                    error = firstCommandLineCheckError(command.EnumCommand, ref indexCircle, ref indexStar, ref indexTriangle, line);
                    firstCommandInLine = false;
                    commandsLine.Add(title(command.EnumCommand));
                }
                else if (commandString.Equals("NEXT"))
                {
                    error = nextLineCheckError(line, loop);
                    line++;
                    functions.Add(new Function(commandsLine));
                    commandsLine = new List<Command>();
                    firstCommandInLine = true;
                }
                else if (command.EnumCommand.Equals(EnumCommand.LOOP))
                {
                    if (loop)
                    {
                        GameManager.instance.showErro(String.Format(Messages.ERRO_LOOP, line), false, true);
                        error = true;
                    }
                    else
                    {
                        loop = true;
                        commandLoop = command;
                        commandsLoop = new List<Command>();
                    }
                }
                else if (command.EnumCommand.Equals(EnumCommand.UNKNOW) && commandNumber(commandString))
                {
                    if (loop)
                    {
                        if (commandsLoop.Count > 0) { 
                            commandLoop.loop = commandsLoop;
                            commandLoop.numRepeatLoop = numRepeat(commandString);
                            loop = false;
                            commandsLine.Add(commandLoop);
                        }
                        else
                        {
                            GameManager.instance.showErro(String.Format(Messages.ERRO_LOOP_SEM_COMANDO, line), false, true);
                            error = true;
                        }
                    }
                    else
                    {
                        GameManager.instance.showErro(String.Format(Messages.ERRO_NUMERO, line), false, true);
                        error = true;
                    }
                }
                else if (command.EnumCommand.Equals(EnumCommand.UNKNOW))
                {
                    GameManager.instance.showErro(Messages.ERRO_AO_RECONHECER_COMANDO, false, true);
                    error = true;
                }
                else
                {
                    if (loop)
                    {
                        commandsLoop.Add(command);
                    }
                    else { 
                        commandsLine.Add(command);
                    }
                }
                refFuncion(command.EnumCommand, ref refCircle, ref refTriangle, ref refStar);
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

    private int numRepeat(string commandString)
    {
        int r = -1;
        int.TryParse(commandString, out r);
        return r;
    }

    private bool commandNumber(string commandString)
    {
        int num = numRepeat(commandString);
        return (num > 1 && num <= 9);   
    }

    private void refFuncion(EnumCommand enumCommand, ref bool refCircle, ref bool refTriangle, ref bool refStar)
    {
        if (!refCircle && enumCommand.Equals(EnumCommand.CIRCLE))
        {
            refCircle = true;
        }
        else if (!refTriangle && enumCommand.Equals(EnumCommand.TRIANGLE))
        {
            refTriangle = true;
        }
        else if (!refStar && enumCommand.Equals(EnumCommand.STAR))
        {
            refStar = true;
        }
    }

    private bool nextLineCheckError(int line, bool loop)
    {
        if (line == 2)
        {
            GameManager.instance.showErro(String.Format(Messages.ERRO_LINHAS_INVALIDAS, line+1), false, true);
            return true;
        }
        if (loop)
        {
            GameManager.instance.showErro(String.Format(Messages.ERRO_LOOP, line), false, true);
            return true;
        }
        return false;
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

    public Command getCommand(string command)
    {
        switch (command)
        {
            case "CIRCLE":
                return new Command(EnumCommand.CIRCLE);
            case "STAR":
                return new Command(EnumCommand.STAR);
            case "TRIANGLE":
                return new Command(EnumCommand.TRIANGLE);
            case "LOOP":
                return new Command(EnumCommand.LOOP);
            case "LEFT":
                return new Command(EnumCommand.LEFT);
            case "RIGHT":
                return new Command(EnumCommand.RIGHT);
            case "MOVE":
                return new Command(EnumCommand.MOVE);
            default:
                return new Command(EnumCommand.UNKNOW);
        }
    }
}