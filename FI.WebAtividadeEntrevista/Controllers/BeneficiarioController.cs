using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();
            
            
                if (model.Id > 0)
                {
                    var existe = new BoBeneficiario().Consultar(model.Id);

                    if (existe != null && existe.Cpf == model.Cpf)
                    {
                        return Json("CPF já cadastrado!.");
                    }

                    bo.Alterar(new Beneficiario()
                    {
                        Id = model.Id,
                        Nome = model.Nome,
                        Cpf = model.Cpf
                    });

                    return Json("Cadastro alterado com sucesso");
                }
                else
                {

                    model.Id = bo.Incluir(new Beneficiario()
                    {
                        IdCliente = model.IdCliente,
                        Nome = model.Nome,
                        Cpf = model.Cpf
                    });
                }

           
                return Json("Cadastro efetuado com sucesso");
            
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();
       
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var existe = new BoBeneficiario().Consultar(model.Id);
                
                if (existe != null && existe.Cpf == model.Cpf)
                {
                    return Json("CPF já cadastrado!.");
                }

                bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Cpf = model.Cpf
                });
                               
                return Json("Cadastro alterado com sucesso");
            }
        }


        [HttpDelete]
        public JsonResult Deletar(string cpf)
        {
            BoBeneficiario bo = new BoBeneficiario();
            bo.Excluir(cpf);
            return Json("Beneficiário Excluido com sucesso");
        }
        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    Cpf = cliente.Cpf
                };

            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult BeneficiarioList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Beneficiario> clientes = new BoBeneficiario().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult ListaBeneficiarios(string cpf)
        {
            try
            {
                int qtd = 0;


                List<Beneficiario> clientes = new BoBeneficiario().Pesquisa(0, 10, "NOME", true, out qtd).ToList();

                var retorno = clientes.Where(x => x.IdCliente == int.Parse(cpf)).ToList();
                //Return result to jTable
                //return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}