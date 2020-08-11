using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using br.com.DesafioCervantes.model;
using Npgsql;
namespace br.com.DesafioCervantes.dao
{
    public class CadastroDAO
    {
        public CadastroDAO()
        {

        }

        NpgsqlConnection conexaoBd;

        String conexaoPostgre = @"server=127.0.0.1;Port=5432;User id=postgres;Password=postdba;Database=DesafioCervantesDB";
        
        
        public void insert(Cadastro oCadastro) {
            try
            {
                conexaoBd = new NpgsqlConnection(conexaoPostgre);
                NpgsqlCommand sql = new NpgsqlCommand("insert into cadastro (campo_texto,campo_numero) values (@texto, @numero)", conexaoBd);
                sql.Parameters.AddWithValue("@texto", oCadastro.campoTexto);
                sql.Parameters.AddWithValue("@numero", oCadastro.campoNumero);
                
                conexaoBd.Open();
                sql.ExecuteNonQuery();
            
                insertLog();
            }
            catch (Exception oErro)
            {

                throw oErro;
            }
            finally
            {
                conexaoBd.Close();
            }
        
        
        }
        public void insertLog()
        {
            try
            {
                conexaoBd = new NpgsqlConnection(conexaoPostgre);
                NpgsqlCommand sql = new NpgsqlCommand("insert into logCadastro (operacao) values ('isert' )", conexaoBd);
                conexaoBd.Open();
                sql.ExecuteNonQuery();
            }catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                conexaoBd.Close();
            }
        }
        public void alter(Cadastro oCadastro) {

            try
            {
                conexaoBd = new NpgsqlConnection(conexaoPostgre);
                NpgsqlCommand sql = new NpgsqlCommand("update cadastro set campo_texto=@texto,campo_numero=@numero where id_cadastro= @id", conexaoBd);
                sql.Parameters.AddWithValue("@texto", oCadastro.campoTexto);
                sql.Parameters.AddWithValue("@numero", oCadastro.campoNumero);
                sql.Parameters.AddWithValue("@id", oCadastro.idCadastro);
                conexaoBd.Open();
                sql.ExecuteNonQuery();
                alterLog();
            }
            catch (Exception oErro)
            {

                throw oErro;
            }
            finally
            {
                conexaoBd.Close();
            }
        }

        public void alterLog()
        {
            try
            {
                conexaoBd = new NpgsqlConnection(conexaoPostgre);
                NpgsqlCommand sql = new NpgsqlCommand("insert into logCadastro (operacao) values ('update' )", conexaoBd);
                conexaoBd.Open();
                sql.ExecuteNonQuery();
            }
            catch (Exception oErro)
            {

                throw oErro;
            }
            finally
            {
                conexaoBd.Close();
            }
        }
        public void delete(Cadastro oCadastro) {

            try
            {
                conexaoBd = new NpgsqlConnection(conexaoPostgre);
                NpgsqlCommand sql = new NpgsqlCommand("delete from cadastro where id_cadastro=@id", conexaoBd);
                sql.Parameters.AddWithValue("@id", oCadastro.idCadastro);
                conexaoBd.Open();
                sql.ExecuteNonQuery();
                deleteLog();
            }
            catch (Exception oErro)
            {

                throw oErro;
            }
            finally
            {
                conexaoBd.Close();
            }
        }

        public void deleteLog()
        {
            try
            {
                conexaoBd = new NpgsqlConnection(conexaoPostgre);
                NpgsqlCommand sql = new NpgsqlCommand("insert into logCadastro (operacao) values ('delete' )", conexaoBd);
                conexaoBd.Open();
                sql.ExecuteNonQuery();
            }
            catch (Exception oErro)
            {

                throw oErro;
            }
            finally
            {
                conexaoBd.Close();
            }
        }


        public Cadastro carregar(int idCadastro) {
            try
            {
                conexaoBd = new NpgsqlConnection(conexaoPostgre);
                NpgsqlCommand sql = new NpgsqlCommand("select * from cadastro where id_cadastro=@id ", conexaoBd);
                sql.Parameters.AddWithValue("@id", idCadastro);
                conexaoBd.Open();
                Cadastro oCadastro = new Cadastro();
                NpgsqlDataReader dataReader = sql.ExecuteReader();

                while (dataReader.Read())
                {
                    oCadastro.idCadastro = Convert.ToInt32(dataReader["id_cadastro"]);
                    oCadastro.campoTexto = dataReader["campo_texto"].ToString();
                    oCadastro.campoNumero = Convert.ToDouble(dataReader["campo_numero"]);
                }

                return oCadastro;


            }
            catch (Exception oErro)
            {

                throw oErro;
            }
            finally
            {
                conexaoBd.Close();
            }


        }

        public List<Cadastro> listar() {
            try
            {
                conexaoBd = new NpgsqlConnection(conexaoPostgre);
                NpgsqlCommand sql = new NpgsqlCommand("select * from cadastro", conexaoBd);
                conexaoBd.Open();
                NpgsqlDataReader dataReader = sql.ExecuteReader();

                var listaCadastro =  TransformaReaderEmListaDeObjeto(dataReader);

                return listaCadastro;
                sql.ExecuteNonQuery();
            }
            catch (Exception oErro)
            {

                throw oErro;
            }
            finally
            {
                conexaoBd.Close();
            }

        }

        public List<Cadastro> TransformaReaderEmListaDeObjeto(NpgsqlDataReader retorna)
        {
            var listaCadastro = new List<Cadastro>();
            while (retorna.Read())
            {
                var temObjeto = new Cadastro()
                {
                    idCadastro = Convert.ToInt32(retorna["id_cadastro"]),
                    campoTexto = retorna["campo_texto"].ToString(),
                    campoNumero = Convert.ToDouble(retorna["campo_numero"]),
                };
                listaCadastro.Add(temObjeto);
            }
            retorna.Close();
            return listaCadastro;
        }


    }
}
