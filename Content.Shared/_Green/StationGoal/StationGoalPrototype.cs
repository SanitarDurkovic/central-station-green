using Robust.Shared.Prototypes;

namespace Content.Shared._Green.StationGoal;

[Prototype]
public sealed partial class StationGoalPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public string Text = "";

    [DataField]
    public bool Implicit;
}
