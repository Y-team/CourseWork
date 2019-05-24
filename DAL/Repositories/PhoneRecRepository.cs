using Model.DB;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Repositories;
using WebCustomerApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BAL.Repository
{
    public class PhoneRecRepository : BaseRepository<PhoneRec>, IPhoneRec
    {
        public PhoneRecRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Create(string phoneNumber,string userId)
        {
            PhoneRec phone = new PhoneRec() { PhoneNumber = phoneNumber ,UserId = userId };
            context.PhoneRecs.Add(phone);
            context.SaveChanges();
        }

        public PhoneRec SearchByPhone(string phoneNumber)
        {
            PhoneRec phone = context.PhoneRecs.FirstOrDefault(p => p.PhoneNumber == phoneNumber);
            return phone;
        }

        public List<PhoneRec> GetByUserId(string userId)
        {
            return context.PhoneRecs.Where(item => item.UserId == userId).ToList();
        }
    }
}
