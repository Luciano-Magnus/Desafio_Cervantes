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
        }
        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            ativaCncelar();
            limpaTela();

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ativaInclusao();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ativaCncelar();
            txtDescricao.Text = "";
            txtNumero.Text = "";
        }
    }
}
