using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace GlimpseDataBinding
{
	public class GlimpseModelBinder : DefaultModelBinder, IModelBinder
	{
		bool IModelBinder.BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			var success = base.BindModel(modelBindingExecutionContext, bindingContext);
			if (success)
			{
				var parms = (Dictionary<string, Tuple<Type, object>>)HttpContext.Current.Items["ModelBind"];
				parms.Add(bindingContext.ModelName, Tuple.Create(bindingContext.ValueProvider.GetType(), (object) bindingContext.ModelState[bindingContext.ModelName].Value.AttemptedValue));
			}
			return success;
		}
	}
}