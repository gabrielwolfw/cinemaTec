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

        // Lee una lista de las salas
        private readonly List<Sala> salas = new();


        // Genera los Id's
        private static int proximoId = 1; // Inicializa con el primer ID


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
        public IActionResult GetSalaId(string salaId){
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




    }
}

    