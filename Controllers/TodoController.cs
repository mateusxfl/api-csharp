using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Models;

namespace api.Controllers
{
    [ApiController] // Como podemos ter controladores para sites ou APIs, é interessante especificar por meio do atributo [ApiController].
    [Route("v1")] // Quando trabalhamos com APIs é importante versioná-las, pois o front-end esta estritamente ligado com a mesma.
    public class TodoController : ControllerBase
    {
        //  O controlador dentro do MVC é o item que vai receber a requisição, manipular ela e devolvê-la para a tela.

        // Action
        [HttpGet]
        [Route("todos")]
        public List<Todo> Get(){
            return new List<Todo>();
        }
    }
}