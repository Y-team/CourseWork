using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using static System.Net.WebRequestMethods;

namespace BAL.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        }
        //private string GetLogo(Operator oper)
        //{
        //    string filePath = "wwwroot/images/OperatorLogo/Logo_Id=" + Convert.ToString(oper.Id) + ".png";
        //    if (File.Exists(filePath))
        //    {
        //        return "/images/OperatorLogo/Logo_Id=" + Convert.ToString(oper.Id) + ".png";
        //    }
        //    else
        //        return null;
        //}
    }
}
