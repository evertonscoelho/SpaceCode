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
        return "Select a level";
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

    public override string getDescricaoAjudaMovimentosBasicos1()
    {
        return "\t\t Your mission is to collect all the stars in the map.To do so use the pieces you received and organize them in a sequence of steps that will guide the rocket to the star.\n " +
            "\t\tStart putting one of the geometrica shaped pieces:";
    }
    public override string getDescricaoAjudaMaisFuncoes()
    {
        return "\t\tUff! the universe is really a big place, isn’t it?\n" +
            "\t\tYour path to collect all the stars is growing every time.We need to adopt a new strategy.\n" +
            "\t\tWhat if you break your sequence of steps into small parts? Try to use more than one line to distributed the pieces, creating new functions \n" +
            "\t\tEach line must begin with a different geometric shape that will be the “name” of your function). But don’t forget, you can only have 3 lines. \n" +
            "\t\tThe first line will ever be executed.To make another line be executed, you need to “call the line”, which means, put a piece with the same geometric form that that the one of the function you want to call, at the first line at the position you want it to be executed.\n" +
            "\t\tTake a look at an example:";
    }

    public override string getDescricaoAjudaMovimentosBasicos2()
    {
        return " Them the movement pieces, giving the direction which the rocket must face and when to move.\n";
    }

    public override string getDescricaoAjudaMovimentosBasicos3()
    {
        return "\t\t     After you have finished this job, touch on the camera icon, take a picture of your instructions e watch the result. \n" +
            "\t\tOh, watch out for the walls. Good Luck!";
    }

    public override string getDescricaoAjudaMaisColetaveis()
    {
        return "\t\tCongratulations to have come till here, you are getting it. Lets’s see how you perform with 2 stars to take. \n" +
            "\t\tTake care with the obstacles in your way, if you touch them, you will have to try again. Good Luck!";
    }

    public override string getDescricaoAjudaRepeticao()
    {
        return "\t\tYou will have to be very smart to conquer the next levels. Search for frequent movements and use the repetition piece to make them. \n" +
            "\t\tFollow the order: repetition piece, the movements you want to make, and the number of times you want all thos commands to be repeated. \n" +
            "\t\tBut be aware to the maximum movements you can make and the maximum pieces you can use.\n" +
            "\t\tTake a look at an example:";
    }

    public override string getDescricaoAjudaRecursao()
    {
        return "\t\tCongratulations commandant! You've come a long way, it's time for your last challenge.\n" +
            "\t\tOn the next levels you must continue seeking for repetitive movements, but, this time, you will not solve then with repetition pieces.Try to solve it using functions that call thenselves.\n" +
            "\t\tTake a look at an example:";
    }

    public override string getBotaoBaixar()
    {
        return "Download";
    }

    public override string getDescricaoAjudaBaixar()
    {
        return "\t\tTouch the button to download the set of pieces for this game. \n" +
            "\t\tPrint the pieces and cut them, but take to not cut the borders.\n\n" +
            "\t\tHave fun :D";
    }
}
