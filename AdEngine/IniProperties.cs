using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngine.API
{
    public class IniProperties
    {
        public static string mongoUrl { get; set; } = "mongodb://adportal2:Ohsg4ewR4z4kDFhWra8UDRaqG64vhtzqdNe3jrThay9D4i7bTs60FEgpLJHWbT7ecSjBz3pOJGOJhbqGLz26zQ==@adportal2.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
        //public static string mongoUrl { get; set; } = "mongodb://matek997:8eKgg6o8LrlxlOPXIdriruyBoZNtUwpdny02t2uCqSNpZAeKMeERIqhMexVi2cWgZQk2lYHVKNrE6fLE3K3MSg==@matek997.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
        public static string mongoPass { get; set; }
        public static string databaseName { get; set; } = "testDB";
    }
}
