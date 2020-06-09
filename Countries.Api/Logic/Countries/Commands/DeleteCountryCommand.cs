using System.Threading;
using System.Threading.Tasks;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;

namespace Countries.Api.Logic.Countries.Commands
{
	public class DeleteCountryCommand : IRequest<Unit>
	{
		public long Id { get;}
		public DeleteCountryCommand(long id)
		{
			Id = id;
		}
	}

	public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Unit>
	{
		private readonly IGenericRepository _countriesRepository;

		public DeleteCountryCommandHandler(IGenericRepository countriesRepository)
		{
			_countriesRepository = countriesRepository;
		}
		public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
		{
			await _countriesRepository.DeleteAsync<Country>(request.Id);
			return Unit.Value;
		}
	}
}
