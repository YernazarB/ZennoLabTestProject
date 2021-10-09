using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZennoLabTestProject.Domain;
using ZennoLabTestProject.Domain.Interfaces;
using ZennoLabTestProject.Queries;

namespace ZennoLabTestProject.QueryHandlers
{
    public class CreateUserDataSetQueryHandler : IRequestHandler<CreateUserDataSetQuery, string>
    {
        private readonly IRepository<UserDataSet> _repository;
        public CreateUserDataSetQueryHandler(IRepository<UserDataSet> repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(CreateUserDataSetQuery query, CancellationToken cancellationToken)
        {
            var guid = Guid.NewGuid();
            var path = await SaveZipFile(query.UserDataSet.ZipFile, guid);

            var message = await CheckQuery(query, path);
            if (!string.IsNullOrWhiteSpace(message))
            {
                DeleteSavedDirectory(path);
                return message;
            }

            var entity = new UserDataSet
            {
                Name = query.UserDataSet.Name,
                AnswerType = query.UserDataSet.AnswerType,
                HasCyrillic = query.UserDataSet.HasCyrillic,
                HasLatin = query.UserDataSet.HasLatin,
                HasNumber = query.UserDataSet.HasNumber,
                Guid = guid,
                DateTime = query.UserDataSet.DateTime,
                CaseSensitive = query.UserDataSet.CaseSensitive,
                HasSymbol = query.UserDataSet.HasSymbol
            };
            await _repository.Save(entity);

            return message;
        }

        private static void DeleteSavedDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            Directory.Delete(path, true);
        }

        private static async Task<string> SaveZipFile(byte[] zipFile, Guid guid)
        {
            if (!zipFile.Any())
            {
                return string.Empty;
            }

            var path = @$"{Directory.GetCurrentDirectory()}\DataSets";
            var zipFilePath = @$"{path}\{guid}.zip";
            string fileName;
            await using (var memoryStream = new MemoryStream(zipFile))
            {
                await using var stream = new FileStream(zipFilePath, FileMode.Create);
                await memoryStream.CopyToAsync(stream);
                using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read);
                fileName = zipArchive.Entries.First().FullName.Replace("/", string.Empty);
            }

            ZipFile.ExtractToDirectory(zipFilePath, path);
            var guidFileName = @$"{path}\{guid}";
            Directory.Move(@$"{path}\{fileName}", guidFileName);
            File.Delete(zipFilePath);
            return guidFileName;
        }

        private static async Task<string> CheckQuery(CreateUserDataSetQuery query, string path)
        {
            var errorMessage = string.Empty;
            if (!Regex.IsMatch(query.UserDataSet.Name, @"^[a-zA-Z]+$"))
            {
                errorMessage = "Имя должна содержать только латинские буквы!";
            }

            if (query.UserDataSet.Name.Contains("captcha"))
            {
                errorMessage = @"Имя не может содержать слово ""captcha""!";
            }

            if (!(query.UserDataSet.HasCyrillic || query.UserDataSet.HasLatin || query.UserDataSet.HasNumber))
            {
                errorMessage = @"Нужно выбрать как минимум одно из: ""Содержит кириллицу"", ""Содержит латиницу"", ""Содержит цифры""!";
            }

            if (string.IsNullOrWhiteSpace(path) || 
                query.UserDataSet.AnswerType != AnswerType.InSeparateFile) 
                return errorMessage;

            var files = Directory.GetFiles(path);
            const string answersFileName = "answers.txt";
            if (files.All(x => x != answersFileName))
            {
                errorMessage = "Отсутствует файл с ответами!";
            }

            var text = await File.ReadAllTextAsync($@"{path}\{answersFileName}");
            if (text.Split("\r\n".ToCharArray()).Length != files.Length - 1)
            {
                errorMessage = "Количество ответов не совпадает с количеством картинок!";
            }

            return errorMessage;
        }
    }
}