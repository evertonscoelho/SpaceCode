public class EN_US : Messages
{

    public override string getNomeJogo()
    {
        return "hue hue \nbrbr";
    }

    public override string getErroNenhumaCameraEncontrada()
    {
        return "Haven't found any camera";
    }

    public override string getErroAbrirCamera()
    {
        return "Can't open your camera";
    }

    public override string getErroNenhumComandoReconhecido()
    {
        return "Could not recognize any command";
    }

    public override string getErroLinhasInvalidas()
    {
        return "I can only read {0} command lines";
    }

    public override string getErroQuantidadeComandos()
    {
        return "This level allows only {0} commands, you put {1}";
    }

    public override string getErroAoReconhecerComando()
    {
        return "Couldn`t recognize the commands: Invalid Command";
    }

    public override string getErroServidor()
    {
        return "Couldn`t connect to server: {1}. Error ({0})";
    }

    public override string getErroProblemaConexao()
    {
        return "A problem happened with the server connection: ";
    }

    public override string getPrimeiroComandoLinha()
    {
        return "The first command on line {0} must be a function";
    }

    public override string getErroFuncaoDefinidaDuasVezes()
    {
        return "The function {0} was defined on two or more lines";
    }

    public override string getErroFuncaoNaoImplementada()
    {
        return "The function {0} was called, but not implemented";
    }
    public override string getErroLoop()
    {
        return "There is an incorrect defined loop on line {0}";
    }

    public override string getErroNumero()
    {
        return "There is a number without any loop command on function {0}";
    }

    public override string getErroLoopSemComando()
    {
        return "There is a loop without any command on function {0}";
    }

    public override string getTituloPainelComandos()
    {
        return "I recognize this commands, am I correct?";
    }

    public override string getTituloPainelErro()
    {
        return "Something went wrong :/";
    }

    public override string getTituloPainelFimJogoVitoria()
    {
        return "That's it! :)";
    }

    public override string getTituloPainelFimJogoDerrota()
    {
        return "Not this time :/";
    }

    public override string getTituloPainelAjuda()
    {
        return "Help";
    }

    public override string getTituloTelaSelecao()
    {
        return "Which level do you want to play?";
    }

    public override string getTituloTelaFases()
    {
        return "Level {0}";
    }

    public override string getTituloBoardComandos()
    {
        return "Used commands";
    }

    public override string getLabelMovimentos()
    {
        return "Moves {0}/{1}";
    }

    public override string getLabelCarregando()
    {
        return "Loading...";
    }

    public override string getBotaoFases()
    {
        return "Go to levels";
    }
    public override string getBotaoTentarNovamente()
    {
        return "Try again";
    }

    public override string getBotaoProximaFase()
    {
        return "Next level";
    }

    public override string getBotaoOk()
    {
        return "OK";
    }

    public override string getBotaoSim()
    {
        return "Yes";
    }

    public override string getNaoBotaoTentarNovamente()
    {
        return "No, let's try again";
    }

    public override string getDescricaoAjudaSobreJogo()
    {
        return "Teste Lorem ipsum dolor sit amet, consectetur adipiscing elit.Mauris lacinia consectetur erat quis tincidunt.Phasellus faucibus urna venenatis, tincidunt ex at, pulvinar nunc. Sed et metus in risus mattis faucibus.Fusce id pulvinar est, id rhoncus dui.Quisque in scelerisque neque. Etiam ut porta odio, et hendrerit massa.Vivamus maximus hendrerit ipsum et elementum. Mauris tempor tempor mi, ullamcorper dignissim lectus accumsan et.Phasellus congue euismod lorem, non pellentesque nulla venenatis ut.Nullam malesuada tellus nec augue facilisis bibendum.Nunc ultrices, orci vitae accumsan venenatis, quam magna dapibus erat, eu posuere sapien lectus et sapien. Sed ut dolor id enim pretium aliquam ac non purus. Curabitur et urna accumsan, dapibus purus nec, tristique ligula. Duis quis elit sed metus venenatis imperdiet nec ac libero. Nulla feugiat imperdiet hendrerit. Phasellus leo nunc, auctor id velit sit amet, posuere congue metus.";
    }

    public override string getDescricaoAjudaSobreFase()
    {
        return "level";
    }

    public override string getMensagemUltimaFase()
    {
        return "Huhul, you've reached the end :)";
    }

    public override string getFuncaoCirculo()
    {
        return "circle";
    }

    public override string getFuncaoEstrela()
    {
        return "star";
    }

    public override string getFuncaoTriangulo()
    {
        return "triangle";
    }

    public override string getPortugues()
    {
        return "Portuguese";
    }

    public override string getIngles()
    {
        return "English";
    }

    public override string getTituloPainelEscolherLinguagem()
    {
        return "Chose a  \nlanguage";
    }

    public override string getMaxUse()
    {
        return "Use a maximum of {0} pieces";
    }
}
