
namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Text;
    using NUnit.Framework;
    using System.Net.Mail;

    [TestFixture]
    class UintTest_Email
    {

        [Test]
        public void _0001_Send()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("konvolucio.report@gmail.com", "konvolucio.a0832d");
            MailMessage mm = new MailMessage("donotreply@konvolucio.com", "konvolucio.report@gmail.com", "test", "test");
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);

        }
    }
}
