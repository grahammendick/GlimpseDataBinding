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
        public IEnumerable GetItems([Control("TextBox1")] string filter, [QueryString("sort")] string order)
        {
            yield return new { Id = 1 };
            yield return new { Id = 2 };
        }
    }
}