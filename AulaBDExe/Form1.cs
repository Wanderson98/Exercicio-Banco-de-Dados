using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AulaBDExe.Conexao;

namespace AulaBDExe
{
    public partial class Form1 : Form
    {
        Conexoes conexao = new Conexoes();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LimparCampos();
            dataGridView1.DataSource = conexao.BuscarDadosTabela();
            dataGridView1.DataMember = conexao.BuscarDadosTabela().Tables[0].TableName;
            btnDeletar.Enabled = false;
            Atualizar.Enabled = false;

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                int i = conexao.InserirDados(txtNome.Text, int.Parse(txtIdade.Text), txtTelefone.Text, txtEmail.Text);
                if (i > 0)
                {
                    MessageBox.Show("Dados Cadastrado com Sucesso!");
                    Form1_Load(sender, e);
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Preencher todos os dados corretamente");
            }
        }

        private void LimparCampos()
        {
            txtEmail.Text = null;
            txtIdade.Text = null;
            txtNome.Text = null;
            txtTelefone.Text = null;
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                int i = conexao.DeletarDado(id);
                if (i > 0)
                {
                    MessageBox.Show("Dados Deletados com Sucesso!");
                    Form1_Load(sender, e);
                    LimparCampos();
                    btnInserir.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNome.Focus();
            txtNome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtIdade.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelefone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            btnInserir.Enabled = false;
            btnDeletar.Enabled = true;
            Atualizar.Enabled = true;
        }

        private void Atualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                int i = conexao.AtualizarDados(txtNome.Text, int.Parse(txtIdade.Text), txtTelefone.Text, txtEmail.Text, id);
                if (i > 0)
                {
                    MessageBox.Show("Dados Atualizados com Sucesso!");
                    Form1_Load(sender, e);
                    LimparCampos();
                    btnInserir.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Preencher todos os dados corretamente");
            }
        }
    }
}
