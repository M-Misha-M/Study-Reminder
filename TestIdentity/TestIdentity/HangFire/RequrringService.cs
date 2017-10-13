using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Net.Mail;
using TestIdentity.DAL;
using TestIdentity.Models;

namespace TestIdentity.HangFire
{
    public class RequrringService : IRequrringService
    {
        IRepository<PersonalInformation> repository;

        public RequrringService(IRepository<PersonalInformation> repo)
        {
            repository = repo;
        }

        public void SendEmail(string email, int day)
        {            
            MailMessage message = new MailMessage();
            message.Subject = "Start study";
            message.Body = $"Your study start will begin through  {day} day(s)";
            message.IsBodyHtml = true;
            message.To.Add(email);           
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
        }

        public void CheckEducationDate()
        {
            const int DaysMonth = 31;
            const int DaysWeek = 7;
            const int OneDay = 1;
            var oneMonthFromNow = DateTime.UtcNow.AddMonths(1).Date;
            var oneWeekFromNow = DateTime.UtcNow.AddDays(7).Date;
            var oneDay = DateTime.UtcNow.AddDays(1).Date;
            var user = repository.Get()
                                  .Where(x => (x.StudyDate == oneMonthFromNow) 
                                           || 
                                        (x.StudyDate == oneWeekFromNow) 
                                           ||
                                        (x.StudyDate == oneDay))
                                  .ToList();
            user.ForEach(u => 
            {
                SendEmail(u.ApplicationUser.Email, (int)(u.StudyDate.Value.Date - DateTime.UtcNow.Date).TotalDays);
                //if (u.StudyDate.Value.Date == oneMonthFromNow)
                //{
                //    SendEmail(u.ApplicationUser.Email, DaysMonth);
                //}
                //else if(u.StudyDate.Value.Date == oneWeekFromNow)
                //{
                //  SendEmail(u.ApplicationUser.Email, DaysWeek);
                //}
                //else if(u.StudyDate.Value.Date == oneDay)
                //{
                //    SendEmail(u.ApplicationUser.Email, OneDay);
                //}
            });
        }
    }
}