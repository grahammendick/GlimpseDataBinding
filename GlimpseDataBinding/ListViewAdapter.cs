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

        private Dictionary<string, Tuple<Type, object>> Parameters
        {
            get;
            set;
        }

        protected override void OnInit(EventArgs e)
        {
            ListView.DataBinding += ListView_DataBinding;
            base.OnInit(e);
        }

        void ListView_DataBinding(object sender, EventArgs e)
        {
            var dataSource = ListView.DataSourceObject as ObjectDataSource;
            Parameters = new Dictionary<string, Tuple<Type, object>>();
            var values = dataSource.SelectParameters.GetValues(HttpContext.Current, dataSource);
            foreach (Parameter parameter in dataSource.SelectParameters)
            {
                Parameters.Add(parameter.Name, Tuple.Create(parameter.GetType(), values[parameter.Name]));
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
            if (Parameters != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
                writer.Write("<table border=1px><tr><th>Name</th><th>Type</th><th>Value</th></tr>");
                foreach (var parameter in Parameters)
                {
                    writer.Write("<tr><td>");
                    writer.Write(parameter.Key);
                    writer.Write("</td><td>");
                    writer.Write(parameter.Value.Item1);
                    writer.Write("</td><td>");
                    writer.Write(parameter.Value.Item2);
                    writer.Write("</td></tr>");
                }
                writer.Write("</table>");
            }
        }
    }
}