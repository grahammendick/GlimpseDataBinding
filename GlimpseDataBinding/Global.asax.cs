using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;
using System.Web.SessionState;

namespace GlimpseDataBinding
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			ModelBinders.Binders.DefaultBinder = new GlimpseModelBinder();
		}
	}
}