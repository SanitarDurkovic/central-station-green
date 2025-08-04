using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Content.Shared.Speech;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed partial class FrontalLispSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    // @formatter:off
    private static readonly Regex RegexUpperTh = new(@"[T]+[Ss]+|[S]+[Cc]+(?=[IiEeYy]+)|[C]+(?=[IiEeYy]+)|[P][Ss]+|([S]+[Tt]+|[T]+)(?=[Ii]+[Oo]+[Uu]*[Nn]*)|[C]+[Hh]+(?=[Ii]*[Ee]*)|[Z]+|[S]+|[X]+(?=[Ee]+)");
    private static readonly Regex RegexLowerTh = new(@"[t]+[s]+|[s]+[c]+(?=[iey]+)|[c]+(?=[iey]+)|[p][s]+|([s]+[t]+|[t]+)(?=[i]+[o]+[u]*[n]*)|[c]+[h]+(?=[i]*[e]*)|[z]+|[s]+|[x]+(?=[e]+)");
    private static readonly Regex RegexUpperEcks = new(@"[E]+[Xx]+[Cc]*|[X]+");
    private static readonly Regex RegexLowerEcks = new(@"[e]+[x]+[c]*|[x]+");
    // @formatter:on

    // Green-Localization-Start
    [GeneratedRegex("с")]
    private static partial Regex SmallSRegex();

    [GeneratedRegex("С")]
    private static partial Regex BigSRegex();

    [GeneratedRegex("ч")]
    private static partial Regex SmallChRegex();

    [GeneratedRegex("Ч")]
    private static partial Regex BigChRegex();

    [GeneratedRegex("ц")]
    private static partial Regex SmallCRegex();

    [GeneratedRegex("Ц")]
    private static partial Regex BigCRegex();

    [GeneratedRegex(@"\B[т](?![АЕЁИОУЫЭЮЯаеёиоуыэюя])")]
    private static partial Regex SmallTRegex();

    [GeneratedRegex(@"\B[Т](?![АЕЁИОУЫЭЮЯаеёиоуыэюя])")]
    private static partial Regex BigTRegex();

    [GeneratedRegex("з")]
    private static partial Regex SmallZRegex();

    [GeneratedRegex("З")]
    private static partial Regex BigZRegex();
    // Green-Localization-End

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<FrontalLispComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, FrontalLispComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // handles ts, sc(i|e|y), c(i|e|y), ps, st(io(u|n)), ch(i|e), z, s
        message = RegexUpperTh.Replace(message, "TH");
        message = RegexLowerTh.Replace(message, "th");
        // handles ex(c), x
        message = RegexUpperEcks.Replace(message, "EKTH");
        message = RegexLowerEcks.Replace(message, "ekth");

        // Green-Localization-Start
        // с - ш
        message = SmallSRegex().Replace(message, _random.Prob(0.90f) ? "ш" : "с");
        message = BigSRegex().Replace(message, _random.Prob(0.90f) ? "Ш" : "С");
        // ч - ш
        message = SmallChRegex().Replace(message, _random.Prob(0.90f) ? "ш" : "ч");
        message = BigChRegex().Replace(message, _random.Prob(0.90f) ? "Ш" : "Ч");
        // ц - ч
        message = SmallCRegex().Replace(message, _random.Prob(0.90f) ? "ч" : "ц");
        message = BigCRegex().Replace(message, _random.Prob(0.90f) ? "Ч" : "Ц");
        // т - ч
        message = SmallTRegex().Replace(message, _random.Prob(0.90f) ? "ч" : "т");
        message = BigTRegex().Replace(message, _random.Prob(0.90f) ? "Ч" : "Т");
        // з - ж
        message = SmallZRegex().Replace(message, _random.Prob(0.90f) ? "ж" : "з");
        message = BigZRegex().Replace(message, _random.Prob(0.90f) ? "Ж" : "З");
        // Green-Localization-End

        args.Message = message;
    }
}
