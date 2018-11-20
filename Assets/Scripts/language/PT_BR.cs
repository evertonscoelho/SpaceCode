public class PT_BR : Messages
{

    public override string getNomeJogo()
    {
        return "hue hue \nbrbr";
    }

    public override string getErroNenhumaCameraEncontrada()
    {
        return "Não encontrei nenhuma câmera";
    }

    public override string getErroAbrirCamera()
    {
        return "Não consegui abrir a câmera";
    }

    public override string getErroNenhumComandoReconhecido()
    {
        return "Não reconheci nenhum comando";
    }

    public override string getErroLinhasInvalidas()
    {
        return "Só consigo ler até {0} linhas de comandos";
    }

    public override string getErroQuantidadeComandos()
    {
        return "Nessa fase só são permitidos {0} comandos, e você colocou {1}";
    }

    public override string getErroAoReconhecerComando()
    {
        return "Não reconheci os comandos: Comando inválido";
    }

    public override string getErroServidor()
    {
        return "Não consegui conectar ao servidor: {1}. Ocorreu o erro ({0})";
    }

    public override string getErroProblemaConexao()
    {
        return "Ocorreu um problema de conexão com o servidor: ";
    }

    public override string getPrimeiroComandoLinha()
    {
        return "O primeiro comando da linha {0} deve ser uma função";
    }

    public override string getErroFuncaoDefinidaDuasVezes()
    {
        return "A função {0} foi definida em duas ou mais linhas";
    }

    public override string getErroFuncaoNaoImplementada()
    {
        return "A função {0} foi chamada, mas não implementada";
    }
    public override string getErroLoop()
    {
        return "Na função {0} existe uma repetição definida de forma incorreta";
    }
    public override string getErroNumero()
    {
        return "Na função {0} existe um número sem comando de repetição";
    }

    public override string getErroLoopSemComando()
    {
        return "Na função {0} existe uma repetição sem nenhum comando";
    }

    public override string getTituloPainelComandos()
    {
        return "Eu reconheci essas peças, é isso mesmo?";
    }

    public override string getTituloPainelErro()
    {
        return "Alguma coisa deu errada :/";
    }

    public override string getTituloPainelFimJogoVitoria()
    {
        return "É isso aí! :)";
    }

    public override string getTituloPainelFimJogoDerrota()
    {
        return "Dessa vez não deu :/";
    }

    public override string getTituloPainelAjuda()
    {
        return "Ajuda";
    }

    public override string getTituloTelaSelecao()
    {
        return "Qual fase quer jogar?";
    }

    public override string getTituloTelaFases()
    {
        return "Fase {0}";
    }

    public override string getTituloBoardComandos()
    {
        return "Peças usadas";
    }

    public override string getLabelMovimentos()
    {
        return "Movimentos {0}/{1}";
    }

    public override string getLabelCarregando()
    {
        return "Carregando...";
    }

    public override string getBotaoFases()
    {
        return "Ver as fases";
    }
    public override string getBotaoTentarNovamente()
    {
        return "Tentar de novo";
    }

    public override string getBotaoProximaFase()
    {
        return "Próxima fase";
    }

    public override string getBotaoOk()
    {
        return "OK";
    }

    public override string getBotaoSim()
    {
        return "Sim";
    }

    public override string getNaoBotaoTentarNovamente()
    {
        return "Não, vou tentar de novo";
    }

    public override string getDescricaoAjudaSobreJogo()
    {
        return "Teste Lorem ipsum dolor sit amet, consectetur adipiscing elit.Mauris lacinia consectetur erat quis tincidunt.Phasellus faucibus urna venenatis, tincidunt ex at, pulvinar nunc. Sed et metus in risus mattis faucibus.Fusce id pulvinar est, id rhoncus dui.Quisque in scelerisque neque. Etiam ut porta odio, et hendrerit massa.Vivamus maximus hendrerit ipsum et elementum. Mauris tempor tempor mi, ullamcorper dignissim lectus accumsan et.Phasellus congue euismod lorem, non pellentesque nulla venenatis ut.Nullam malesuada tellus nec augue facilisis bibendum.Nunc ultrices, orci vitae accumsan venenatis, quam magna dapibus erat, eu posuere sapien lectus et sapien. Sed ut dolor id enim pretium aliquam ac non purus. Curabitur et urna accumsan, dapibus purus nec, tristique ligula. Duis quis elit sed metus venenatis imperdiet nec ac libero. Nulla feugiat imperdiet hendrerit. Phasellus leo nunc, auctor id velit sit amet, posuere congue metus.";
    }

    public override string getDescricaoAjudaSobreFase()
    {
        return "fase";
    }

    public override string getMensagemUltimaFase()
    {
        return "Huhul, você passou todas as fases :)";
    }

    public override string getFuncaoCirculo()
    {
        return "círculo";
    }

    public override string getFuncaoEstrela()
    {
        return "estrela";
    }

    public override string getFuncaoTriangulo()
    {
        return "triângulo";
    }

    public override string getPortugues()
    {
        return "Português";
    }

    public override string getIngles()
    {
        return "Inglês";
    }

    public override string getTituloPainelEscolherLinguagem()
    {
        return "Escolha a \nlíngua";
    }

    public override string getMaxUse()
    {
        return "Use no máximo {0} peças";
    }
}