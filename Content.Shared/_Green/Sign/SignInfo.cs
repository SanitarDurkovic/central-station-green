using Robust.Shared.Serialization;

namespace Content.Shared._Green.Sign;

[DataDefinition, Serializable, NetSerializable]
public partial struct SignInfo
{
    [DataField]
    public string Name;

    [DataField]
    public string? Handwriting;
}
