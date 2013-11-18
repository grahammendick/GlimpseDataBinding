using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlimpseDataBinding
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable GetItems(string filter, string order)
        {
            yield return new { Id = 1 };
            yield return new { Id = 2 };
        }
    }
}