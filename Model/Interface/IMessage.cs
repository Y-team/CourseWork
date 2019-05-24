using Model.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Interfaces;


namespace Model.Interface
{
    public interface IMessage : IBaseRepository<Message>
    {
      //  void Create(DateTime dataStart, int period, DateTime dateEnd ,int phoneId,int userMesId );
        void Create(int userMessageId, int phoneRecId);
      /*  Message SearchByCreateDate(DateTime dateTime);


        public Message SearchByCreateDate(DateTime dateTime)
        {
            Message message = context.Messages.FirstOrDefault(p => p.DateCreate == dateTime));
            return message;
        }*/
    }
}
