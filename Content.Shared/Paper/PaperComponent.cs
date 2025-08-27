using Content.Shared._Green.Sign;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared.Paper;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class PaperComponent : Component
{
    public PaperAction Mode;
    [DataField("content"), AutoNetworkedField]
    public string Content { get; set; } = "";

    [DataField("contentSize")]
    public int ContentSize { get; set; } = 10000;

    [DataField("stampedBy"), AutoNetworkedField]
    public List<StampDisplayInfo> StampedBy { get; set; } = new();

    // Green-Signs-Start
    [DataField, AutoNetworkedField]
    public List<SignInfo> Signs { get; set; } = [];
    // Green-Signs-End

    /// <summary>
    ///     Stamp to be displayed on the paper, state from bureaucracy.rsi
    /// </summary>
    [DataField("stampState"), AutoNetworkedField]
    public string? StampState { get; set; }

    [DataField, AutoNetworkedField]
    public bool EditingDisabled;

    /// <summary>
    /// Sound played after writing to the paper.
    /// </summary>
    [DataField("sound")]
    public SoundSpecifier? Sound { get; private set; } = new SoundCollectionSpecifier("PaperScribbles", AudioParams.Default.WithVariation(0.1f));

    [Serializable, NetSerializable]
    public sealed class PaperBoundUserInterfaceState : BoundUserInterfaceState
    {
        public readonly string Text;
        public readonly List<StampDisplayInfo> StampedBy;
        public readonly List<SignInfo> Signs; // Green-Signs
        public readonly PaperAction Mode;

        public PaperBoundUserInterfaceState(string text, List<StampDisplayInfo> stampedBy, List<SignInfo> signs, PaperAction mode = PaperAction.Read) // Green-Signs
        {
            Text = text;
            StampedBy = stampedBy;
            Signs = signs; // Green-Signs
            Mode = mode;
        }
    }

    [Serializable, NetSerializable]
    public sealed class PaperInputTextMessage : BoundUserInterfaceMessage
    {
        public readonly string Text;

        public PaperInputTextMessage(string text)
        {
            Text = text;
        }
    }

    // Green-Signs-Start
    [Serializable, NetSerializable]
    public sealed class SignBoundUserInterfaceState(string name, int maxLength, string? handwriting) : BoundUserInterfaceState
    {
        public readonly string Name = name;
        public readonly int MaxLength = maxLength;
        public readonly string? Handwriting = handwriting;
    }

    [Serializable, NetSerializable]
    public sealed class SignMessage(string name) : BoundUserInterfaceMessage
    {
        public readonly string Name = name;
    }
    // Green-Signs-End

    [Serializable, NetSerializable]
    public enum PaperUiKey
    {
        Key
    }

    // Green-Signs-Start
    [Serializable, NetSerializable]
    public enum SignUiKey
    {
        Key
    }
    // Green-Signs-End

    [Serializable, NetSerializable]
    public enum PaperAction
    {
        Read,
        Write,
    }

    [Serializable, NetSerializable]
    public enum PaperVisuals : byte
    {
        Status,
        Stamp
    }

    [Serializable, NetSerializable]
    public enum PaperStatus : byte
    {
        Blank,
        Written
    }
}
