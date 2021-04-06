using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using Acorna.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GroupModel>> GetAllGroups()
        {
            try
            {
                List<Group> groups = await _unitOfWork.GetRepository<Group>().GetAllAsync();
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
                PaginationRecord<Group> group = await _unitOfWork.GetRepository<Group>().GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
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
                Group group = await _unitOfWork.GetRepository<Group>().GetSingleAsync(groupId);
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
                return _unitOfWork.GetRepository<Group>().GetTotalCount();
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
                    _unitOfWork.GetRepository<Group>().Insert(group);
                    _unitOfWork.SaveChanges();
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
                Group group = _unitOfWork.GetRepository<Group>().GetSingle(groupModel.Id);


                if (group != null)
                {
                    group.GroupNameAr = groupModel.GroupNameAr;
                    group.GroupNameEn = groupModel.GroupNameEn;

                    _unitOfWork.GetRepository<Group>().Update(group);
                    _unitOfWork.SaveChanges();
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
                Group group = _unitOfWork.GetRepository<Group>().GetSingle(id);

                if (group != null)
                {
                    _unitOfWork.GetRepository<Group>().Delete(group);
                    _unitOfWork.SaveChanges();
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
