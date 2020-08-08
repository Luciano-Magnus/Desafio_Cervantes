using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using br.com.DesafioCervantes.dao;
using br.com.DesafioCervantes.model;

namespace br.com.DesafioCervantes.aplicacao
{
    public class AppCadastro
    {
        public AppCadastro() { }

        CadastroDAO cadastroDAO;

        public void insert(Cadastro cadastro) 
        {
            try
            {
                cadastroDAO = new CadastroDAO();
                cadastroDAO.insert(cadastro);
            }
            catch (Exception Erro)
            {

                throw Erro;
            }
        }
        public void alter(Cadastro cadastro) 
        {
            try
            {
                cadastroDAO = new CadastroDAO();
                cadastroDAO.alter(cadastro);
            }
            catch (Exception Erro)
            {

                throw Erro;
            }
        }
        public void delete(Cadastro cadastro) 
        {
            try
            {
                cadastroDAO = new CadastroDAO();
                cadastroDAO.delete(cadastro);
            }
            catch (Exception Erro)
            {

                throw Erro;
            }
        }

        public Cadastro carregar(int idCadastro) 
        {
            Cadastro cadastro;
            try
            {
                cadastroDAO = new CadastroDAO();
                cadastro =  cadastroDAO.carregar(idCadastro);
            }
            catch (Exception Erro)
            {

                throw Erro;
            }

            return cadastro;
        }

        public List<Cadastro> listar() 
        {
            try
            {
                cadastroDAO = new CadastroDAO();
                return cadastroDAO.listar();
            }
            catch (Exception Erro)
            {

                throw Erro;
            }
        }

        
    }
}
