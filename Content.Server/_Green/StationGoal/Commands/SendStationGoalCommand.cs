using System.Linq;
using Content.Server.Administration;
using Content.Server.Commands;
using Content.Shared._Green.StationGoal;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.Prototypes;

namespace Content.Server._Green.StationGoal.Commands;

[AdminCommand(AdminFlags.Fun)]
public sealed class SendStationGoalCommand : IConsoleCommand
{
    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;

    public string Command => "sendstationgoal";

    public string Description => "Sends station goal to given station.";

    public string Help => "sendstationgoal <station> <goal>";

    public void Execute(IConsoleShell shell, string arg, string[] args)
    {
        if (args.Length != 2)
        {
            shell.WriteError(Loc.GetString("shell-wrong-argument-number"));
            return;
        }

        if (!NetEntity.TryParse(args[0], out var netEntity))
        {
            shell.WriteError(Loc.GetString("shell-invalid-entity-uid", ("uid", args[0])));
            return;
        }

        if (!_entity.TryGetEntity(netEntity, out var entity))
        {
            shell.WriteError(Loc.GetString("shell-could-not-find-entity-with-uid", ("uid", netEntity)));
            return;
        }

        if (!_prototype.TryIndex<StationGoalPrototype>(args[1], out var goal))
        {
            shell.WriteError(Loc.GetString("shell-argument-must-be-prototype", ("index", 1), ("prototypeName", "StationGoalPrototype")));
            return;
        }

        _entity.System<StationGoalSystem>().SendStationGoal(entity.Value, goal);
    }

    public CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        if (args.Length == 1)
            return CompletionResult.FromHintOptions(ContentCompletionHelper.StationIds(_entity), "station");

        if (args.Length == 2)
        {
            var goals = _prototype.EnumeratePrototypes<StationGoalPrototype>().Select(goal => goal.ID);

            return CompletionResult.FromHintOptions(goals, "goal");
        }

        return CompletionResult.Empty;
    }
}
