﻿using AutoMapper;
using Model.Interfaces;
using Model.ViewModels.GroupViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    /// <summary>
    /// Manager for Groups, include all methods needed to work with ApplicationGroup storage.
    /// Inherited from BaseManager and have additional methods.
    /// </summary>
    public class GroupManager : BaseManager, IGroupManager
    {
        public GroupManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        /// <summary>
        /// Get one group from db by Id
        /// </summary>
        /// <param name="id">Id of company wich you need</param>
        /// <returns>ViewModel of group from db</returns>
        public GroupViewModel Get(int id)
        {
            ApplicationGroup group = unitOfWork.ApplicationGroups.GetById(id);
            return mapper.Map<ApplicationGroup, GroupViewModel>(group);
        }

        /// <summary>
        /// Method for getting all groups from db
        /// </summary>
        /// <returns>IEnumerable of mapped to ViewModel objects</returns>
        public IEnumerable<GroupViewModel> GetGroups()
        {
            IEnumerable<ApplicationGroup> groups = unitOfWork.ApplicationGroups.GetAll();
            return mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<GroupViewModel>>(groups);
        }

        /// <summary>
        /// Method for inserting new group to db
        /// </summary>
        /// <param name="item">ViewModel of group</param>
        public void Insert(GroupViewModel item)
        {
            ApplicationGroup group = mapper.Map<GroupViewModel, ApplicationGroup>(item);
            unitOfWork.ApplicationGroups.Insert(group);
            unitOfWork.Save();
        }

        /// <summary>
        /// Update group in db
        /// </summary>
        /// <param name="item">ViewModel wich need to update in db</param>
        public void Update(GroupViewModel item)
        {
            ApplicationGroup group = mapper.Map<GroupViewModel, ApplicationGroup>(item);
            unitOfWork.ApplicationGroups.Update(group);
            unitOfWork.Save();
        }

        /// <summary>
        /// Delete group by Id
        /// </summary>
        /// <param name="id">Id of group wich need to delete</param>
        public void Delete(int id)
        {
            ApplicationGroup group = unitOfWork.ApplicationGroups.GetById(id);
            unitOfWork.ApplicationGroups.Delete(group);
            unitOfWork.Save();
        }
    }
}
