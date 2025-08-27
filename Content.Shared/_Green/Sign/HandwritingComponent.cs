using Robust.Shared.GameStates;

namespace Content.Shared._Green.Sign;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class HandwritingComponent : Component
{
    [DataField, AutoNetworkedField]
    public string? Handwriting;
}
