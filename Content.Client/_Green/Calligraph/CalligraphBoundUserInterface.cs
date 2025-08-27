using JetBrains.Annotations;
using Robust.Client.UserInterface;
using Content.Shared._Green.Calligraph;

namespace Content.Client._Green.Calligraph;

[UsedImplicitly]
public sealed class CalligraphBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
{
    [ViewVariables]
    private CalligraphWindow? _window;

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<CalligraphWindow>();
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        _window?.Populate((CalligraphBoundUserInterfaceState)state);
    }
}
