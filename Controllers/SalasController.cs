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
        private readonly string rutaArchivoSalas = @"..\cinemaTec\cinetecbase\admin\salas.txt";
        
        
        // --------- METODOS ----------------
        // Definir Get, Put, Post, Delete

        //Postman test: GET/api/salas
        [HttpGet]
        public IActionResult GetSalas(){
            try{
                // Leer el documento salas.txt
                string contenido = System.IO.File.ReadAllText(rutaArchivoSalas);
                // Devuelve un valor nulo si es el archivo está vacio
                List<Sala>? salas = JsonConvert.DeserializeObject<List<Sala>>(contenido) ?? new List<Sala>();// Recorre todas las salas
                return Ok(salas);
            }catch(Exception ex){
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        //Postman test: GET/api/salas/id
        [HttpGet("{id}")]
        public IActionResult GetSalaId(int id){
            // Leer el documento salas.txt
            try{
                string contenido = System.IO.File.ReadAllText(rutaArchivoSalas);
                List<Sala>? salas = JsonConvert.DeserializeObject<List<Sala>>(contenido) ?? new List<Sala>();// Recorre todas las salas
                Sala? sala = salas.FirstOrDefault(s => s.SalaId == id);

                if(sala != null){
                    return Ok(sala);
                }else{
                    return NotFound($"Sala con ID {id} no encontrada.");
                }
            }
            catch(Exception ex){
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}

    