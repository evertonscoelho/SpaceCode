public class EN_US : Messages{

    public override string getNomeJogo()
    {
        return "hue hue \nbrbr";
    }

    public override string getErroNenhumaCameraEncontrada()
    {
        return "Nenhuma câmera encontrada";
    }

    public override string getErroAbrirCamera()
    {
        return "Não foi possível abrir a câmera";
    }

    public override string getErroNenhumComandoReconhecido()
    {
        return "Nenhum comando reconhecido";
    }

    public override string getErroLinhasInvalidas()
    {
        return "É permitido apenas {0} linhas de comandos";
    }

    public override string getErroQuantidadeComandos()
    {
        return "São permitidos para essa fase apenas {0} comandos, e você colocou {1}";
    }

    public override string getErroAoReconhecerComando()
    {
        return "Ocorreu um erro ao reconhecer os comandos: Comando inválido";
    }

    public override string getErroServidor()
    {
        return "Ocorreu um erro ({0}) ao tentar conectar ao servidor: {1}";
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
        return "Na função {0} existe um loop definido de forma incorreta";
    }
    public override string getErroNumero()
    {
        return "Na função {0} existe um número sem comando de loop";
    }

    public override string getErroLoopSemComando()
    {
        return "Na função {0} existe um loop sem nenhum comando";
    }

    public override string getTituloPainelComandos()
    {
        return "Eu reconheci essas peças, estão corretas?";
    }

    public override string getTituloPainelErro()
    {
        return "Ocorreu um erro :/";
    }

    public override string getTituloPainelFimJogoVitoria()
    {
        return "Ganhou :)";
    }

    public override string getTituloPainelFimJogoDerrota()
    {
        return "Perdeu :/";
    }

    public override string getTituloPainelAjuda()
    {
        return "Ajuda";
    }

    public override string getTituloTelaSelecao()
    {
        return "Escolha a fase";
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
        return "Fases";
    }
    public override string getBotaoTentarNovamente()
    {
        return "Tentar Novamente";
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
        return "Não, tentar novamente";
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
        return "triangulo";
    }
}
