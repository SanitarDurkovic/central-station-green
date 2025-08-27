using Robust.Shared.Audio;

namespace Content.Shared._Green.Calligraph;

[RegisterComponent]
public sealed partial class CalligraphComponent : Component
{
    [DataField]
    public bool AllStations;

    [DataField]
    public SoundSpecifier? ScanSound = new SoundPathSpecifier("/Audio/Machines/scanning.ogg");
}
