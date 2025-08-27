using Robust.Shared.Serialization;

namespace Content.Shared._Green.Calligraph;

[Serializable, NetSerializable]
public sealed class CalligraphBoundUserInterfaceState(List<SignRecord> records) : BoundUserInterfaceState
{
    public readonly List<SignRecord> Records = records;
}

[Serializable, NetSerializable]
public enum CalligraphUiKey
{
    Key
}
