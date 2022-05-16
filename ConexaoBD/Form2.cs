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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string conectionString = @"Data Source=LAPTOP-FSNQLFT0\SQLEXPRESS;Initial Catalog=BDRevisao;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conectionString);


            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente;", sqlConnection);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);

            dataGridView1.DataSource = ds;

            dataGridView1.DataMember = ds.Tables[0].TableName;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); 
            txtLocal.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            string conectionString = @"Data Source=LAPTOP-FSNQLFT0\SQLEXPRESS;Initial Catalog=BDRevisao;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conectionString);

            string sql = $"Delete from Cliente WHERE ClienteId = {int.Parse(txtID.Text)} ";

            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.CommandType = CommandType.Text;
            sqlConnection.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Dado Excluido com Sucesso!");
                    txtNome.Text = null;
                    txtLocal.Text = null;
                    txtEmail.Text = null;
                    txtNome.Focus();
                    Form2_Load(sender, e);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Dado Não Excluido!");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string conectionString = @"Data Source=LAPTOP-FSNQLFT0\SQLEXPRESS;Initial Catalog=BDRevisao;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conectionString);

            string sql = $"Update cliente set NomeCliente = '{txtNome.Text}, Localizacao = '{txtLocal.Text}', Email = '{txtEmail.Text}' WHERE ClienteID = {int.Parse(txtID.Text)}; ";

            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.CommandType = CommandType.Text;
            sqlConnection.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Dado Atualizados com Sucesso!");
                    txtNome.Text = null;
                    txtLocal.Text = null;
                    txtEmail.Text = null;
                    txtNome.Focus();
                    Form2_Load(sender, e);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Dados Não Atualizados!");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
