using System;
using System.Collections.Generic;

namespace conReflection
{
	public class Persona
	{
		public Persona ()
		{
		}
		public string Nombre {
			get;
			set;
		}
		public byte Edad {
			get;
			set;
		}
		public List<Domicilio> Domicilios {
			get;
			set;
		}
	}
}