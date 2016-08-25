using System;
using System.Text;
using System.Collections;

namespace conReflection
{
	public class JsonCustomSerializer
	{
		public JsonCustomSerializer ()
		{
		}

		public string Serialize (Object o)
		{
			/*antes de continuar con el proceso, verificamos si el objeto
             * que hemos recibimos es un tipo de dato simple y no una propiedad,
             * un string o un tipo de dato primitivo, recuerden que en Json,
             * los string se serializan con "" comillas, los char con '' comillas simples
             * y los otros tipos de datos que son primitivos como double, int, 
             * NO llevan comillas.
             * Este bloque de código romperá la función recursiva
             */
			if (o is String)
				return string.Format ("\"{0}\"", o);
			else if (o is char)
				return string.Format ("'{0}'", o);
			else if (o.GetType ().IsPrimitive)
				return o.ToString ();

			var json = new StringBuilder ();

			//verificamos si el objeto recibido es una colección de datos
			if (o is IList) {

				//obtenemos la lista de objetos que hemos recibido
				var objects = ((IList)o);
				json.Append ('[');
				for (int i = 0; i < objects.Count; i++) {
					//por cada elemento de la colección, llamaremos  de nuevo
					//a nuestra función Serialize que  serializa objetos, esta
					//es la recursividad
					json.Append (Serialize (objects [i]));

					if (i < objects.Count - 1)
						json.Append (',');
				}
				json.Append (']');
				return json.ToString ();
			}

			/*obtenemos un arreglo con la información de las
             * propiedades del objeto, es como si obtuvieramos las 
             * propiedades de las propiedades de un objeto, y luego obtenemos 
             * cuantas propiedades tiene el objeto, para luego iterarlas
             */
			var properties = o.GetType ().GetProperties ();
			var totalProperties = properties.GetLength (0);
			//var json = new StringBuilder ();
			json.Append ('{');
			for (int i = 0; i < totalProperties; i++) {
				//primero concatenamos el nombre que tiene la propiedad del objeto
				//"Name" es una propiedad de una de las propiedades del objeto
				//(es el nombre que le ponemos a las propiedades cuando definimos
				//el objeto), Name es solo uno de las tantos atributos que
				//puede tener la propiedad de un objeto
				json.AppendFormat ("\"{0}\": ", properties [i].Name);

				//después concateremos el valor de la propiedad serializado a json,
				//como nuestro valor puede ser una colección de datos,
				//llamamos recursivamente a la función Serialize para que
				//nos devuelva el valor serializado
				json.AppendFormat ("{0}", Serialize (properties [i].GetValue (o)));
				if (i < totalProperties - 1)
					json.Append (',');
			}
			json.Append ('}');
			return json.ToString ();
		}
	}
}