using Acorna.CommonMember;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Repository;
using Acorna.Core.Repository.Project.BillingSystem.Report;
using Acorna.DTOs.Security;
using Acorna.Repository.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

using System.Threading.Tasks;

internal class CallDetailsViewRepository : ICallDetailsReportRepository
{
	private readonly IDbFactory _dbFactory;

	internal CallDetailsViewRepository(IDbFactory dbFactory)
	{
		_dbFactory = dbFactory;
	}


	public List<CallDetailsDTO> GetCallDetails(CallsInfoFilterModel filter, out int countRecord)
	{
		var param = new SqlParameter[] {
						new SqlParameter() {
							ParameterName = "@Count",
							SqlDbType =  System.Data.SqlDbType.BigInt,
							Direction = System.Data.ParameterDirection.Output,
						},
						new SqlParameter() {
							ParameterName = "@FromDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = !String.IsNullOrEmpty(filter.FromDate) ? filter.ToDate : System.Data.SqlTypes.SqlString.Null
						},
						new SqlParameter() {
							ParameterName = "@ToDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = !String.IsNullOrEmpty(filter.ToDate) ? filter.ToDate : System.Data.SqlTypes.SqlString.Null
						},
						new SqlParameter() {
							ParameterName = "@UserId",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.UserId.HasValue ? filter.UserId.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@GroupId",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.GroupId.HasValue? filter.GroupId.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@ServiceTypeId",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.ServiceTypeId.HasValue? filter.ServiceTypeId.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@CountryId",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.CountryId.HasValue? filter.CountryId.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@CountryIdExclude",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.CountryIdExclude.HasValue? filter.CountryIdExclude.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@PhoneTypeId",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.PhoneTypeId.HasValue? filter.PhoneTypeId.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@TypePhoneNumberId",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.TypePhoneNumberId.HasValue? filter.TypePhoneNumberId.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@PageIndex",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.PageIndex.HasValue? filter.PageIndex.Value : 0
						},
						new SqlParameter() {
							ParameterName = "@PageSize",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.PageSize.HasValue? filter.PageSize.Value : 10
						}};

		var list = _dbFactory.DataContext.CallDetails.FromSqlRaw("[dbo].[GetCallDetails] @Count OUTPUT, @FromDate, @ToDate, @UserId, @GroupId, @ServiceTypeId, @CountryId, @CountryIdExclude, @PhoneTypeId, @TypePhoneNumberId, @PageIndex, @PageSize", param).ToList();
		countRecord = param[0].Value != null ?  Convert.ToInt32(param[0].Value): 0;
		return list;
	}


	public List<CallSummaryDTO> GetCallSummary(CallsInfoFilterModel filter, out int countRecord)
	{
		var param = new SqlParameter[] {
						new SqlParameter() {
							ParameterName = "@Count",
							SqlDbType =  System.Data.SqlDbType.BigInt,
							Direction = System.Data.ParameterDirection.Output,
						},

						new SqlParameter() {
							ParameterName = "@FromCallDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = !string.IsNullOrEmpty(filter.FromDate) ? filter.FromDate : System.Data.SqlTypes.SqlString.Null
						},
						new SqlParameter() {
							ParameterName = "@ToCallDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = !string.IsNullOrEmpty(filter.ToDate) ? filter.ToDate : System.Data.SqlTypes.SqlString.Null
						},

						new SqlParameter() {
							ParameterName = "@FromBillDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = System.Data.SqlTypes.SqlString.Null
						},
						new SqlParameter() {
							ParameterName = "@ToBillDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = System.Data.SqlTypes.SqlString.Null
						},

						new SqlParameter() {
							ParameterName = "@GroupId",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.GroupId.HasValue? filter.GroupId.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@IsSubmitted",
							SqlDbType =  System.Data.SqlDbType.Bit,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.IsSubmitted.HasValue? filter.IsSubmitted.Value : System.Data.SqlTypes.SqlBoolean.Null
						},
						new SqlParameter() {
							ParameterName = "@PageIndex",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.PageIndex.HasValue? filter.PageIndex.Value : 0
						},
						new SqlParameter() {
							ParameterName = "@PageSize",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.PageSize.HasValue? filter.PageSize.Value : 10
						}};

		var list = _dbFactory.DataContext.CallSummary.FromSqlRaw("[dbo].[GetCallSummary] @Count OUTPUT, @FromCallDate, @ToCallDate, @FromBillDate, @ToBillDate, @GroupId, @IsSubmitted, @PageIndex, @PageSize", param).ToList();
		countRecord = param[0].Value != null ? Convert.ToInt32(param[0].Value) : 0;
		return list;
	}

	public List<CallFinanceDTO> GetCallFinance(CallsInfoFilterModel filter, out int countRecord)
	{
		var param = new SqlParameter[] {
						new SqlParameter() {
							ParameterName = "@Count",
							SqlDbType =  System.Data.SqlDbType.BigInt,
							Direction = System.Data.ParameterDirection.Output,
						},

						new SqlParameter() {
							ParameterName = "@FromCallDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = System.Data.SqlTypes.SqlString.Null
						},
						new SqlParameter() {
							ParameterName = "@ToCallDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = System.Data.SqlTypes.SqlString.Null
						},

						new SqlParameter() {
							ParameterName = "@FromBillDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = !String.IsNullOrEmpty(filter.FromDate) ? filter.FromDate : System.Data.SqlTypes.SqlString.Null
						},
						new SqlParameter() {
							ParameterName = "@ToBillDate",
							SqlDbType =  System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input,
							Value = !String.IsNullOrEmpty(filter.ToDate) ? filter.ToDate : System.Data.SqlTypes.SqlString.Null
						},

						new SqlParameter() {
							ParameterName = "@GroupId",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.GroupId.HasValue? filter.GroupId.Value : System.Data.SqlTypes.SqlInt32.Null
						},
						new SqlParameter() {
							ParameterName = "@IsSubmitted",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value =  System.Data.SqlTypes.SqlBoolean.Null
						},
						new SqlParameter() {
							ParameterName = "@PageIndex",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.PageIndex.HasValue? filter.PageIndex.Value : 0
						},
						new SqlParameter() {
							ParameterName = "@PageSize",
							SqlDbType =  System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input,
							Value = filter.PageSize.HasValue? filter.PageSize.Value : 10
						}};

		var list = _dbFactory.DataContext.CallFinance.FromSqlRaw("[dbo].[GetCallSummary] @Count OUTPUT, @FromCallDate, @ToCallDate, @FromBillDate, @ToBillDate, @GroupId, @IsSubmitted, @PageIndex, @PageSize", param).ToList();
		countRecord = param[0].Value != null ? Convert.ToInt32(param[0].Value) : 0;
		return list;
	}
}