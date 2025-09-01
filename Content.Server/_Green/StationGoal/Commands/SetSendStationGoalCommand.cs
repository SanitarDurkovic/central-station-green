using Content.Server.Administration;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server._Green.StationGoal.Commands;

[AdminCommand(AdminFlags.Fun)]
public sealed class SetSendStationGoalCommand : IConsoleCommand
{
    [Dependency] private readonly IEntityManager _entity = default!;

    public string Command => "setsendstationgoal";

    public string Description => "Sets whether station goal should be sended on round start.";

    public string Help => "setsendstationgoal <should>";

    public void Execute(IConsoleShell shell, string arg, string[] args)
    {
        if (args.Length != 1)
        {
            shell.WriteError(Loc.GetString("shell-wrong-argument-number"));
            return;
        }

        if (!bool.TryParse(args[0], out var should))
        {
            shell.WriteError(Loc.GetString("shell-invalid-bool"));
            return;
        }

        _entity.System<StationGoalSystem>().SendStationGoalOnRoundStart = should;
    }

    public CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        if (args.Length == 1)
            return CompletionResult.FromHintOptions(CompletionHelper.Booleans, "should");

        return CompletionResult.Empty;
    }
}
