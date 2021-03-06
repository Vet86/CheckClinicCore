﻿using CheckClinic.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace CheckClinic.MailNotifier
{
    public class MailNotifier : IMailNotifier
    {
        private readonly IMailSettings _mailSettings;
        private List<MailAddress> _receivers = new List<MailAddress>();

        public MailNotifier(IMailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public void AddReceiver(MailAddress receiver)
        {
            _receivers.Add(receiver);
        }

        public void AddReceivers(IEnumerable<MailAddress> receivers)
        {
            _receivers.AddRange(receivers);
        }

        public void ClearAllReceivers()
        {
            _receivers.Clear();
        }

        public IReadOnlyList<MailAddress> GetReceivers()
        {
            return _receivers;
        }

        public void Send(string title, string content)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.MailSender, _mailSettings.NameSender)
            };

            foreach (var address in _receivers)
            {
                mailMessage.To.Add(address);
            }

            mailMessage.Subject = title;
            mailMessage.Body = content;
            SmtpClient smtp = new SmtpClient(_mailSettings.Smtp, _mailSettings.Port)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(_mailSettings.MailSender, _mailSettings.PasswordSender),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            smtp.Send(mailMessage);
        }
    }
}
