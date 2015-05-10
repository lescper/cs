using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net.Mail;

namespace FxProductMonitor.BLL
{
    public class SendMail
    {
        public void Send(string msg, string mailTo, string attchments)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.163.com";
            smtpClient.Credentials = new System.Net.NetworkCredential("lescper@163.com", "521zhangwei");
            MailAddress from = new MailAddress("lescper@163.com");
            MailAddress to = new MailAddress("549341762@qq.com");
            MailMessage message = new MailMessage(from, to);
            if (!string.IsNullOrEmpty(mailTo))
            {
                foreach (var mail in mailTo.Split(';'))
                {
                    message.To.Add(new MailAddress(mail));
                }
            }

            if (!string.IsNullOrEmpty(attchments))
            {
                foreach (var attachment in attchments.Split(';'))
                {
                    message.Attachments.Add(new Attachment(attachment));
                }
            }
            message.Subject = "分销系统通知" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            message.Body = msg;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;
            smtpClient.Send(message);
        }
    }
}
