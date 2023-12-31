﻿using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
//using AspNetCore.Reporting;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Reporting.NETCore;
using System.Xml.Linq;

namespace Acorna.Service.Project.BillingSystem
{
    internal class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _baseTempFolder = "TempFiles";
        private readonly string _baseReportFolder = "ReportFiles";
        private string _rootPath = "";

        internal ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public byte[] GenerateCallSummaryReport(CallsInfoFilterModel model, string rootPath, string reportName)
        {
            var report = GetLocalReport(reportName, rootPath);
            int countRecord = 0;
            var list = _unitOfWork.CallDetailsReportRepository.GetCallSummary(model, out countRecord);

            report.DataSources.Add(new ReportDataSource("ReportDataSet", list));

            var parameters = new[] {
              new ReportParameter("DateFrom", model.FromDate),
              new ReportParameter("DateTo", model.ToDate),
              new ReportParameter("GroupName", GetGroupName(model)),
              new ReportParameter("IsSubmitted", GetIsSubmitted(model)),
        };

            report.SetParameters(parameters);


            var result = report.Render(renderRepotType(model.ReportType));


            return result;
        }

        public byte[] GenerateCallDetailsReport(CallsInfoFilterModel model, string rootPath, string reportName)
        {
            var report = GetLocalReport(reportName, rootPath);

            int countRecord = 0;
            var list = _unitOfWork.CallDetailsReportRepository.GetCallDetails(model, out countRecord);

            report.DataSources.Add(new ReportDataSource("ReportDataSet", list));

            var parameters = new[] {
            new ReportParameter("DateFrom", model.FromDate),
            new ReportParameter("DateTo", model.ToDate),
            new ReportParameter("GroupName", GetGroupName(model)),
            new ReportParameter("UserName", GetUserName(model)),
            new ReportParameter("ServiceTypeName", GetServiceTypeName(model)),
            new ReportParameter("CountryName", GetCountryName(model)),
            new ReportParameter("TypePhoneNumberName", GetTypePhoneNumberName(model)),
             };

            report.SetParameters(parameters);
            int ext = (int)(DateTime.Now.Ticks >> 10);
            var result = report.Render(renderRepotType(model.ReportType));

            return result;
        }

        public byte[] GenerateCallFinanceReport(CallsInfoFilterModel model, string rootPath, string reportName)
        {
            var report = GetLocalReport(reportName, rootPath);

            int countRecord = 0;
            var list = _unitOfWork.CallDetailsReportRepository.GetCallFinance(model, out countRecord);

           // report.DataSources.Add(new ReportDataSource("ReportDataSet", list));
            report.DataSources.Add(new ReportDataSource("ReportDataSet", list));

           
            var parameters = new[] {
            new ReportParameter("DateFrom", model.FromDate),
            new ReportParameter("DateTo", model.ToDate),
            new ReportParameter("GroupName", GetGroupName(model)),
             };

            report.SetParameters(parameters);
            var result = report.Render(renderRepotType(model.ReportType));

            return result;
        }


