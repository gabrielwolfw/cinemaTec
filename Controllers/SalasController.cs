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
        private readonly List<Sala> salas = new();
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
                                SalaId = Convert.ToInt32(hojaSalas.Cells[fila, 1].Value),
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


        
        // Metodo Get: Consulta un valor especifico de Id de las Salas
        // Get test: GET/api/salas/1
        [HttpGet("{salaid}")]
        public IActionResult GetSalaPorId(int salaid){
            try{


                if(salaid > 0){
                    // Buscar la sala con un Id especifico
                    Sala? sala = salas.FirstOrDefault(s => s.SalaId == salaid);
                    if(sala != null){
                        return Ok(sala);
                    }else{
                        return NotFound($"Sala con ID {salaid} no encontrada.");
                    }
                }
                return Ok(null);
            }catch(Exception ex){
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }
        // Metodo Post: Ingresa valor de SalaId, Nombre Sucursal, Cantidad Columnas, Cantidad Filas, Capacidad
        // Post Testing:

        [HttpPost]
        public IActionResult AgregarSala([FromBody] Sala nuevaSala){
            try{
                if(nuevaSala != null){
                    if(EsIdUnico(nuevaSala.SalaId)){
                        salas.Add(nuevaSala);
                        using(var package = new ExcelPackage(new FileInfo(rutaArchivoSalas))){
                            var hojaSalas = package.Workbook.Worksheets["Salas"];
                            if(hojaSalas == null){
                                hojaSalas = package.Workbook.Worksheets.Add("Salas");
                            }
                            int fila = hojaSalas.Dimension?.Rows ?? 1;
                            fila ++;
                            hojaSalas.Cells[fila, 1].Value = nuevaSala.SalaId;
                            hojaSalas.Cells[fila, 2].Value = nuevaSala.NombreSucursal;
                            hojaSalas.Cells[fila, 3].Value = nuevaSala.CantidadColumnas;
                            hojaSalas.Cells[fila, 4].Value = nuevaSala.CantidadFilas;
                            hojaSalas.Cells[fila, 5].Value = nuevaSala.Capacidad;
                            package.Save();
                        }

                        return CreatedAtAction("GetSalaPorId", new { salaid = nuevaSala.SalaId }, nuevaSala);
                    }else{
                        return BadRequest("La sala ya ha sido creada");
                    }

                }else{
                    return BadRequest("Los datos de la sala no son validos");
                }
            }catch(Exception ex){
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }
        // Función para comprobar que el Id es unico
        private bool EsIdUnico(int salaId){
            return !salas.Any(s => s.SalaId == salaId);
        }

        // Método PUT: Actualizar una sala existente
        // Postman test: PUT/api/salas/1 (donde 1 es el ID de la sala que deseas actualizar)
        [HttpPut("{salaid}")]
        public IActionResult ActualizarSala(int salaid, [FromBody] Sala salaActualizada)
        {
            try
            {
                if (salaActualizada != null)
                {
                    // Buscar la sala existente por ID
                    Sala? salaExistente = salas.FirstOrDefault(s => s.SalaId == salaid);

                    if (salaExistente != null)
                    {
                        // Actualizar los datos de la sala existente
                        salaExistente.NombreSucursal = salaActualizada.NombreSucursal;
                        salaExistente.CantidadColumnas = salaActualizada.CantidadColumnas;
                        salaExistente.CantidadFilas = salaActualizada.CantidadFilas;
                        salaExistente.Capacidad = salaActualizada.Capacidad;

                        // Aquí puedes guardar los cambios en el archivo Excel o en tu sistema de almacenamiento de datos

                        return Ok(salaExistente);
                    }
                    else
                    {
                        return NotFound($"Sala con ID {salaid} no encontrada.");
                    }
                }
                else
                {
                    return BadRequest("Los datos de la sala no son válidos.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        // Método DELETE: Eliminar una sala
        // Postman test: DELETE/api/salas/1 (donde 1 es el ID de la sala que deseas eliminar)
        [HttpDelete("{salaid}")]
        public IActionResult EliminarSala(int salaid)
        {
            try
            {
                // Buscar la sala con el ID especificado
                Sala? salaAEliminar = salas.FirstOrDefault(s => s.SalaId == salaid);

                if (salaAEliminar != null)
                {
                    // Remover la sala de la lista en memoria
                    salas.Remove(salaAEliminar);

                    using (var package = new ExcelPackage(new FileInfo(rutaArchivoSalas)))
                    {
                        var hojaSalas = package.Workbook.Worksheets["Salas"];
                        if (hojaSalas != null && hojaSalas.Dimension != null)
                        {
                            int filaAEliminar = -1;

                            // Busca la fila que contiene la sala con el ID especificado
                            for (int fila = 2; fila <= hojaSalas.Dimension.Rows; fila++)
                            {
                                if (Convert.ToInt32(hojaSalas.Cells[fila, 1].Value) == salaid)
                                {
                                    filaAEliminar = fila;
                                    break;
                                }
                            }

                            if (filaAEliminar > 0)
                            {
                                hojaSalas.DeleteRow(filaAEliminar, 1); // Elimina la fila
                                package.Save(); // Guarda el archivo Excel
                            }
                        }
                    }
                    return NoContent(); // Respuesta HTTP 204 (NoContent) indicando éxito sin contenido
                }
                else
                {
                    return NotFound($"Sala con ID {salaid} no encontrada.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}

    