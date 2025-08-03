using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Content.Shared.Speech;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed partial class MothAccentSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    private static readonly Regex RegexLowerBuzz = new Regex("z{1,3}");
    private static readonly Regex RegexUpperBuzz = new Regex("Z{1,3}");

    // Green-Localization-Start
    [GeneratedRegex("ж+")]
    private static partial Regex SmallZhRegex();
    [GeneratedRegex("Ж+")]
    private static partial Regex BigZhRegex();
    [GeneratedRegex("з+")]
    private static partial Regex SmallZRegex();
    [GeneratedRegex("З+")]
    private static partial Regex BigZRegex();
    // Green-Localization-End

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<MothAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, MothAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // buzzz
        message = RegexLowerBuzz.Replace(message, "zzz");
        // buZZZ
        message = RegexUpperBuzz.Replace(message, "ZZZ");

        // Green-Localization-Start
        // ж => жжж
        message = SmallZhRegex().Replace(message, _random.Pick(new List<string>() { "жж", "жжж" }));
        // Ж => ЖЖЖ
        message = BigZhRegex().Replace(message, _random.Pick(new List<string>() { "ЖЖ", "ЖЖЖ" }));
        // з => ссс
        message = SmallZRegex().Replace(message, _random.Pick(new List<string>() { "зз", "ззз" }));
        // З => CCC
        message = BigZRegex().Replace(message, _random.Pick(new List<string>() { "ЗЗ", "ЗЗЗ" }));
        // Green-Localization-End

        args.Message = message;
    }
}
