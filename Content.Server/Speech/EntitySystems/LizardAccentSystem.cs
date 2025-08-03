using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Content.Shared.Speech;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed partial class LizardAccentSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    private static readonly Regex RegexLowerS = new("s+");
    private static readonly Regex RegexUpperS = new("S+");
    private static readonly Regex RegexInternalX = new(@"(\w)x");
    private static readonly Regex RegexLowerEndX = new(@"\bx([\-|r|R]|\b)");
    private static readonly Regex RegexUpperEndX = new(@"\bX([\-|r|R]|\b)");

    // Green-Localization-Start
    [GeneratedRegex("с+")]
    private static partial Regex SmallSRegex();
    [GeneratedRegex("С+")]
    private static partial Regex BigSRegex();
    [GeneratedRegex("з+")]
    private static partial Regex SmallZRegex();
    [GeneratedRegex("З+")]
    private static partial Regex BigZRegex();
    [GeneratedRegex("ш+")]
    private static partial Regex SmallShRegex();
    [GeneratedRegex("Ш+")]
    private static partial Regex BigShRegex();
    [GeneratedRegex("ч+")]
    private static partial Regex SmallChRegex();
    [GeneratedRegex("Ч+")]
    private static partial Regex BigChRegex();
    // Green-Localization-End

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<LizardAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, LizardAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // hissss
        message = RegexLowerS.Replace(message, "sss");
        // hiSSS
        message = RegexUpperS.Replace(message, "SSS");
        // ekssit
        message = RegexInternalX.Replace(message, "$1kss");
        // ecks
        message = RegexLowerEndX.Replace(message, "ecks$1");
        // eckS
        message = RegexUpperEndX.Replace(message, "ECKS$1");

        // Green-Localization-Start
        // c => ссс
        message = SmallSRegex().Replace(message, _random.Pick(new List<string>() { "сс", "ссс" }));
        // С => CCC
        message = BigSRegex().Replace(message, _random.Pick(new List<string>() { "СС", "ССС" }));
        // з => ссс
        message = SmallZRegex().Replace(message, _random.Pick(new List<string>() { "сс", "ссс" }));
        // З => CCC
        message = BigZRegex().Replace(message, _random.Pick(new List<string>() { "СС", "ССС" }));
        // ш => шшш
        message = SmallShRegex().Replace(message, _random.Pick(new List<string>() { "шш", "шшш" }));
        // Ш => ШШШ
        message = BigShRegex().Replace(message, _random.Pick(new List<string>() { "ШШ", "ШШШ" }));
        // ч => щщщ
        message = SmallChRegex().Replace(message, _random.Pick(new List<string>() { "щщ", "щщщ" }));
        // Ч => ЩЩЩ
        message = BigChRegex().Replace(message, _random.Pick(new List<string>() { "ЩЩ", "ЩЩЩ" }));
        // Green-Localization-End

        args.Message = message;
    }
}
