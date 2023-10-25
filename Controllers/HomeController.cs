# pragma warning disable CS8629 // El tipo de valor anulable puede ser nulo.
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;

namespace Dojodachi.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("Energia") == null)
        {
            HttpContext.Session.SetInt32("Saciedad", 20);
            HttpContext.Session.SetInt32("Felicidad", 20);
            HttpContext.Session.SetInt32("Comidas", 3);
            HttpContext.Session.SetInt32("Energia", 50);
            ViewBag.Message = "Bienvenido a Dojodachi!";
            ViewBag.Jugando = "Jugando";
        }
        else if (HttpContext.Session.GetInt32("Energia") > 0)
        {
            HttpContext.Session.SetInt32("Saciedad", 20);
            HttpContext.Session.SetInt32("Felicidad", 20);
            HttpContext.Session.SetInt32("Comidas", 3);
            HttpContext.Session.SetInt32("Energia", 50);
            ViewBag.Message = "Bienvenido a Dojodachi!";
            ViewBag.Jugando = "Jugando";
        }
        return View();
    }

    [HttpPost]
    [Route("Dojodachi")]
    public IActionResult Dojodachi(string operacion)
    {
        ViewBag.Jugando = "Jugando";
        if (operacion == null)
        {
            return RedirectToAction("Index");
        }
        switch (operacion)
        {
            case "Alimentar":
                if (HttpContext.Session.GetInt32("Comidas") > 0)
                {
                    Random random = new Random();
                    int? comidas = HttpContext.Session.GetInt32("Comidas");
                    comidas--;
                    HttpContext.Session.SetInt32("Comidas", (int)comidas);
                    int probabilidad = random.Next(1, 5);
                    if (probabilidad == 1)

                    {
                        ViewBag.Message = $"No me gusta la comida, mi saciedad no aumentó y ahora tengo {comidas} comidas";
                    }
                    else
                    {
                        int saciedad = random.Next(5, 11);
                        int? saciedadActual = HttpContext.Session.GetInt32("Saciedad");
                        saciedadActual += saciedad;
                        HttpContext.Session.SetInt32("Saciedad", (int)saciedadActual);
                        ViewBag.Message = $"Me has alimentado, mi saciedad aumentó en {saciedad} y ahora tengo {comidas} comidas";
                    }
                }
                else
                {
                    ViewBag.Message = "No tienes comida para alimentarme";
                }
                break;
            case "Jugar":
                if (HttpContext.Session.GetInt32("Energia") > 0)
                {
                    Random random = new Random();
                    int? energia = HttpContext.Session.GetInt32("Energia");
                    energia -= 5;
                    HttpContext.Session.SetInt32("Energia", (int)energia);
                    int probabilidad = random.Next(1, 5);
                    if (probabilidad == 1)

                    {
                        ViewBag.Message = $"No me gusta jugar, mi felicidad no aumentó y ahora tengo {energia} de energía";
                    }
                    else
                    {
                        int felicidad = random.Next(5, 11);
                        int? felicidadActual = HttpContext.Session.GetInt32("Felicidad");
                        felicidadActual += felicidad;
                        HttpContext.Session.SetInt32("Felicidad", (int)felicidadActual);
                        ViewBag.Message = $"Has jugado conmigo, mi felicidad aumentó en {felicidad} y ahora tengo {energia} de energía";
                    }
                }
                else
                {
                    ViewBag.Message = "No tienes energía para jugar conmigo";
                }
                break;
            case "Trabajar":
                if (HttpContext.Session.GetInt32("Energia") > 0)
                {
                    Random random = new Random();
                    int comidas = random.Next(1, 4);
                    int? energia = HttpContext.Session.GetInt32("Energia");
                    energia -= 5;
                    HttpContext.Session.SetInt32("Energia", (int)energia);
                    int? comidasActual = HttpContext.Session.GetInt32("Comidas");
                    comidasActual += comidas;
                    HttpContext.Session.SetInt32("Comidas", (int)comidasActual);
                    ViewBag.Message = $"Has trabajado conmigo, he ganado {comidas} comidas y ahora tengo {energia} de energía";
                }
                else
                {
                    ViewBag.Message = "No tienes energía para trabajar conmigo";
                }
                break;
            case "Dormir":
                int? energiaActual = HttpContext.Session.GetInt32("Energia");
                energiaActual += 15;
                HttpContext.Session.SetInt32("Energia", (int)energiaActual);
                int? saciedadActual2 = HttpContext.Session.GetInt32("Saciedad");
                saciedadActual2 -= 5;
                HttpContext.Session.SetInt32("Saciedad", (int)saciedadActual2);
                int? felicidadActual2 = HttpContext.Session.GetInt32("Felicidad");
                felicidadActual2 -= 5;
                HttpContext.Session.SetInt32("Felicidad", (int)felicidadActual2);
                ViewBag.Message = "Me has hecho dormir, mi energía aumentó en 15, mi saciedad y felicidad disminuyeron en 5";
                break;
            case "Reiniciar":
                HttpContext.Session.Clear();
                return RedirectToAction("Index");

        }
        int? saciedadActual3 = HttpContext.Session.GetInt32("Saciedad");
        int? felicidadActual3 = HttpContext.Session.GetInt32("Felicidad");
        int? energiaActual3 = HttpContext.Session.GetInt32("Energia");
        if (saciedadActual3 >= 100 && felicidadActual3 >= 100 && energiaActual3 >= 100)
        {
            ViewBag.Message = "¡Felicidades! Has ganado";
            ViewBag.Jugando = "Ganaste";
        }
        else if (saciedadActual3 <= 0 || felicidadActual3 <= 0)
        {
            ViewBag.Message = "¡Lo siento! Has perdido";
            ViewBag.Jugando = "Perdiste";
        }

        return View();
    }

    [HttpGet]
    [Route("Privacy")]
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