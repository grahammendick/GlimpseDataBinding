using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;

namespace GlimpseDataBinding
{
    public class ListViewAdapter : ControlAdapter
    {
        private ListView ListView
        {
            get
            {
                return (ListView)Control;
            }
        }

		private Dictionary<string, List<List<DataBindParameter>>> DataBindInfo
		{
			get
			{
				return (Dictionary<string, List<List<DataBindParameter>>>) HttpContext.Current.Items["DataBindInfo"];
			}
		}


        protected override void OnInit(EventArgs e)
        {
			ListView.CallingDataMethods += ListView_CallingDataMethods;
			ListView.DataBinding += ListView_DataBinding;
            base.OnInit(e);
        }

		void ListView_CallingDataMethods(object sender, CallingDataMethodsEventArgs e)
		{
			HttpContext.Current.Items["ModelBind"] = new List<DataBindParameter>();
		}

        void ListView_DataBinding(object sender, EventArgs e)
        {
            var dataSource = ListView.DataSourceObject as ObjectDataSource;
			List<DataBindParameter> parameters = null;
			if (dataSource != null)
			{
				parameters = new List<DataBindParameter>();
				var values = dataSource.SelectParameters.GetValues(HttpContext.Current, dataSource);
				foreach (Parameter parameter in dataSource.SelectParameters)
				{
					var name = parameter as ControlParameter != null ? ((ControlParameter)parameter).ControlID : ((QueryStringParameter)parameter).QueryStringField;
					parameters.Add(new DataBindParameter(name, parameter.GetType(), values[parameter.Name]));
				}
			}
			else
			{
				parameters = (List<DataBindParameter>)HttpContext.Current.Items["ModelBind"];
				HttpContext.Current.Items["ModelBind"] = new List<DataBindParameter>();
			}
			if (!DataBindInfo.ContainsKey(ListView.UniqueID))
				DataBindInfo[ListView.UniqueID] = new List<List<DataBindParameter>>();
			DataBindInfo[ListView.UniqueID].Add(parameters);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
			if (DataBindInfo.ContainsKey(ListView.UniqueID))
			{
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
                writer.Write("<table border=1px><tr><th>Name</th><th>Type</th><th>Value</th></tr>");
				var parameterList = DataBindInfo[ListView.UniqueID];
				var i = 0;
				foreach (var parameters in parameterList)
				{
					writer.Write("<th colspan=\"3\">");
					writer.Write("Data Bind " + i);
					writer.Write("</th>");
					foreach (var parameter in parameters)
					{
						writer.Write("<tr><td>");
						writer.Write(parameter.Name);
						writer.Write("</td><td>");
						writer.Write(parameter.Type);
						writer.Write("</td><td>");
						writer.Write(parameter.Value);
						writer.Write("</td></tr>");
					}
					i++;
				}
                writer.Write("</table>");
            }
        }
    }
}