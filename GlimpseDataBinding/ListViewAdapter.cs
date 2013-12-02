using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;

namespace GlimpseDataBinding
{
    public class DataBoundControlAdapter : ControlAdapter
    {
        private DataBoundControl DataBoundControl
        {
            get
            {
                return (DataBoundControl)Control;
            }
        }

        private Dictionary<string, Tuple<Type, object>> Parameters
        {
            get;
            set;
        }

        protected override void OnInit(EventArgs e)
        {
            DataBoundControl.DataBinding += ListControl_DataBinding;
            DataBoundControl.DataBound += ListControl_DataBound;
            Parameters = new Dictionary<string, Tuple<Type, object>>();
            Parameters.Add("DataBinding event fired", Tuple.Create(typeof(Boolean), (object)false));
            base.OnInit(e);
        }

        void ListControl_DataBinding(object sender, EventArgs e)
        {
            Parameters["DataBinding event fired"] = Tuple.Create(typeof(Boolean), (object)true);
            var dataSource = DataBoundControl.DataSourceObject as ObjectDataSource;
            if (dataSource != null)
            {
                var values = dataSource.SelectParameters.GetValues(HttpContext.Current, dataSource);
                foreach (Parameter parameter in dataSource.SelectParameters)
                {
                    Parameters.Add(parameter.Name, Tuple.Create(parameter.GetType(), values[parameter.Name]));
                }
            }
        }

        void ListControl_DataBound(object sender, EventArgs e)
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
                    writer.WriteEncodedText(String.Format("{0}",parameter.Key));
                    writer.Write("</td><td>");
                    writer.WriteEncodedText(String.Format("{0}",parameter.Value.Item1));
                    writer.Write("</td><td>");
                    writer.WriteEncodedText(String.Format("{0}",parameter.Value.Item2));
                    writer.Write("</td></tr>");
                }
            }

            if (!String.IsNullOrEmpty(this.DataBoundControl.DataSourceID))
            {
                writer.Write("<tr><td>");
                writer.Write("DataSourceID");
                writer.Write("</td><td>");
                writer.Write("string");
                writer.Write("</td><td>");
                writer.WriteEncodedText(this.DataBoundControl.DataSourceID);
                writer.Write("</td></tr>");
            }

            var lc = this.DataBoundControl as ListControl;

            if (lc != null)
            {
                if (lc.DataSource != null)
                {
                    writer.Write("<tr><td>");
                    writer.Write("DataSource");
                    writer.Write("</td><td>");
                    writer.Write("string");
                    writer.Write("</td><td>");
                    writer.WriteEncodedText(lc.DataSource.ToString());
                    writer.Write("</td></tr>");
                }
                if (!String.IsNullOrEmpty(lc.DataValueField))
                {
                    writer.Write("<tr><td>");
                    writer.Write("DataValueField");
                    writer.Write("</td><td>");
                    writer.Write("string");
                    writer.Write("</td><td>");
                    writer.WriteEncodedText(lc.DataValueField);
                    writer.Write("</td></tr>");
                }
                if (!String.IsNullOrEmpty(lc.DataTextField))
                {
                    writer.Write("<tr><td>");
                    writer.Write("DataTextField");
                    writer.Write("</td><td>");
                    writer.Write("string");
                    writer.Write("</td><td>");
                    writer.WriteEncodedText(lc.DataTextField);
                    writer.Write("</td></tr>");
                }
            }

                
        
            writer.Write("</table>");
 
        }
    }
}