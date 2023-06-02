using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace AcessoBD
{
    public partial class frmAcesso : Form
    {
        public frmAcesso()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Método modifica (para DELETE , UPDATE e INSERT)

        private void modifica(String sql)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conexao.abreConexao(); 
            try
            {
                if (MessageBox.Show("Deseja executar esse ação?", "Atenção",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Ação realizada com exito.");
                    }
                    else
                    {
                        MessageBox.Show("Falha ao realizar essa ação.");
                    }
                    cmd.Dispose();
                }

            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.fechaConexao();
            }
        }

        #endregion

        #region Método Pesquisa
        private void metodoPesquisa(String sql)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conexao.abreConexao();
            MySqlDataReader dr;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCodigo.Text = dr["codigo"].ToString();
                    txtNome.Text = dr["nome"].ToString();
                    txtUF.Text = dr["uf"].ToString();
                }
                dr.Close();
                cmd.Dispose();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.fechaConexao();
            }
        }

        #endregion

        private void btnApagar_Click(object sender, EventArgs e)
        {
            String apaga = String.Format(
                "DELETE FROM estados WHERE codigo = {0}",txtCodigo.Text);
            modifica(apaga);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            String sql =  "INSERT INTO estados VALUES('" + txtCodigo.Text + "','" + txtNome.Text + "'," + txtUF.Text + "')";

            String novo = String.Format(" INSERT INTO estados VALUES('{0}','{1}','{2}')", txtCodigo.Text, txtNome.Text, txtUF.Text);
            modifica(novo);
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            String atualiza = String.Format("UPDATE estados SET nome='{0}', uf='{1}' WHERE codigo= '{2}'",
                txtNome.Text, txtUF.Text, txtCodigo.Text);
            modifica(atualiza);
        }
        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            String primeiro = "SELECT * FROM estados LIMIT 1";
            metodoPesquisa(primeiro);
        }
        private void btnUltimo_Click(object sender, EventArgs e)
        {
            String ultimo = "SELECT * FROM estados ORDER BY codigo DESC LIMIT 1";
            metodoPesquisa(ultimo);
        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            String anterior = String.Format("SELECT * FROM estados WHERE codigo < {0} LIMIT 1",txtCodigo.Text);
            metodoPesquisa(anterior);
        }
        private void btnProximo_Click(object sender, EventArgs e)
        {
            String proximo = String.Format("SELECT * FROM estados WHERE codigo > {0} LIMIT 1", txtCodigo.Text);
            metodoPesquisa(proximo);
        }
        private void frmAcesso_Load(object sender, EventArgs e)
        {
            String primeiro = "SELECT * FROM estados LIMIT 1";
            metodoPesquisa(primeiro);
        }
    }
}