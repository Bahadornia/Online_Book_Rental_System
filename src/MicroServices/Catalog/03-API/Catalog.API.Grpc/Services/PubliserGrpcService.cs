using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Responses;
using Catalog.ApplicationServices.Queries;
using MapsterMapper;
using MediatR;
using ProtoBuf.Grpc;

namespace Catalog.API.Grpc.Services
{
    public class PubliserGrpcService : IPublisherGrpcService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PubliserGrpcService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<GetPublisherRs>> GetPublishers(CallContext context)
        {
            var query = new GetPublishersQuery();
            var rs = await _mediator.Send(query, context.CancellationToken);
            return _mapper.Map<IReadOnlyCollection<GetPublisherRs>>(rs);
        }
    }
}
