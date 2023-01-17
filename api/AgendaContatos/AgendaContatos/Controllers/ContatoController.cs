using AgendaContatos.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AgendaContatos.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ContatoController : Controller
  {
    private readonly IConfiguration _configuration;
    public ContatoController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
      string query = @"
                      select Id, Nome, Email,
                      Celular 
                      from
                      dbo.Contatos
                      ";

      DataTable tabela = new DataTable();
      string sqlDataSource = _configuration.GetConnectionString("DataBase");
      SqlDataReader myReader;
      using (SqlConnection myCon = new SqlConnection(sqlDataSource))
      {
        myCon.Open();
        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        {
          myReader = myCommand.ExecuteReader();
          tabela.Load(myReader);
          myReader.Close();
          myCon.Close();
        }
      }

      return new JsonResult(tabela);
    }

    [HttpPost]
    public JsonResult Post(Contato contato)
    {
      string query = @"
                      insert into dbo.Contatos
                      (Nome,Email,Celular)
               values (@Nome,@Email,@Celular)
                      ";

      DataTable tabela = new DataTable();
      string sqlDataSource = _configuration.GetConnectionString("DataBase");
      SqlDataReader myReader;
      using (SqlConnection myCon = new SqlConnection(sqlDataSource))
      {
        myCon.Open();
        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        {
          myCommand.Parameters.AddWithValue("@Nome", contato.Nome);
          myCommand.Parameters.AddWithValue("@Email", contato.Email);
          myCommand.Parameters.AddWithValue("@Celular", contato.Celular);
          myReader = myCommand.ExecuteReader();
          tabela.Load(myReader);
          myReader.Close();
          myCon.Close();
        }
      }

      return new JsonResult("Adicionado com sucesso");
    }

    [HttpPut]
    public JsonResult Put(Contato contato)
    {
      string query = @"
                      update dbo.Contatos
                      set Nome = @Nome,
                        Email = @Email,
                        Celular = @Celular
                      where Id = @Id
                      ";

      DataTable tabela = new DataTable();
      string sqlDataSource = _configuration.GetConnectionString("DataBase");
      SqlDataReader myReader;
      using (SqlConnection myCon = new SqlConnection(sqlDataSource))
      {
        myCon.Open();
        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        {
          myCommand.Parameters.AddWithValue("@Id", contato.Id);
          myCommand.Parameters.AddWithValue("@Nome", contato.Nome);
          myCommand.Parameters.AddWithValue("@Email", contato.Email);
          myCommand.Parameters.AddWithValue("@Celular", contato.Celular);
          myReader = myCommand.ExecuteReader();
          tabela.Load(myReader);
          myReader.Close();
          myCon.Close();
        }
      }

      return new JsonResult("Atualizado com sucesso");
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
      string query = @"
                      delete from dbo.Contatos
                      where Id = @Id
                      ";

      DataTable tabela = new DataTable();
      string sqlDataSource = _configuration.GetConnectionString("DataBase");
      SqlDataReader myReader;
      using (SqlConnection myCon = new SqlConnection(sqlDataSource))
      {
        myCon.Open();
        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        {
          myCommand.Parameters.AddWithValue("@Id", id);
          myReader = myCommand.ExecuteReader();
          tabela.Load(myReader);
          myReader.Close();
          myCon.Close();
        }
      }

      return new JsonResult("Deletado com sucesso");
    }

  }
}
