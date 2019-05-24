using Model.DB;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Model.Interfaces;

namespace BAL.Interface
{
    public interface IUserMessage :IBaseRepository<UserMessage>
    {
        void Create(string messageText,string Id);
       
    }
}
