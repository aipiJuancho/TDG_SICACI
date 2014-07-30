using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDG_SICACI.Models
{
    public sealed class ErrorMessages
    {
        private static string _fieldRequired = "El campo \"{0}\" no puede quedar vacío";
        public static string FieldRequired
        {
            get
            {
                return _fieldRequired;
            }
        }

        private static string _longitudUser = "La longitud mínima de caracteres para un usuario es de 4";
        public static string LongitudUser
        {
            get
            {
                return _longitudUser;
            }
        }

        private static string _caracteresUser = "El nombre de usuario solo puede estar compuesto por caracteres de la A-Z";
        public static string CaracteresUser
        {
            get
            {
                return _caracteresUser;
            }
        }

        private static string _longitudPWD = "La longitud mínima de caracteres para una contraseña es de 8";
        public static string LongitudPWD
        {
            get
            {
                return _longitudPWD;
            }
        }

        private static string _onlyLetras_WSpace = "Este campo solo admite caracteres alfanuméricos";
        public static string OnlyLetras_WSpace
        {
            get
            {
                return _onlyLetras_WSpace;
            }
        }

        private static string _error_Email = "El correo electrónico ingresado no es válido";
        public static string Email
        {
            get
            {
                return _error_Email;
            }
        }

        private static string _error_comparePWD = "Las contraseñas ingresadas no coinciden";
        public static string ComparePWD
        {
            get
            {
                return _error_comparePWD;
            }
        }
    }
}