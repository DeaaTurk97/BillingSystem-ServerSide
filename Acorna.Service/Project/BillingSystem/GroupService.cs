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
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public GroupService(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GroupModel>> GetAllGroups()
        {
            try
            {
                List<Group> groups = await _iUnitOfWork.GetRepository<Group>().GetAllAsync();
                return _mapper.Map<List<GroupModel>>(groups);
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
                PaginationRecord<Group> group = await _iUnitOfWork.GetRepository<Group>().GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
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
                Group group = await _iUnitOfWork.GetRepository<Group>().GetSingleAsync(groupId);
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
                return _iUnitOfWork.GetRepository<Group>().GetTotalCount();
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
                {
                    _iUnitOfWork.GetRepository<Group>().Insert(group);
                    _iUnitOfWork.SaveChanges();
                }

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
                Group group = _iUnitOfWork.GetRepository<Group>().GetSingle(groupModel.Id);


                if (group != null)
                {
                    group.GroupNameAr = groupModel.GroupNameAr;
                    group.GroupNameEn = groupModel.GroupNameEn;
                    _iUnitOfWork.GetRepository<Group>().Update(group);
                    _iUnitOfWork.SaveChanges();
                }

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
                Group group = _iUnitOfWork.GetRepository<Group>().GetSingle(id);

                if (group != null)
                {
                    _iUnitOfWork.GetRepository<Group>().Delete(group);
                    _iUnitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
