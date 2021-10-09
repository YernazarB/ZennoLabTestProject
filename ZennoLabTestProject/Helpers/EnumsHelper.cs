using System;
using ZennoLabTestProject.Domain;

namespace ZennoLabTestProject.Helpers
{
    public static class EnumsHelper
    {
        public static string AnswerTypeString(this AnswerType answerType)
        {
            switch (answerType)
            {
                case AnswerType.Absent:
                    return "Отсутсвует";
                case AnswerType.OnName:
                    return "В именах файлов";
                case AnswerType.InSeparateFile:
                    return "В отдельном файле";
                default:
                    throw new ArgumentOutOfRangeException(nameof(answerType), answerType, null);
            }
        }
    }
}