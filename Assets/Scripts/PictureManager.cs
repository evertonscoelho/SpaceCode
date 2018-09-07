using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public static PictureManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public Function[] pictureClick()
    {
        StartCoroutine(RequestManager.request());
        Function[] functions = testClass();
        return functions;
    }

    public Function[] testClass()
    {
        Function[] functions = { new Function(), new Function(), new Function() };

        functions[0].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.UP, EnumCommand.LEFT, EnumCommand.RIGHT, EnumCommand.RIGHT };
        //functions[0].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.DOWN, EnumCommand.F2, EnumCommand.F1 };
        functions[1].Commands = new EnumCommand[] { EnumCommand.DOWN, EnumCommand.F3, EnumCommand.LEFT };
        functions[2].Commands = new EnumCommand[] { EnumCommand.RIGHT, EnumCommand.UP };

        //functions[0].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.F2, EnumCommand.F3 };
        //functions[1].Commands = new EnumCommand[] { EnumCommand.UP, EnumCommand.UP };
        //functions[2].Commands = new EnumCommand[] { EnumCommand.LEFT, EnumCommand.DOWN };
        return functions;
    }
}

