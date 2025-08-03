using Content.Shared.Inventory;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared.Radio.Components;

/// <summary>
///     This component relays radio messages to the parent entity's chat when equipped.
/// </summary>
[RegisterComponent]
public sealed partial class HeadsetComponent : Component
{
    [DataField("enabled")]
    public bool Enabled = true;

    public bool IsEquipped = false;

    [DataField("requiredSlot")]
    public SlotFlags RequiredSlot = SlotFlags.EARS;

    // Green-HeadsetSound-Start
    [DataField]
    public SoundSpecifier Sound;

    [DataField]
    public HashSet<ProtoId<RadioChannelPrototype>> ToggledSoundChannels = [];
    // Green-HeadsetSound-End
}
