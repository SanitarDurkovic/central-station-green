using Robust.Shared.Serialization;

namespace Content.Shared._Green.Calligraph;

[DataDefinition, Serializable, NetSerializable]
public partial struct SignRecord
{
    [DataField]
    public string Name;

    [DataField]
    public string? Handwriting;

    [DataField]
    public string? Author;
}
