using MediatR;
using ZennoLabTestProject.Dtos;

namespace ZennoLabTestProject.Queries
{
    public class CreateUserDataSetQuery : IRequest<string>
    {
        public UserDataSetDto UserDataSet { get; set; }
    }
}