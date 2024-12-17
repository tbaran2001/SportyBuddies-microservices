namespace Sport.API.Sports.GetSports;

public record GetSportsQuery : IQuery<GetSportsResult>;

public record GetSportsResult(IEnumerable<Models.Sport> Sports);

internal class GetSportsQueryHandler(IDocumentSession session) : IQueryHandler<GetSportsQuery, GetSportsResult>
{
    public async Task<GetSportsResult> Handle(GetSportsQuery query, CancellationToken cancellationToken)
    {
        var sports = await session.Query<Models.Sport>().ToListAsync(cancellationToken);
        return new GetSportsResult(sports);
    }
}