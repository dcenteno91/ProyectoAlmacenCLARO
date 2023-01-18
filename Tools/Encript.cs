using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AesEncript;

namespace WebAlmacen.Tools
{
    public class Encript
    {
        private static readonly string Clave = "L0$9@!sa$2K20QU3RR!!";

        public static string Desencriptar(string cadena)
        {
            try
            {
                return Aes256.Decrypt(cadena, Clave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Encriptar(string cadena)
        {
            try
            {
                return Aes256.Encrypt(cadena, Clave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
