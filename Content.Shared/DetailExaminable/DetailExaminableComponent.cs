using Content.Shared._Green.Notes;
using Robust.Shared.GameStates;

namespace Content.Shared.DetailExaminable;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class DetailExaminableComponent : Component
{
    [DataField(required: true), AutoNetworkedField]
    public string Content = string.Empty;

    // Green-Notes-Start
    [DataField, AutoNetworkedField]
    public ErpPreference Erp = ErpPreference.No;
    // Green-Notes-End
}
