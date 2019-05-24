﻿using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace Model.Interfaces
{
    public interface IStopWordRepository : IBaseRepository<StopWord>
    {
        void Create(string word);

        StopWord SearchByWord(string word);
    }
}
