using br.com.DesafioCervantes.aplicacao;
using br.com.DesafioCervantes.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesafioCervantes
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        public void ativaCncelar()
        {
            btnNovo.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
        }

        public void ativaInclusao()
        {
            btnNovo.Enabled = true;
            btnIncluir.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
        }

        public void ativaAlteracao()
        {
            btnNovo.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;
        }

        public void limpaTela()
        {
            txtDescricao.Text = "";
            txtNumero.Text = "";
            txtId.Text = "";
        }
        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            ativaCncelar();
            limpaTela();
            montaGrid();

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ativaInclusao();
            txtId.Text = "";
            txtId.Enabled = false;
            limpaTela();
            txtDescricao.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ativaCncelar();
            txtId.Text = "";
            limpaTela();
        }

        private void montaGrid()
        {
            try
            {
                grdCadastro.Rows.Clear();
                AppCadastro cadastroAPP = new AppCadastro();
                foreach (Cadastro cadastro in cadastroAPP.listar())
                {
                    grdCadastro.Rows.Add(cadastro.campoTexto.ToString(), cadastro.campoNumero.ToString(), cadastro.idCadastro.ToString());
                }
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro: " + erro.Message);
            }
        }

        private void grdCadastro_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtId.Text = grdCadastro.Rows[grdCadastro.CurrentRow.Index].Cells["idCadastro"].Value.ToString();
                txtId.Enabled = false;
                txtId_Validating(null, null);
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro: " + erro.Message);
            }
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtId.Text))
                {
                    AppCadastro appCadastro = new AppCadastro();
                    var cadastro = appCadastro.carregar(Convert.ToInt32(txtId.Text));
                    if (String.IsNullOrEmpty(cadastro.campoTexto))
                    {
                        MessageBox.Show("Cadastro nao encontrado. ");
                    }
                    else
                    {
                        carregaDados(cadastro);
                        ativaAlteracao();
                    }
                }
                
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro: " + erro.Message);
            }
        }

        private void carregaDados(Cadastro cadastro)
        {
            txtId.Text = cadastro.idCadastro.ToString();
            txtDescricao.Text = cadastro.campoTexto.ToString();
            txtNumero.Text = cadastro.campoNumero.ToString();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                var cadastro = montaObjeto();
                AppCadastro cadastroAPP = new AppCadastro();

                cadastroAPP.insert(cadastro);
               

                MessageBox.Show("Cadastro salvo com sucesso!", "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);

                montaGrid();
                limpaTela();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro: " + erro.Message);

            }
        }

        private Cadastro montaObjeto()
        {
            Cadastro cadastro = new Cadastro();
            if (String.IsNullOrEmpty(txtId.Text) || txtId.Equals("0"))
            {
                cadastro.idCadastro = 0;
            }
            else
            {
                cadastro.idCadastro = Convert.ToInt32(txtId.Text);
            }
            cadastro.campoTexto = txtDescricao.Text;
            cadastro.campoNumero = Convert.ToDouble(txtNumero.Text);

            return cadastro;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                var cadastro = montaObjeto();
                AppCadastro cadastroAPP = new AppCadastro();

                cadastroAPP.alter(cadastro);

                MessageBox.Show("Cadastro alterado com sucesso!", "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);

                montaGrid();
                ativaCncelar();
                limpaTela();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro: " + erro.Message);

            }
}

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                var cadastro = montaObjeto();
                AppCadastro cadastroAPP = new AppCadastro();

                cadastroAPP.delete(cadastro);

                MessageBox.Show("Cadastro exculido com sucesso!", "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);

                montaGrid();
                limpaTela();
               
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro: " + erro.Message);

            }
        }
    }
}
