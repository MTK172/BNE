using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CRUD.Repositorio.Contrato;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Departamento> _departamentoRepository;
        private readonly IGenericRepository<Empregado> _empregadoRepository;

        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<Departamento> departamentoRepository, IGenericRepository<Empregado> empregadoRepository)
        {
            _logger = logger;
            _departamentoRepository = departamentoRepository;
            _empregadoRepository = empregadoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task< IActionResult > ListaDepartamentos()
        {
            List<Departamento> _lista = await _departamentoRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, _lista);

        }

        [HttpGet]
        public async Task< IActionResult > ListaEmpregados()
        {
            List<Empregado> _lista = await _empregadoRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, _lista);

        }

        [HttpPost]
        public async Task<IActionResult> GuardarEmpregado([FromBody] Empregado modelo)
        {
            bool _resultado = await _empregadoRepository.Guardar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "erro" });
        }


        [HttpPut]
        public async Task<IActionResult> EditarEmpregado([FromBody] Empregado modelo)
        {
            bool _resultado = await _empregadoRepository.Editar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "erro" });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarEmpregeado(int idEmpregado)
        {
            bool _resultado = await _empregadoRepository.Eliminar(idEmpregado);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "erro" });
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}