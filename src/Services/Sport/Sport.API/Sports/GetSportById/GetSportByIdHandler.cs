namespace Sport.API.Sports.GetSportById;

public record GetSportByIdQuery(Guid Id) : IQuery<GetSportByIdResult>;

public record GetSportByIdResult(Models.Sport Sport);

internal class GetSportByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetSportByIdQuery, GetSportByIdResult>
{
    public async Task<GetSportByIdResult> Handle(GetSportByIdQuery query, CancellationToken cancellationToken)
    {
        var sport = await session.LoadAsync<Models.Sport>(query.Id, cancellationToken);
        if (sport is null)
            throw new SportNotFoundException(query.Id);

        return new GetSportByIdResult(sport);
    }
}