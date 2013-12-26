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

        public BusinessObject bo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownList1.DataSource = this.GetColors();
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();

                bo = new BusinessObject();
                txtId.DataBind();
                txtName.DataBind();
                chkHasBeenSavedToDb.DataBind();
            }
        }

        public IEnumerable GetItems(string filter, string order)
        {
            yield return new { Id = 1 };
            yield return new { Id = 2 };
        }

        public IEnumerable GetColors()
        {
            yield return new { Id = "R", Name = "Red" };
            yield return new { Id = "O", Name = "Orange" };
            yield return new { Id = "Y", Name = "Yellow" };
            yield return new { Id = "G", Name = "Green" };
            yield return new { Id = "B", Name = "Blue" };
            yield return new { Id = "I", Name = "Indigo" };
            yield return new { Id = "V", Name = "Violet" };
        }
    }


    public class BusinessObject
    {
        public int ID { get; set; }
        public string name { get; set; }
        public bool isAwesome { get; set; }

        public BusinessObject()
        {
            this.ID = 555;
            this.name = "Test object 555";
            this.isAwesome = true;
        }
    }
}