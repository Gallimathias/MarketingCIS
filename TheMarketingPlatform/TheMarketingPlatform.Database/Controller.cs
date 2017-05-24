using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.JSON;
using TheMarketingPlatform.Mail;

namespace TheMarketingPlatform.Database
{
    public class Controller
    {
        public List<IMailClientSettings> MailClientSettings { get; private set; }

        MainDatabaseContext databaseContext;

        public Controller(string connection)
        {
            databaseContext = new MainDatabaseContext(connection);

        }

        public Mail GetLastMessage() =>
            databaseContext.GetTable<Mail>().OrderByDescending(m => m.TimeStamp).FirstOrDefault();

        public int Insert(MimeMessage mimeMessage)
        {
            var mail = new Mail()
            {
                Body = mimeMessage.TextBody,
                Subject = mimeMessage.Subject,
                TimeStamp = mimeMessage.Date
            };

            databaseContext.GetTable<Mail>().InsertOnSubmit(mail);
            databaseContext.SubmitChanges();

            var mailAddresses = new List<MailAddress>();
            var dictonary = new Dictionary<AddressType, InternetAddressList>
            {
                { AddressType.From, mimeMessage.From },
                { AddressType.To, mimeMessage.To },
                { AddressType.CC, mimeMessage.Cc },
                { AddressType.BCC, mimeMessage.Bcc }
            };

            foreach (var type in dictonary)
            {
                foreach (var adress in type.Value)
                {
                    mailAddresses.Add(new MailAddress()
                    {
                        Adress = adress.Name,
                        MailId = mail.Id,
                        Type = new[] { (byte)type.Key }
                    });
                }
            }

            databaseContext.GetTable<MailAddress>().InsertAllOnSubmit(mailAddresses);
            databaseContext.SubmitChanges();

            return mail.Id;
        }
        public int Insert(Response response, int messageid)
        {
            var luisResponse = new LuisResponse()
            {
                MailId = messageid,
                TimeStamp = DateTimeOffset.Now
            };

            databaseContext.GetTable<LuisResponse>().InsertOnSubmit(luisResponse);
            databaseContext.SubmitChanges();

            var entities = new List<LuisEntity>();
            foreach (var entity in response.Entities)
            {
                entities.Add(new LuisEntity()
                {
                    LuisResponseId = luisResponse.Id,
                    Entity = entity.EntityName,
                    StartIndex = entity.StartIndex,
                    EndIndex = entity.EndIndex,
                    Score = entity.Score,
                    Type = entity.Type
                });
            }

            databaseContext.GetTable<LuisEntity>().InsertAllOnSubmit(entities);

            var intents = new List<LuisIntent>();

            foreach (var intent in response.Intents)
            {
                intents.Add(new LuisIntent()
                {
                    LuisResponseId = luisResponse.Id,
                    Intent = intent.IntentName,
                    Score = intent.Score,
                    IsTopScore = intent.IntentName == response.TopScoringIntent.IntentName
                });
            }

            databaseContext.GetTable<LuisIntent>().InsertAllOnSubmit(intents);
            databaseContext.SubmitChanges();

            return luisResponse.Id;
        }

        private void GetMailSettings()
        {
            foreach (var setting in databaseContext.GetTable<MailAccount>())
            {
                MailClientSettings.Add(new ClientSetting()
                {
                    Host = setting.Host,
                    //Password = setting.Password,
                    Port = setting.Port,
                    Type = (MailClientType)setting.Type.ToArray()[0],
                    UserName = setting.Username,
                    UseSsl = setting.UseSsl
                });
            }
        }
    }
}
