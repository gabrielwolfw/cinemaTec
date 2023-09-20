using System;

namespace cinemaTec.Models{
    public class Sala{
        public string SalaId{get;set;} //Identificador
        public string NombreSucursal{get;set;} //Nombre de Sucursal
        public int CantidadFilas{get;set;} // Cantidad de Filas
        public int CantidadColumnas{get;set;} // Cantidad de Columnas
        public int Capacidad{get;set;} // Capacidad de la Sala


            public Sala()
            {
                SalaId = string.Empty; // Inicializar con una cadena vacía
                NombreSucursal = string.Empty; // Inicializar con una cadena vacía
            }
    }


}