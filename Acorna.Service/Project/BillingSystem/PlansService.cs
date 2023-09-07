using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    internal class PlansService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public PlansService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<List<PlanModel>> GetAllPlans()
        {
            try
            {
                List<Plan> plans = await _unitOfWork.GetRepository<Plan>().GetAllAsync();
                List<PlanModel> PlanModels = _mapper.Map<List<PlanModel>>(plans);
                return PlanModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationRecord<PlanModel>> GetPlan(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<Plan> plans = await _unitOfWork.GetRepository<Plan>().GetAllAsync(pageIndex, pageSize, x => x.Id, null, OrderBy.Descending, e => e.PlanServices);
                PaginationRecord<PlanModel> paginationRecordModel = new PaginationRecord<PlanModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<PlanModel>>(plans.DataRecord),
                    CountRecord = plans.CountRecord,
                };
                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PlanModel> GetPlanId(int planId)
        {
            try
            {
                Plan plan = await _unitOfWork.GetRepository<Plan>().GetAllAsync(planId);
                PlanModel planModel = _mapper.Map<PlanModel>(plan);
                return planModel;
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
                return _unitOfWork.GetRepository<Plan>().GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddPlan(PlanModel planModel)
        {
            try
            {
                Plan plan = _mapper.Map<Plan>(planModel);

                if (plan != null)
                {
                    plan.PlanServices = planModel.PlanServices.Select(e => new PlanService
                    {
                        ServiceUsedId = e.PlanService,
                        Limit = e.Limit,
                        Unit = e.Unit,
                        AdditionalUnit = e.AdditionalUnit,
                        AdditionalUnitPrice = e.AdditionalUnitPrice,
                        Id = 0,
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now
                    }).ToList();
                    _unitOfWork.GetRepository<Plan>().Insert(plan);

                    _unitOfWork.SaveChanges();
                }

                return plan.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdatePlan(PlanModel planModel)
        {
            Plan plan = _unitOfWork.GetRepository<Plan>().GetSingle(planModel.Id);

            if (plan != null)
            {
                plan.Code = planModel.Code;
                plan.Name = planModel.Name;
                plan.Description = planModel.Description;
                plan.Price = planModel.Price;
                var dbServices = _unitOfWork.GetRepository<PlanService>().GetWhere(e => e.PlanId == plan.Id);
                var toBeDeleted = dbServices.Where(e => !planModel.PlanServices.Any(p => p.PlanService == e.ServiceUsedId) && e.PlanId == planModel.Id);
                if (toBeDeleted.Any())
                {
                    _unitOfWork.GetRepository<PlanService>().DeleteRange(toBeDeleted.ToList());
                    //var users = _userManager.Users.Where(e => e.PlanId == plan.Id);
                    //// send emails
                    //foreach (var user in users)
                    //{
                    //    foreach (var item in toBeDeleted)
                    //    {
                    //        _unitOfWork.EmailRepository.ServiceRemoved(user.Email);
                    //    }
                    //}
                }
                _unitOfWork.SaveChanges();
                var toBeAdded = planModel.PlanServices.Where(e => !dbServices.Any(db => db.ServiceUsedId == e.PlanService));
                var toBeUpdatedd = planModel.PlanServices.Where(e => dbServices.Any(db => db.ServiceUsedId == e.PlanService));
                if (toBeAdded.Any())
                {
                    _unitOfWork.GetRepository<PlanService>().InsertRange(
                         toBeAdded.Select(e => new PlanService
                         {
                             ServiceUsedId = e.PlanService,
                             Limit = e.Limit,
                             Unit = e.Unit,
                             AdditionalUnit = e.AdditionalUnit,
                             AdditionalUnitPrice = e.AdditionalUnitPrice,
                             PlanId = plan.Id,
                             Id = 0,
                             CreatedBy = 1,
                             CreatedDate = DateTime.Now
                         }).ToList());

                    //var users = _userManager.Users.Where(e => e.PlanId == plan.Id);
                    //// send emails
                    //foreach (var user in users)
                    //{
                    //    foreach (var item in toBeDeleted)
                    //    {
                    //        _unitOfWork.EmailRepository.NewServiceAdded(user.Email);
                    //    }
                    //}
                }
                _unitOfWork.GetRepository<PlanService>().UpdateRange(
                     toBeUpdatedd.Select(e => new PlanService
                     {
                         ServiceUsedId = e.PlanService,
                         Limit = e.Limit,
                         Unit = e.Unit,
                         AdditionalUnit = e.AdditionalUnit,
                         AdditionalUnitPrice = e.AdditionalUnitPrice,
                         PlanId = plan.Id,
                         Id = 0,
                         CreatedBy = 1,
                         CreatedDate = DateTime.Now
                     }).ToList());
                plan.PlanServices = null;
                _unitOfWork.GetRepository<Plan>().Update(plan);
                _unitOfWork.SaveChanges();
            }

            return true;
        }

        public bool DeletePlan(int id)
        {
            try
            {
                Plan plan = _unitOfWork.GetRepository<Plan>().GetSingle(id);
                var services = _unitOfWork.GetRepository<PlanService>().GetWhere(e => e.PlanId == id);

                if (plan != null)
                {
                    _unitOfWork.GetRepository<PlanService>().DeleteRange(services.ToList());
                    _unitOfWork.GetRepository<Plan>().Delete(plan);
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
