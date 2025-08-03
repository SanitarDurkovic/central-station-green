using System.Text.RegularExpressions;
using Content.Shared.Speech;
using Robust.Shared.Random;

namespace Content.Server._Green.Speech;

public sealed partial class GrowlingAccentSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<GrowlingAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(Entity<GrowlingAccentComponent> entity, ref AccentGetEvent e)
    {
        var message = e.Message;

        // r => rrr
        message = SmallRRegex().Replace(message, _random.Pick(new List<string> { "rr", "rrr" }));
        // R => RRR
        message = BigRRegex().Replace(message, _random.Pick(new List<string> { "RR", "RRR" }));

        // р => ррр
        message = CyrillicSmallRRegex().Replace(message, _random.Pick(new List<string> { "рр", "ррр" }));
        // Р => РРР
        message = CyrillicBigRRegex().Replace(message, _random.Pick(new List<string> { "РР", "РРР" }));

        e.Message = message;
    }

    [GeneratedRegex("r+")]
    private static partial Regex SmallRRegex();

    [GeneratedRegex("R+")]
    private static partial Regex BigRRegex();

    [GeneratedRegex("р+")]
    private static partial Regex CyrillicSmallRRegex();

    [GeneratedRegex("Р+")]
    private static partial Regex CyrillicBigRRegex();
}
