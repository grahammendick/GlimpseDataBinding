using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlimpseDataBinding
{
    public partial class Demo : System.Web.UI.Page
    {
		protected void Page_Load(object sender, EventArgs e)
		{
			HttpContext.Current.Items["DataBindInfo"] = new Dictionary<string, List<List<DataBindParameter>>>();
			var text = TextBox1.Text;
			TextBox1.Text = "different text";
			ListView1.DataBind();
			TextBox1.Text = text;
			ListView1.DataBind();
		}

        public IEnumerable GetItems([Control("TextBox1")] string filter, [QueryString("sort")] string order)
        {
            yield return new { Id = 1 };
            yield return new { Id = 2 };
        }
    }
}