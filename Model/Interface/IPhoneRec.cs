using Model.DB;
using System;
using System.Collections.Generic;
using System.Text;
using Model.Interfaces;


namespace Model.Interface
{
    public interface IPhoneRec : IBaseRepository<PhoneRec>
    {
        void Create(string phoneNumber, string userId);
        PhoneRec SearchByPhone(string phoneNumber);
        List<PhoneRec> GetByUserId(string userId);
    }
}
