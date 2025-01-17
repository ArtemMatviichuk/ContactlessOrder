﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContactlessOrder.Common.Dto.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace ContactlessOrder.BLL.Infrastructure
{
    public class FileHelper
    {
        public async Task<string> SaveFile(IFormFile file, string path, string fileName = null)
        {
            var fileNameWithTimestamp =
                $"{fileName ?? Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now:yyyyMMddHHmmssffff}{Path.GetExtension(file.FileName)}";

            using (var fileStream = new FileStream(Path.Combine(path, fileNameWithTimestamp), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileNameWithTimestamp;
        }

        public async Task<FileDto> GetFile(string fileName, string path)
        {
            var filePath = Directory.GetFiles(path, fileName, SearchOption.AllDirectories).FirstOrDefault();
            if (filePath == null)
            {
                return null;
            }

            var bytes = await File.ReadAllBytesAsync(filePath);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return new FileDto
            {
                FileName = fileName,
                Bytes = bytes,
                ContentType = contentType
            };
        }
    }
}