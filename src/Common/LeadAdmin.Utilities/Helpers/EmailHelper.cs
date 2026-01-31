using System.Net;
using System.Net.Mail;
using LeadAdmin.Utilities.Constants;
using LeadAdmin.Entities.Core;

namespace LeadAdmin.Utilities.Helpers
{
    public class EmailHelper
    {
        public static void SendEmailFromTemplate(EmailSetting emailSetting, string subject, string body, string toList, string ccList, string attachmentFileName = "", string attachmentContent = "")
        {
            if(emailSetting == null || string.IsNullOrWhiteSpace(emailSetting.FromAddress))
            {
                emailSetting = new EmailSetting();
                emailSetting.FromAddress = ConfigSettings.Instance.DefaultSmtpSettings.FromAddress;
                emailSetting.UserName = ConfigSettings.Instance.DefaultSmtpSettings.UserName;
                emailSetting.Password = ConfigSettings.Instance.DefaultSmtpSettings.Password;
                emailSetting.SMTPAddress = ConfigSettings.Instance.DefaultSmtpSettings.Smtp;
                emailSetting.PortAddreess = ConfigSettings.Instance.DefaultSmtpSettings.Port;
            }

            var msg = new MailMessage();
            msg.From = new MailAddress(emailSetting.FromAddress);            
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            if (!string.IsNullOrWhiteSpace(attachmentContent))
            {
                if (string.IsNullOrWhiteSpace(attachmentFileName)) attachmentFileName = Guid.NewGuid().ToString();
                var memoryStream = PdfHelper.ToPdf(attachmentContent);
                var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Plain);
                var attachment = new Attachment(memoryStream, contentType);
                attachment.ContentDisposition.FileName = attachmentFileName + ".pdf";
                msg.Attachments.Add(attachment);
            }

            var smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(emailSetting.UserName, emailSetting.Password);
            smtpClient.Host = emailSetting.SMTPAddress;
            smtpClient.Port = emailSetting.PortAddreess;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            if (!string.IsNullOrWhiteSpace(toList))
            {
                toList = toList.Replace(";", ",").ToLower();
                var emailToList = toList.Split(',').Distinct();
                foreach (var item in emailToList)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        try { msg.To.Add(item.Trim()); } catch { }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(ccList))
            {
                ccList = ccList.Replace(";", ",").ToLower();
                var emailCcList = ccList.Split(',').Distinct();
                foreach (var item in emailCcList)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        try { msg.CC.Add(item.Trim()); } catch { }
                    }
                }
            }

            if (msg.CC.Count > 0 || msg.To.Count > 0)
            {
                try
                {
                    smtpClient.Send(msg);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }
    }
}
