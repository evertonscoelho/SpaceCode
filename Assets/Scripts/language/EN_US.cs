public class EN_US : Messages
{

    public override string getNomeJogo()
    {
        return "Space" +
            "\nCode";
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

    public override string getDescricaoAjudaSobreJogo()
    {
        return "\t\tWelcome young commandant, you are about to enter on a journey through the universe in the pursuit of knowledge. Are you ready to take the controls of our powerful rocket and take all the stars you find? \n" +
            "\t\tTo guide our vehicle command him step by step, using the pieces you received, and collect all the stars you find.\n" +
            "\t\tGet ready and start to conquer the galaxy!\n";
    }

    public override string getDescricaoAjudaMovimentosBasicos()
    {
        return "\t\tSua missão é coletar todas as estrelas espalhadas pelo mapa. Para isso, utilize as peças que você recebeu para montar a sequência de passos que levará o foguete até a estrela.\n " +
            "\t\tComece colocando uma das peças com formas geométricas, e em sequência utilize as peças de movimentação, indicando para que lado o foguete deve se virar e quando ele deve se mover. \n" +
            "\t\tDepois, clique na câmera, tire uma foto das peças e veja o resultado. \n" +
            "\t\tAh, cuidado para não tocar nas paredes do caminho.Boa sorte!";
    }

}
