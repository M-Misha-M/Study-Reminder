using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public  void SendEmail(string email, int day)
        {
            EmailService emailService = new EmailService();
            IdentityMessage identityMessage = new IdentityMessage();
            identityMessage.Subject = "Start study";
            identityMessage.Body = $"Your study start will begin through  {day} day(s)";
            identityMessage.Destination = email;
            emailService.SendAsync(identityMessage);
        }

        public void CheckEducationDate()
        {
            const int DaysMonth = 31;
            const int DaysWeek = 7;
            const int OneDay = 1;
            var oneMonthFromNow = DateTime.Now.AddMonths(1).Date;
            var oneWeekFromNow = DateTime.Now.AddDays(7).Date;
            var oneDay = DateTime.Now.AddDays(1).Date;
            var user = repository.Get()
                                  .Where(x => (x.StudyDate == oneMonthFromNow) || 
                                        (x.StudyDate == oneWeekFromNow) ||
                                        (x.StudyDate == oneDay))
                                  .ToList();
            user.ForEach(u => 
            {
                if (u.StudyDate.Value.Date == oneMonthFromNow)
                {
                    SendEmail(u.ApplicationUser.Email, DaysMonth);
                }
                else if(u.StudyDate.Value.Date == oneWeekFromNow)
                {
                  SendEmail(u.ApplicationUser.Email, DaysWeek);
                }
                else if(u.StudyDate.Value.Date == oneDay)
                {
                    SendEmail(u.ApplicationUser.Email, OneDay);
                }
            });
        }
    }
}