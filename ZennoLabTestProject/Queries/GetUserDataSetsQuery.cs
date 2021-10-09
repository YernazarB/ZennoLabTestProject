using System.Collections.Generic;
using MediatR;
using ZennoLabTestProject.Dtos;

namespace ZennoLabTestProject.Queries
{
    public class GetUserDataSetsQuery : IRequest<List<UserDataSetDto>>
    {
    }
}