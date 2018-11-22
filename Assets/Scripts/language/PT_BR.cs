public class PT_BR : Messages
{

    public override string getNomeJogo()
    {
        return "Space" +
            "\nCode";
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

    public override string getDescricaoAjudaSobreJogo()
    {
        return "\t\tBem vindo jovem Comandante, você está prestes a embarcar em uma aventura pelo espaço em busca de conhecimento. Está pronto para assumir os controles do nosso poderoso foguete e pegar todas as estrelas que encontrar? \n" +
            "\t\tPara guiar nosso veículo, diga passo a passo o que ele deve fazer utilizando as peças que você recebeu, e recolha todas as estrelas que encontrar pelo mapa.\n" +
            "\t\tPrepare suas peças e comece a conquistar a galáxia!\n";
    }

    public override string getDescricaoAjudaMovimentosBasicos1()
    {
        return "\t\tSua missão é coletar todas as estrelas espalhadas pelo mapa. Para isso, utilize as peças que você recebeu para montar a sequência de passos que levará o foguete até a estrela.\n " +
            "\t\tComece colocando uma das peças com formas geométricas:";
            
    }

    public override string getDescricaoAjudaMovimentosBasicos2()
    {
        return "Em sequência utilize as peças de movimentação, indicando para que lado o foguete deve se virar e quando ele deve se mover:\n";
    }

    public override string getDescricaoAjudaMovimentosBasicos3()
    {
        return "\t\tDepois, clique na câmera, tire uma foto das peças e veja o resultado. \n" +
            "\t\tAh, cuidado para não tocar nas paredes do caminho. Boa sorte!";
    }

    public override string getDescricaoAjudaMaisColetaveis()
    {
        return "\t\tParabéns por chegar até aqui, você já está pegando o jeito. Vamos ver como você se sai agora com 2 estrelas para pegar. \n" +
            "\t\tCuidado com os obstáculos pelo caminho, se o foguete tocar neles, precisará tentar novamente. Boa sorte!";
    }

    public override string getDescricaoAjudaMaisFuncoes()
    {
        return "\t\tUfa! O universo é um lugar bem grande, não é mesmo? \n" +
            "\t\tO seu caminho para buscar todas as estrelas está ficando cada vez maior. Precisamos adotar uma nova estratégia. \n" +
            "\t\tQue tal se você quebrar suas sequências de comando em pequenos pedaços? Experimente utilizar mais de uma linha para distribuir as peças, criando novas funções. \n" +
            "\t\tCada linha deve começar por uma figura geométrica diferente, que vai ser o 'nome' da função. Mas lembre-se: você pode ter no máximo 3 linhas. \n" +
            "\t\tA primeira linha sempre será executada. Para que outra linha seja executada, é preciso 'chamar a linha', quer dizer, colocar uma peça com a figura que é o nome da função na primeira linha, na posição que você quiser executá - la. \n" +
            "\t\tVeja o Exemplo: ";
    }

    public override string getDescricaoAjudaRepeticao()
    {
        return "\t\tNos próximos desafios você precisará ser esperto. Procure por movimentos frequentes e utilize a peça de repetição para fazê-los. \n" +
            "\t\tColoque, na ordem: a peça de repetição, os movimentos a serem repetidos e, ao final, a quantidade de vezes que isto deve ser feito. \n" +
            "\t\tMas fique atento, nesses desafios há um número máximo de peças que você poderá utilizar.\n" +
            "\t\tVeja o Exemplo:";
    }

    public override string getDescricaoAjudaRecursao()
    {
        return "\t\tMuito bem! Você percorreu um longo caminho Comandante, está na hora do último desafio. \n" +
            "\t\tNas próximas fases você deve continuar procurando por movimentos repetitivos que podem ser feitos, porém, desta vez, ao invés de utilizar as peças de repetição utilize funções que chamam a elas mesmas. \n" +
            "\t\tVeja o Exemplo:";
    }
}