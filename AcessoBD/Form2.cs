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
    public partial class Form2 : Form
    {
        public Form2()
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




        #region botão de Pesquisa
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
                    string sexo = dr["sexo"].ToString();

                    if (sexo == "F")
                    {
                        rdbFeminino.Checked = true;
                    }
                    else
                    {
                        rdbMasculino.Checked = true;
                    }
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
                "DELETE FROM clientes WHERE codigo = {0}", txtCodigo.Text);
            modifica(apaga);
        }

        private void btnNovo_Click(object sender, EventArgs e)

        {
            if (rdbFeminino.Checked)
            {
                String novo = String.Format(" INSERT INTO clientes VALUES('{0}','{1}','{2}')", txtCodigo.Text, txtNome.Text, rdbFeminino.Text);
                modifica(novo);
            }
            else
            {
                string novo2 = String.Format(" insert into clientes values('{0}','{1}','{2}')", txtCodigo.Text, txtNome.Text, rdbMasculino.Text);
                modifica(novo2);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

            if (rdbFeminino.Checked)
            {
                String atualiza = String.Format("UPDATE clientes SET nome='{0}', sexo='{1}' WHERE codigo= '{2}'",
                txtNome.Text, rdbFeminino.Text, txtCodigo.Text);
                modifica(atualiza);
            }
            else
            {
                String atualiza2 = String.Format("UPDATE clientes SET nome='{0}', sexo='{1}' WHERE codigo= '{2}'",
                txtNome.Text, rdbMasculino.Text, txtCodigo.Text);
                modifica(atualiza2);
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }


        private void Form2_Load_1(object sender, EventArgs e)
        {
            
                String primeiro = "SELECT * FROM clientes LIMIT 1";
                metodoPesquisa(primeiro);
            
        }

        private void btnProximo_Click_1(object sender, EventArgs e)
        {
            String proximo = String.Format("SELECT * FROM clientes WHERE codigo > {0} LIMIT 1", txtCodigo.Text);
            metodoPesquisa(proximo);
        }

        private void btnAnterior_Click_1(object sender, EventArgs e)
        {
            String anterior = String.Format("SELECT * FROM clientes WHERE codigo < {0} LIMIT 1", txtCodigo.Text);
            metodoPesquisa(anterior);
        }

        private void btnUltimo_Click_1(object sender, EventArgs e)
        {
            String ultimo = "SELECT * FROM clientes ORDER BY codigo DESC LIMIT 1";
            metodoPesquisa(ultimo);
        }

        private void btnPrimeiro_Click_1(object sender, EventArgs e)
        {
            String primeiro = "SELECT * FROM clientes LIMIT 1";
            metodoPesquisa(primeiro);
        }
    }
}
