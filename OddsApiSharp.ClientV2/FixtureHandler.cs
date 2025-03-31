using OddsApiSharp.ClientV2.Containers;
using OddsApiSharp.ClientV2.Containers.Websocket;

public class FixtureDataWrapper
{
    private readonly Fixture _fixtureData;

    public event EventHandler<Fixture> FixtureUpdated;

    public FixtureDataWrapper(Fixture fixtureData)
    {
        _fixtureData = fixtureData;
    }

    public void HandleWsUpdate(WsMsg updateDataMsg)
    {
        if (updateDataMsg.Channel == "fixtures")
        {

            return;
        }

        var bookmakerOdds = _fixtureData.Odds.FirstOrDefault(x => x.Bookmaker == updateDataMsg.Data.Bookmaker);

        if (bookmakerOdds == null)
            return;

        var bookmakerOutcomes = bookmakerOdds.Markets.SelectMany(x => x.Outcomes).ToList();

        foreach (var (key, outcomes) in updateDataMsg.Data.Outcomes)
        {
            foreach (var outcome in outcomes)
            {
                var updatedOdds = bookmakerOutcomes.FirstOrDefault(x => outcome.OddsId == x.OddsId);

                if (updatedOdds == null)
                    continue;

                if (outcome.Active.HasValue)
                    updatedOdds.Active = outcome.Active;

                if (outcome.Betslip != null)
                    updatedOdds.Betslip = outcome.Betslip;

                if (outcome.BookmakerOutcomeId != null)
                    updatedOdds.BookmakerOutcomeId = outcome.BookmakerOutcomeId;

                if (outcome.ChangedAt.HasValue)
                    updatedOdds.ChangedAt = outcome.ChangedAt;

                if (outcome.OddsId.HasValue)
                    updatedOdds.OddsId = outcome.OddsId;

                if (outcome.OutcomeId.HasValue)
                    updatedOdds.OutcomeId = outcome.OutcomeId;

                if (outcome.OutcomeName != null)
                    updatedOdds.OutcomeName = outcome.OutcomeName;

                if (outcome.PlayerId.HasValue)
                    updatedOdds.PlayerId = outcome.PlayerId;

                if (outcome.Price.HasValue)
                    updatedOdds.Price = outcome.Price;
            }
        }
    }
}