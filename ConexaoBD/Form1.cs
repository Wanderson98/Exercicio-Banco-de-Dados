using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexaoBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conectionString = @"Data Source=LAPTOP-FSNQLFT0\SQLEXPRESS;Initial Catalog=BDRevisao;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conectionString);
           

                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente;", sqlConnection);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand= cmd;
                da.Fill(ds);

                dataGridView1.DataSource = ds;

                dataGridView1.DataMember = ds.Tables[0].TableName;

         
        }

        private void btnLimparDGV_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
        }

        private void btnCarregarDGV_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            string conectionString = @"Data Source=LAPTOP-FSNQLFT0\SQLEXPRESS;Initial Catalog=BDRevisao;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conectionString);

            string sql = $"insert into cliente ( NomeCliente, Localizacao, Email) values ( '{txtNome.Text}',  '{txtLocal.Text}', '{txtEmail.Text}'); ";

            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.CommandType = CommandType.Text;
            sqlConnection.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if(i > 0)
                {
                    MessageBox.Show("Dados Cadastrado com Sucesso!");
                    txtNome.Text = null;
                    txtLocal.Text = null;
                    txtEmail.Text = null;
                    txtNome.Focus();
                    Form1_Load(sender, e);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Dados Não Cadastrados!");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnAbreForm2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
