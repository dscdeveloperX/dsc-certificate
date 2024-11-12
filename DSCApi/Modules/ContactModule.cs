using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DSCApi.Models;

namespace DSCApi.Modules
{
    public class ContactModule
    {
        public async Task<string[]> SendMail(EmailConfigModel emailConfigModel, EmailModel emailModel)
        {

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailConfigModel.From, emailModel.DisplayName);
                mail.To.Add(new MailAddress(emailConfigModel.To));
                mail.Subject = emailModel.Subject;
                mail.IsBodyHtml = emailConfigModel.IsBodyHtml;
                mail.Body = emailModel.Body;
                Attachment archivo = new Attachment(System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/archivo.pdf"));
                mail.Attachments.Add(archivo);

                using (SmtpClient smtp = new SmtpClient(emailConfigModel.Host, emailConfigModel.Port))
                {
                    smtp.Credentials = new NetworkCredential(emailConfigModel.UserName, emailConfigModel.Password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = emailConfigModel.EnableSsl;
                    await smtp.SendMailAsync(mail);
                };
                return new string[] { };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<string[]> SendMailDocument(EmailConfigModel emailConfigModel, EmailModel emailModel, string documentoCode)
        {

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailConfigModel.From, emailModel.DisplayName);
                mail.To.Add(new MailAddress(emailConfigModel.To));
                mail.Subject = emailModel.Subject;
                mail.IsBodyHtml = emailConfigModel.IsBodyHtml;
                mail.Body = emailModel.Body;
                Attachment archivo = new Attachment(System.Web.Hosting.HostingEnvironment.MapPath($"~/Uploads/Document/{documentoCode}.pdf"));
                mail.Attachments.Add(archivo);

                using (SmtpClient smtp = new SmtpClient(emailConfigModel.Host, emailConfigModel.Port))
                {
                    smtp.Credentials = new NetworkCredential(emailConfigModel.UserName, emailConfigModel.Password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = emailConfigModel.EnableSsl;
                    await smtp.SendMailAsync(mail);
                };
                return new string[] { };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}