using Content.Shared._Green.StationGoal;
using Robust.Shared.Prototypes;

namespace Content.Server._Green.StationGoal;

[RegisterComponent]
public sealed partial class StationGoalComponent : Component
{
    [DataField]
    public List<ProtoId<StationGoalPrototype>> Goals = [];
}
