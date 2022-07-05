using System.Data.SqlClient;
using System.Text;

namespace modulo2_semana6_tests;

public class ExemploTest
{
    [Fact]
    public void Exemplo_Metodod_InputTrue_ReturnFalse()
    {
        var result = true;
        Assert.True(result, "Erro no método");
    }

    [Fact]
    public void Aula_01_Exemplo_Conversao_String_Para_Numero()
    {
        string mensagem = "OLÁ MUNDO !";
        int numero = Convert.ToInt32(mensagem);
    }

    /// <summary>
    /// Exemplo aula 01
    /// </summary>
    /// <exception cref="Exception"></exception>
    [Fact]
    public void Aula_01_Criar_Arquivo_Texto()
    {
        StringBuilder stringBuilder = new();

        stringBuilder!.AppendLine("AULA 01");
        stringBuilder!.AppendLine($"DATA E HORA {DateTime.Now}");

        var file = $@"MUDAR-O-DIRETORIO\Aula-{DateTime.Now.ToString("HHmmss")}.txt";
        string mensagem = "";

        // Escrever o arquivo
        try
        {
            //using (StreamWriter streamWriter = new StreamWriter(file))
            //{
            //    streamWriter.WriteLine(stringBuilder);
            //    streamWriter.Flush();
            //}

            StreamWriter streamWriter = new StreamWriter(file);
            streamWriter.WriteLine(stringBuilder);

            // Para simular CustomErrorException comentar a linha  streamWriter = null;
            // Para simular Exception comentar as tres linhas 
            streamWriter.Flush();
            streamWriter.Close();
            streamWriter = null;

            if (streamWriter != null)
            {
                throw new CustomErrorException($"ERRO NO ARQUIVO TEXTO, FECHAR CONEXÃO DO {nameof(streamWriter)}. AULA 01");
            }

            // Ler o arquivo
            using (StreamReader reader = new StreamReader(file))
            {
                mensagem = reader.ReadToEnd();
            }
        }
        catch (CustomErrorException exCustom)
        {
            mensagem = exCustom.Message;
        }
        catch (Exception ex)
        {
            mensagem = ex.StackTrace;
            throw new Exception("Erro ao escrever aquirov.", ex);
        }
        finally
        {
            Assert.True(!string.IsNullOrEmpty(mensagem), "Erro na declaração da var mensagem");
        }
    }

    [Serializable]
    public class CustomErrorException : Exception
    {
        public CustomErrorException()
        {
        }

        public CustomErrorException(string mensagemErro) : base(mensagemErro)
        {
        }
    }

}