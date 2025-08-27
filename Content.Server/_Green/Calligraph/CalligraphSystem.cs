using System.Linq;
using Content.Server.StationRecords.Systems;
using Content.Shared._Green.Calligraph;
using Content.Shared.Paper;
using Content.Shared.Station;
using Content.Shared.StationRecords;
using Content.Shared.Verbs;
using Robust.Shared.Audio.Systems;

namespace Content.Server._Green.Calligraph;

public sealed class CalligraphSystem : EntitySystem
{
    [Dependency] private readonly SharedUserInterfaceSystem _ui = default!;
    [Dependency] private readonly SharedStationSystem _station = default!;
    [Dependency] private readonly StationRecordsSystem _records = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<CalligraphComponent, GetVerbsEvent<UtilityVerb>>(OnGetVerbs);
    }

    private void OnGetVerbs(Entity<CalligraphComponent> entity, ref GetVerbsEvent<UtilityVerb> e)
    {
        if (!e.CanInteract || !e.CanComplexInteract || !e.CanAccess)
            return;

        if (!TryComp<PaperComponent>(e.Target, out var paper))
            return;

        var user = e.User;

        e.Verbs.Add(new()
        {
            Text = Loc.GetString("calligraph-verb-scan-text"),
            Act = () =>
            {
                List<SignRecord> records = [];

                foreach (var sign in paper.Signs)
                {
                    string? author = null;

                    if (sign.Handwriting is not null)
                    {
                        if (entity.Comp.AllStations)
                        {
                            foreach (var station in _station.GetStations())
                            {
                                author = GetAuthorOrNull(station, sign.Handwriting);

                                if (author is not null)
                                    break;
                            }
                        }
                        else
                        {
                            var station = _station.GetOwningStation(entity);

                            if (station is not null)
                                author = GetAuthorOrNull(station.Value, sign.Handwriting);
                        }
                    }

                    records.Add(new()
                    {
                        Name = sign.Name,
                        Handwriting = sign.Handwriting,
                        Author = author
                    });
                }

                _audio.PlayPvs(entity.Comp.ScanSound, entity);

                _ui.OpenUi(entity.Owner, CalligraphUiKey.Key, user);
                _ui.SetUiState(entity.Owner, CalligraphUiKey.Key, new CalligraphBoundUserInterfaceState(records));
            }
        });

        string? GetAuthorOrNull(EntityUid station, string handwriting)
        {
            return _records.GetRecordsOfType<GeneralStationRecord>(station).FirstOrDefault(record => record.Item2.Handwriting == handwriting).Item2?.Name;
        }
    }
}
