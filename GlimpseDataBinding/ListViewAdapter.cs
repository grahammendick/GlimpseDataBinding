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
            ListView.DataBound += ListView_DataBound;
            Parameters = new Dictionary<string, Tuple<Type, object>>();
            Parameters.Add("DataBinding event fired", Tuple.Create(typeof(Boolean), (object)false));
            base.OnInit(e);
        }

        void ListView_DataBinding(object sender, EventArgs e)
        {
            Parameters["DataBinding event fired"] = Tuple.Create(typeof(Boolean), (object)true);
            var dataSource = ListView.DataSourceObject as ObjectDataSource;
            var values = dataSource.SelectParameters.GetValues(HttpContext.Current, dataSource);
            foreach (Parameter parameter in dataSource.SelectParameters)
            {
                Parameters.Add(parameter.Name, Tuple.Create(parameter.GetType(), values[parameter.Name]));
            }
        }

        void ListView_DataBound(object sender, EventArgs e)
        {
            //not sure if there is anything useful to do here...
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
            writer.Write("<table border=1px><tr><th>Name</th><th>Type</th><th>Value</th></tr>");
 
            if (Parameters != null && Parameters.Keys.Count > 0)
            {
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
            }
            
            if (this.ListView.DataSourceID != null)
            {
                writer.Write("<tr><td>");
                writer.Write("DataSourceID");
                writer.Write("</td><td>");
                writer.Write("string");
                writer.Write("</td><td>");
                writer.Write(this.ListView.DataSourceID);
                writer.Write("</td></tr>");
            }
            writer.Write("</table>");
 
        }
    }
}