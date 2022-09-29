using Bogus;

namespace Link;

public class Participant
{
    public Participant()
    {
        Name = string.Empty;
        PhoneNumber = string.Empty;
        StartNumber = string.Empty;
    }

    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool HasCheated { get; set; }
    public string PhoneNumber { get; set; }
    public string StartNumber { get; set; }

    public DateTime TheTime => new((EndTime - StartTime).Ticks);

    public override string ToString()
        => $"{StartNumber.PadRight(10)}{Name.PadRight(30)}{TheTime:T}\t{StartTime:T}\t{EndTime:T}\t{DidCheat}\t Phone: {PhoneNumber}";

    private string DidCheat => HasCheated ? "Cheated" : "Fair";
}

public static class ParticipantBuilder
{
    public static IEnumerable<Participant> Build()
    {
        var participants = new Faker<Participant>();

        participants
            .RuleFor(p => p.Name, f => f.Name.FullName())
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.StartTime,
                f => f.Date.Between(new DateTime(2022, 09, 01, 10, 0, 0), new DateTime(2022, 09, 01, 11, 0, 0)))
            .RuleFor(p => p.EndTime,
                f => f.Date.Between(new DateTime(2022, 09, 01, 13, 0, 0), new DateTime(2022, 09, 01, 14, 0, 0)))
            .RuleFor(p => p.HasCheated, f => f.PickRandom(true, false))
            .RuleFor(p => p.StartNumber, f => f.Random.Number(99999).ToString());

        return participants.GenerateBetween(50, 100);
    }
}