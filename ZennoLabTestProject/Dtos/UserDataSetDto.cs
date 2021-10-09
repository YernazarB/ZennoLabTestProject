using System;
using Microsoft.AspNetCore.Http;
using ZennoLabTestProject.Domain;

namespace ZennoLabTestProject.Dtos
{
    public class UserDataSetDto
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public bool HasCyrillic { get; set; }
        public bool HasLatin { get; set; }
        public bool HasNumber { get; set; }
        public bool HasSymbol { get; set; }
        public bool CaseSensitive { get; set; }
        public AnswerType AnswerType { get; set; }
        public byte[] ZipFile { get; set; } 
    }
}