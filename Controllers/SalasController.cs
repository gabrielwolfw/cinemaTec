using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using cinemaTec.Models;


namespace cinemaTec.Controllers{
    [Route("api/salas")]
    [ApiController]
    public class SalasController: ControllerBase{

        // Obtiene la ruta de la base de datos referente a salas del cine
        private readonly string rutaArchivoSalas;
        public SalasController(){
            string capertaAdminBD = "adminBD";
            string archivoSala = "salas.txt";
            rutaArchivoSalas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,capertaAdminBD,archivoSala);
        }
        
        // --------- METODOS ----------------
        // Definir Get, Put, Post, Delete

        //Postman test: GET/api/salas
        [HttpGet]
        public IActionResult GetSalas(){
            try{
                // Leer el documento salas.txt
                string contenido = System.IO.File.ReadAllText(rutaArchivoSalas);
                // Devuelve un valor nulo si es el archivo está vacio
                List<Sala>? salas = JsonConvert.DeserializeObject<List<Sala>>(contenido) ?? new List<Sala>();
                return Ok(salas);
            }catch(Exception ex){
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        //Postman test: GET/api/salas/id
    }
}

    