using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IRepository<Group> groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<List<GroupModel>> GetAllGroups()
        {
            try
            {
                List<Group> job = await _groupRepository.GetAllAsync();
                return _mapper.Map<List<GroupModel>>(job); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationRecord<GroupModel>> GetGroups(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<Group> group = await _groupRepository.GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
                PaginationRecord<GroupModel> paginationRecordModel = new PaginationRecord<GroupModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<GroupModel>>(group.DataRecord),
                    CountRecord = group.CountRecord,
                };
                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GroupModel> GetGroupId(int groupId)
        {
            try
            {
                Group group = await _groupRepository.GetSingleAsync(groupId);
                return _mapper.Map<GroupModel>(group);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCountRecord()
        {
            try
            {
                return _groupRepository.GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddGroup(GroupModel groupModel)
        {
            try
            {
                Group group = _mapper.Map<Group>(groupModel);

                if (group != null)
                    _groupRepository.Insert(group);

                return group.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateGroup(GroupModel groupModel)
        {
            try
            {
                Group group =  _groupRepository.GetSingle(groupModel.Id);
                group.GroupNameAr = groupModel.GroupNameAr;
                group.GroupNameEn = groupModel.GroupNameEn;

                if (group != null)
                    _groupRepository.Update(group);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteGroup(int id)
        {
            try
            {
                Group group =  _groupRepository.GetSingle(id);

                if (group != null)
                    _groupRepository.Delete(group);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
