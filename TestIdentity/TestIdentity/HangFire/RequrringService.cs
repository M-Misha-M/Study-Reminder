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

        public RequrringService(IRepository<PersonalInformation> repository)
        {
            this.repository = repository;
        }

        public void SendEmail(string email, int day)
        {            
            var message = new MailMessage();
            message.Subject = "Start study";
            message.Body = $"Your study start will begin through  {day} day(s)";
            message.IsBodyHtml = true;
            message.To.Add(email);           
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
        }

        public void CheckEducationDate()
        {
            const int oneMonth = 1;
            const int weekDays = 7;
            const int oneDay = 1; 
            var oneMonthFromNow = DateTime.UtcNow.AddMonths(oneMonth).Date;
            var oneWeekFromNow = DateTime.UtcNow.AddDays(weekDays).Date;
            var oneDayFromNow = DateTime.UtcNow.AddDays(oneDay).Date;
            var user = repository.Get()
                                 .Where(x => (x.StudyDate == oneMonthFromNow) 
                                          || (x.StudyDate == oneWeekFromNow) 
                                          || (x.StudyDate == oneDayFromNow))
                                  .ToList();
            user.ForEach(u => 
            {
               SendEmail(u.ApplicationUser.Email, (int)(u.StudyDate.Value.Date - DateTime.UtcNow.Date).TotalDays);
            });
        }
    }
}