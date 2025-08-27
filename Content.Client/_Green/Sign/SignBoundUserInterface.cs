using JetBrains.Annotations;
using Robust.Client.UserInterface;
using Content.Shared.Paper;

namespace Content.Client._Green.Sign;

[UsedImplicitly]
public sealed class SignBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
{
    [ViewVariables]
    private SignWindow? _window;

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<SignWindow>();
        _window.OnSigned += OnSigned;
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        _window?.Populate((PaperComponent.SignBoundUserInterfaceState)state);
    }

    private void OnSigned(string name)
    {
        SendMessage(new PaperComponent.SignMessage(name));
    }
}
