using AutoMapper;
using invensys.auth.infrastructure.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invensys.auth.application.Endpoints.AuthClients.Queries
{
    public class GetAuthClientsQuery : IRequest<AuthClientDTO>
    {
        public Guid ClientId { get; init; }
    }

    public class GetAuthClientQueryHandler : IRequestHandler<GetAuthClientsQuery, AuthClientDTO>
    {
        public GetAuthClientQueryHandler(AuthenticationServerContext context, IMapper mapper)
        {

        }

        public Task<AuthClientDTO> Handle(GetAuthClientsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
