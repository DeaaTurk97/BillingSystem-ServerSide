using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Acorna.Controllers.Base;

namespace Acorna.Controllers.Documents
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : TeamControllerBase
    {
        IConfiguration _config;
        public DocumentsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string filePath)
        {
            byte[] downloadedFile = new byte[] { };
            try
            {
                downloadedFile = await System.IO.File.ReadAllBytesAsync(filePath);
                return Ok(downloadedFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadDocuments()
        {
            List<DocumentModel> uploadedDocuments = new List<DocumentModel>();
            try
            {
                string filePath = _config["StoredFilesPath"];
                System.IO.FileInfo file = new System.IO.FileInfo(filePath);
                file.Directory.Create();

                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        String uploadedFileName = Path.GetRandomFileName();
                        var fullFilePath = Path.Combine(filePath, uploadedFileName);
                        using (var stream = System.IO.File.Create(fullFilePath))
                        {
                            await formFile.CopyToAsync(stream);
                            uploadedDocuments.Add(new DocumentModel() { Key = uploadedFileName, URL = fullFilePath, Name = formFile.FileName });
                        }
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
            return Ok(uploadedDocuments);
        }

        [HttpDelete]
        public IActionResult Delete(string fileName)
        {
            try
            {
                string filePath = _config["StoredFilesPath"];
                var fullFilePath = Path.Combine(filePath, fileName);
                System.IO.File.Delete(fullFilePath);
            }
            catch (Exception)
            {
                BadRequest(false);
            }
            return Ok(true);
        }
    }
}
