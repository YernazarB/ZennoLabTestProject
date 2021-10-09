using System;

namespace ZennoLabTestProject.Domain
{
    public class UserDataSet
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public bool HasCyrillic { get; set; }
        public bool HasLatin { get; set; }
        public bool HasNumber { get; set; }
        public bool HasSymbol { get; set; }
        public bool CaseSensitive { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}