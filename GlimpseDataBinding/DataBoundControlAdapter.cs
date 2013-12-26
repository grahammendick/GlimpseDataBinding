using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;

namespace GlimpseDataBinding
{
    public class WebControlAdapter : ControlAdapter
    {
        private WebControl webControl
        {
            get
            {
                return (WebControl)Control;
            }
        }

        private Dictionary<string, Tuple<Type, object>> Parameters
        {
            get;
            set;
        }

        protected override void OnInit(EventArgs e)
        {
            this.webControl.DataBinding += Control_DataBinding;
            var dbc = this.webControl as DataBoundControl;
            if (dbc != null)
            {
                dbc.DataBound += Control_DataBound;
            }
            Parameters = new Dictionary<string, Tuple<Type, object>>();
            Parameters.Add("DataBinding event fired", Tuple.Create(typeof(Boolean), (object)false));
            base.OnInit(e);
        }

        void Control_DataBinding(object sender, EventArgs e)
        {
            Parameters["DataBinding event fired"] = Tuple.Create(typeof(Boolean), (object)true);
            var dbc = this.webControl as DataBoundControl;
            if (dbc != null)
            {
                var dataSource = dbc.DataSourceObject as ObjectDataSource;
                if (dataSource != null)
                {
                    var values = dataSource.SelectParameters.GetValues(HttpContext.Current, dataSource);
                    foreach (Parameter parameter in dataSource.SelectParameters)
                    {
                        Parameters.Add(parameter.Name, Tuple.Create(parameter.GetType(), values[parameter.Name]));
                    }
                }
            }
            string message = "Value at data bind:";
            if (sender is TextBox)
            {
                var textBox = sender as TextBox;
                Parameters.Add(message, Tuple.Create(typeof(String), (object)textBox.Text));
            }
            else if (sender is CheckBox)
            {
                var checkBox = sender as CheckBox;
                Parameters.Add(message, Tuple.Create(typeof(Boolean), (object)checkBox.Checked));
            }
            
        }

        void Control_DataBound(object sender, EventArgs e)
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

            var dbc = this.webControl as DataBoundControl;

            if (dbc != null && !String.IsNullOrEmpty(dbc.DataSourceID))
            {
                writer.Write("<tr><td>");
                writer.Write("DataSourceID");
                writer.Write("</td><td>");
                writer.Write("string");
                writer.Write("</td><td>");
                writer.WriteEncodedText(dbc.DataSourceID);
                writer.Write("</td></tr>");
            }

            var lc = dbc as ListControl;

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