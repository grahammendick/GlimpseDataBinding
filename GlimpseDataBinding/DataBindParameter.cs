using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlimpseDataBinding
{
	public class DataBindParameter
	{
		public DataBindParameter(string name, Type type, object value)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;
		}

		public string Name { get; set; }
		public Type Type { get; set; }
		public object Value { get; set; }
	}
}