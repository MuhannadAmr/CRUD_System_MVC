using MVCProject.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVCProject.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mohanedamr2015@gmail.com", "niwvqtcbcygvaaip");
			client.Send("mohanedamr2015@gmail.com",email.To,email.Subject,email.Body);
		}
	}
}
