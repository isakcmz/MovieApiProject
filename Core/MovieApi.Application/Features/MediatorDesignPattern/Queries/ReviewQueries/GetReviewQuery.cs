using MediatR;
using MovieApi.Application.Features.MediatorDesignPattern.Results.ReviewResults;

namespace MovieApi.Application.Features.MediatorDesignPattern.Queries.ReviewQueries
{
    public class GetReviewQuery : IRequest<List<GetReviewQueryResult>>
    {
    }
}
