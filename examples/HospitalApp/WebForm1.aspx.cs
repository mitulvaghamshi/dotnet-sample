using System;

namespace HospitalApp
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Label1.Text = "Hello World!";
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			Label1.Text = "Hello Universe!";
		}
	}
}