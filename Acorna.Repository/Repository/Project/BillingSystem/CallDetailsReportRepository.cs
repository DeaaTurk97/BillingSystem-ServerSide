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
using System.Text;
using System.Threading.Tasks;

internal class CallDetailsReportRepository : ICallDetailsReportRepository
{
    private readonly IDbFactory _dbFactory;

    internal CallDetailsReportRepository(IDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }


    public Task<List<CallDetailsReportDTO>> GetReport(CallDetailsReportModel filter)
    {
        var list = _dbFactory.DataContext.CallDetailsReport.FromSqlRaw("[dbo].[GetCallDetailsReport]").ToListAsync();

        return list;
    }
  
}