        public string GetReportUrl(byte[] reportByte, string rootPath, string reportName, string reportType, string scheme, string host, bool deleteOldTempFiles = true)
        {
            var subPath = System.IO.Path.Combine(rootPath, _baseTempFolder);

            bool exists = System.IO.Directory.Exists(subPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(subPath);

            var fileName = reportName + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "." + reportType;
            var path = subPath + "\\" + fileName;

            System.IO.File.WriteAllBytes(path, reportByte);

            var _baseURL = $"{scheme}://{host}";

            var urlPath = $"{_baseURL}/api/DownloadFile/Download?file={fileName}";

            if (deleteOldTempFiles)
            {
                _rootPath = rootPath;
                DeleteOldTempFilesThread();
            }
            return urlPath;
        }

        #region Common Functions

        private LocalReport GetLocalReport(string reportName, string rootPath)
        {
            string reportPath = string.Format("{0}\\{1}\\{2}.rdlc", rootPath, _baseReportFolder, reportName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");

            var document = XDocument.Load(reportPath);

            var stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0L;

            using (var fileStrem = new FileStream(reportPath, FileMode.Open, FileAccess.Read))
            {
                LocalReport report = new();
                report.LoadReportDefinition(fileStrem);
                return report;
            }

           
        }

        public string renderRepotType(string reportType)
        {
            switch (reportType.ToLower())
            {
                default:
                case "pdf":
                    reportType = "pdf";
                    break;
                case "doc":
                    reportType = "Word";
                    break;
                case "xls":
                    reportType = "Excel";
                    break;
            }

            return reportType;
        }

        private void DeleteOldTempFilesThread()
        {

            var deleteThread = new Thread(this.DeleteOldTempFiles);
            deleteThread.Start();
        }

        /// <summary>
        /// This for delete old temp reports files
        /// </summary>
        private void DeleteOldTempFiles()
        {
            var path = System.IO.Path.Combine(_rootPath, _baseTempFolder);

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-1))
                    fi.Delete();
            }
        }


        private string GetGroupName(CallsInfoFilterModel model)
        {
            var groupName = "";
            if (model.GroupId != null)
            {
                var group = _unitOfWork.GetRepository<Group>().FirstOrDefault(a => a.Id == model.GroupId.Value);
                if (group != null)
                    groupName = group != null ? (model.Lang == "ar" ? group.GroupNameAr : group.GroupNameEn) : groupName;
            }
            return groupName;
        }

        private string GetUserName(CallsInfoFilterModel model)
        {
            var userName = "";
            if (model.UserId != null)
            {
                var user = _unitOfWork.SecurityRepository.GetUserById(model.UserId.Value).Result;
                if (user != null)
                    userName = user != null ? user.UserName : userName;
            }
            return userName;
        }

        private string GetServiceTypeName(CallsInfoFilterModel model)
        {
            var serviceTypeName = "";
            if (model.ServiceUsedId != null)
            {
                var serviceType = _unitOfWork.GetRepository<ServiceUsed>().FirstOrDefault(a => a.Id == model.ServiceUsedId.Value);
                if (serviceType != null)
                    serviceTypeName = serviceType != null ? (model.Lang == "ar" ? serviceType.ServiceUsedNameAr : serviceType.ServiceUsedNameEn) : serviceTypeName;
            }

            return serviceTypeName;
        }

        private string GetCountryName(CallsInfoFilterModel model)
        {
            var countryName = "";
            if (model.CountryId != null)
            {
                var country = _unitOfWork.GetRepository<ServiceUsed>().FirstOrDefault(a => a.Id == model.CountryId.Value);
                if (country != null)
                    countryName = country != null ? (model.Lang == "ar" ? country.ServiceUsedNameAr : country.ServiceUsedNameEn) : countryName;
            }

            return countryName;
        }

        private string GetTypePhoneNumberName(CallsInfoFilterModel model)
        {
            var typePhoneNumberName = "";
            if (model.TypePhoneNumberId != null)
            {
                var typePhoneNumber = _unitOfWork.GetRepository<TypePhoneNumber>().FirstOrDefault(a => a.Id == model.TypePhoneNumberId.Value);
                if (typePhoneNumber != null)
                    typePhoneNumberName = typePhoneNumber != null ? (model.Lang == "ar" ? typePhoneNumber.TypeNameAr : typePhoneNumber.TypeNameEn) : typePhoneNumberName;
            }

            return typePhoneNumberName;
        }

        private string GetIsSubmitted(CallsInfoFilterModel model)
        {
            var isSubmitted = "";
            if (model.IsSubmitted != null)
            {
                if (model.Lang == "ar")
                {
                    isSubmitted = model.IsSubmitted.Value ? "نعم" : "لا";
                }
                else
                {
                    isSubmitted = model.IsSubmitted.Value ? "Yes" : "No";
                }
            }
            return isSubmitted;
        }
        #endregion


    }
}
