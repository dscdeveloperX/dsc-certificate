using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class ConfigEmail
    {
        public EmailConfigModel AccountWebSite(string email) => new EmailConfigModel()
        {
            Host = "mail.facefocuscontrol.com",
            Port = 25,
            UserName = "dsc@facefocuscontrol.com",
            Password = "S@n200420052012",
            EnableSsl = false,
            From = "dsc@facefocuscontrol.com",
            To = email, //"dscdeveloper@outlook.com",
            IsBodyHtml = true
        };

        public EmailConfigModel AccountOutlook() => new EmailConfigModel()
        {
            Host = "smtp-mail.outlook.com",
            Port = 587,
            UserName = "darwinrodolfosanchezcorrea@outlook.com",
            Password = "Son200420052012",
            EnableSsl = true,
            From = "darwinrodolfosanchezcorrea@outlook.com",
            To = "darwinrodolfosanchezcorrea@gmail.com",
            IsBodyHtml = true
        };

    }
}