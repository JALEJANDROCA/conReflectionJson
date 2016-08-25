using System;
using System.Text;
using System.Collections.Generic;

namespace conReflection
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//creamos nuestra lista de domicilios
			var domicilios = new List<Domicilio> () {
				new Domicilio{ 
					Calle = "1ra Calle", 
					Colonia = "Colonia 1", 
					Telefonos = new string[]{ "Telefono1", "Telefono2" } 
				},
				new Domicilio{ 
					Calle = "2da Calle", 
					Colonia = "Colonia 2", 
					Telefonos = new string[]{ "Telefono1", "Telefono2" } },
				new Domicilio{ 
					Calle = "3ra Calle", 
					Colonia = "Colonia 3", 
					Telefonos = new string[]{ "Telefono1", "Telefono2" } }
			};
			//creamos nuestro objeto de tipo Persona e inicializamos sus propiedades
			var persona = new Persona { Nombre = "Alex", Edad = 21, Domicilios = domicilios };

			//creamos una instancia nuestra clase que serializa
			var js = new JsonCustomSerializer ();

			//serializamos e imprimimos el obj Persona
			Console.WriteLine (js.Serialize(persona));
            Console.ReadKey();
		}
	}
}