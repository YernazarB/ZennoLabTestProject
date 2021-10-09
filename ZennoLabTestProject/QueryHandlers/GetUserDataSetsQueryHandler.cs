using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZennoLabTestProject.Domain;
using ZennoLabTestProject.Domain.Interfaces;
using ZennoLabTestProject.Dtos;
using ZennoLabTestProject.Helpers;
using ZennoLabTestProject.Queries;

namespace ZennoLabTestProject.QueryHandlers
{
    public class GetUserDataSetsQueryHandler : IRequestHandler<GetUserDataSetsQuery, List<UserDataSetDto>>
    {
        private readonly IRepository<UserDataSet> _repository;
        public GetUserDataSetsQueryHandler(IRepository<UserDataSet> repository)
        {
            _repository = repository;
        }

        public async Task<List<UserDataSetDto>> Handle(GetUserDataSetsQuery request, CancellationToken cancellationToken)
        {
            return (await _repository.Get()).Select(x => new UserDataSetDto
            {
                AnswerType = x.AnswerType,
                Name = x.Name,
                HasCyrillic = x.HasCyrillic,
                HasLatin = x.HasLatin,
                HasNumber = x.HasNumber,
                CaseSensitive = x.CaseSensitive,
                DateTime = x.DateTime,
                HasSymbol = x.HasSymbol
            }).ToList();
        }
    }
}