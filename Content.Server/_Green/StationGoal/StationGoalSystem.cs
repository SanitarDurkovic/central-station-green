using Content.Server.Fax;
using Content.Shared._Green.StationGoal;
using Content.Shared.Fax.Components;
using Content.Shared.GameTicking;
using Content.Shared.Station;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server._Green.StationGoal;

public sealed class StationGoalSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedStationSystem _station = default!;
    [Dependency] private readonly FaxSystem _fax = default!;

    public bool SendStationGoalOnRoundStart { get; set; }

    public override void Initialize()
    {
        SubscribeLocalEvent<SendStationGoalsEvent>(OnSendStationGoals);
        SubscribeLocalEvent<RoundRestartCleanupEvent>(OnRoundRestartCleanup);
    }

    private void OnSendStationGoals(ref SendStationGoalsEvent e)
    {
        if (!SendStationGoalOnRoundStart)
            return;

        foreach (var station in _station.GetStations())
            SendStationGoal(station);
    }

    private void SendStationGoal(EntityUid station)
    {
        List<StationGoalPrototype> goals = [];

        if (TryComp<StationGoalComponent>(station, out var stationGoals))
            foreach (var goal in stationGoals.Goals)
                goals.Add(_prototype.Index(goal));
        else
            foreach (var goal in _prototype.EnumeratePrototypes<StationGoalPrototype>())
                if (goal.Implicit)
                    goals.Add(goal);

        SendStationGoal(station, _random.Pick(goals));
    }

    private void OnRoundRestartCleanup(RoundRestartCleanupEvent e)
    {
        SendStationGoalOnRoundStart = true;
    }

    public void SendStationGoal(EntityUid station, StationGoalPrototype goal)
    {
        FaxPrintout printout = new(Loc.GetString("station-goal-form", ("station", Name(station)), ("goal", Loc.GetString(goal.Text))), Loc.GetString("station-goal-name"), null, null, "paper_stamp-centcom", [new() { StampedName = Loc.GetString("stamp-component-stamped-name-centcom"), StampedColor = Color.FromHex("#006600") }]);

        var query = EntityQueryEnumerator<FaxMachineComponent>();
        while (query.MoveNext(out var entity, out var fax))
        {
            if (!fax.ReceiveAllStationGoals && !(fax.ReceiveStationGoal && _station.GetOwningStation(entity) == station))
                continue;

            _fax.Receive(entity, printout, component: fax);
        }
    }
}
