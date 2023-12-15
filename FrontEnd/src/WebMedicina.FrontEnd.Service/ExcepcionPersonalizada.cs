﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.Service {
    public class ExcepcionPersonalizada : Exception {
        public void ConstruirPintarExcepcion(Exception e) {
            Type tipo = e.GetType();
            string mensaje = e.Message;
            string camino = e.StackTrace ?? string.Empty;

            // Generamos la excepcion y la pintamos por consola 
            Console.Error.WriteLine($"- Tipo: {tipo} - Mensaje: {mensaje} - Camino: {camino}");
        }
    }
}
