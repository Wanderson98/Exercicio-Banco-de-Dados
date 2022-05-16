using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace AulaBDExe.Conexao
{
    internal class Conexoes
    {
        static string conectionString = @"Data Source=LAPTOP-FSNQLFT0\SQLEXPRESS;Initial Catalog=BDRevisao;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(conectionString);


        private void AbriConexao()
        {
            sqlConnection.Open();

        }
        private void FecharConexao()
        {
            sqlConnection.Close();
        }

        public DataSet BuscarDadosTabela()
        {
            AbriConexao();
            SqlCommand cmd = new SqlCommand("SELECT Pessoaid as Id, nomepessoa as Nome, IdadePessoa as Idade, TelefonePessoa as Telefone, EmailPessoa as Email FROM Pessoa;", sqlConnection);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            FecharConexao();
            return ds;
        }

        public int InserirDados(string name, int idade, string telefone, string email)
        {
            AbriConexao();
            string sql = $"INSERT INTO Pessoa (NomePessoa, IdadePessoa, TelefonePessoa, EmailPessoa) Values ('{name}',{idade} , '{telefone}','{email}');";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.CommandType = CommandType.Text;

            int i = cmd.ExecuteNonQuery();
            FecharConexao();
            return i;


        }

        public int DeletarDado(int id)
        {
            AbriConexao();
            string sql = $"Delete from Pessoa WHERE PessoaId = {id} ";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.CommandType = CommandType.Text;
            int i = cmd.ExecuteNonQuery();
            FecharConexao();
            return i;
        }

        public int AtualizarDados(string name, int idade, string telefone, string email, int id)
        {
            AbriConexao();
            string sql = $"Update Pessoa set NomePessoa = '{name}', IdadePessoa = {idade}, TelefonePessoa ='{telefone}', EmailPessoa = '{email}' WHERE PessoaID = {id}";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.CommandType = CommandType.Text;
            int i = cmd.ExecuteNonQuery();
            FecharConexao();
            return i;
        }
    }
}

