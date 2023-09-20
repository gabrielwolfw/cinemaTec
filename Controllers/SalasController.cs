using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

using cinemaTec.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;


namespace cinemaTec.Controllers{
    [Route("api/salas")]
    [ApiController]
    public class SalasController: ControllerBase{

        // Lista Global de todas las salas existentes
        private List<Sala> salas = new();

        // Lee la ruta de la base de datos referente a salas del cine
        private readonly string rutaArchivoSalas = @"..\cinemaTec\cinetecbase\salas.xlsx";
        



        // --------- METODOS ----------------
        // Definir Get, Put, Post, Delete

        // Metodo Get: Consulta todas las salas
        //Postman test: GET/api/salas
        [HttpGet]
        public IActionResult GetSalas()
        {
            try
            {
                List<Sala> salas = new List<Sala>();
                using (var package = new ExcelPackage(new FileInfo(rutaArchivoSalas)))
                {
                    var hojaSalas = package.Workbook.Worksheets["Salas"];
                    if (hojaSalas != null && hojaSalas.Dimension != null)
                    {
                        int contadorfilas = hojaSalas.Dimension.Rows;
                        for (int fila = 2; fila <= contadorfilas; fila++)
                        {
                            Sala sala = new Sala
                            {
                                // Datos de la sala
                                SalaId = (string)hojaSalas.Cells[fila, 1].Value,
                                NombreSucursal = (string)hojaSalas.Cells[fila, 2].Value,
                                CantidadColumnas = Convert.ToInt32(hojaSalas.Cells[fila, 3].Value),
                                CantidadFilas = Convert.ToInt32(hojaSalas.Cells[fila, 4].Value),
                                Capacidad = Convert.ToInt32(hojaSalas.Cells[fila, 5].Value)
                                };
                                salas.Add(sala);
                                }
                            }
                        }
                        return Ok(salas);
            }catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Postman test: GET/api/salas/id
        // Metodo Get: Consulta un valor especifico de Id de las Salas
        [HttpGet("{salaId}")]
        public IActionResult GetSalaId(string? salaId){
            try{
                // Evitar que el valor salaId no sea nulo
                if (salaId != null)
                {
                    Sala? sala = salas.FirstOrDefault(s => s.SalaId == salaId);

                    if(sala != null){
                        return Ok(sala);

                    }
                    else{
                        return NotFound($"Sala con ID {salaId} no encontrada.");
                    }
                }

                // Agregar un valor de retorno predeterminado
                return Ok(null);
            }catch(Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        // Metodo Post: Ingresa valor de SalaId, Nombre Sucursal, Cantidad Columnas, Cantidad Filas, Capacidad
        // Post Testing:
        [HttpPost]
        public IActionResult AgregarSala([FromBody] Sala nuevaSala){
            try{
                using(var package = new ExcelPackage(new FileInfo(rutaArchivoSalas))){

                    var hojaSalas = package.Workbook.Worksheets["Salas"];

                    // Comprueba que la hoja Sala exista, para almacenar los datos
                    if(hojaSalas == null){
                        return StatusCode(500, "La hoja de trabajo 'Salas' no existe.");
                    }

                    // Comprueba que el ID de la sala no este repetido
                    if(salas.Any(s => s.SalaId == nuevaSala.SalaId))
                    {
                        return BadRequest("El ID de sala ya existe.");
                    }

                    int filaNuevaSala = hojaSalas.Dimension.Rows + 1; // Obtener la próxima fila vacía
                    hojaSalas.Cells[filaNuevaSala, 1].Value = nuevaSala.SalaId;
                    hojaSalas.Cells[filaNuevaSala, 2].Value = nuevaSala.NombreSucursal;
                    hojaSalas.Cells[filaNuevaSala, 3].Value = nuevaSala.CantidadFilas;
                    hojaSalas.Cells[filaNuevaSala, 4].Value = nuevaSala.CantidadColumnas;
                    hojaSalas.Cells[filaNuevaSala, 5].Value = nuevaSala.Capacidad;

                    // Guarda las modificaciones en el excel
                    package.Save();
                }
                // En caso de que la sala se haya creado de manera exitosa.
                return StatusCode(201, "Sala creada con éxito");

            }catch(Exception ex){
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }




    }
}

    