namespace Link;

internal class Program
{
    private static readonly IEnumerable<Participant> Participants = ParticipantBuilder.Build().ToList();

    static void Main(string[] args)
    {
        var top5 = Participants
            .Where(x => !x.HasCheated)
            .OrderBy(p => p.TheTime)
            .Take(5);

        var top5Cheeters = Participants
            .Where(x => x.HasCheated)
            .OrderBy(p => p.TheTime)
            .Take(5);

        var combined = Participants
            .GroupBy(p
                    => p.HasCheated,
                (ch, part)
                    => new
                    {
                        ch,
                        Peeps = part
                            .OrderBy(p => p.TheTime)
                            .Take(5)
                    });

        foreach (var x1 in combined)
        {
            Console.WriteLine(x1.ch ? "Cheaters" : "Fair Players");
            PrintEm(x1.Peeps);
        }
        Console.WriteLine("\n-----------------\n\n");
        PrintEm(top5);
        Console.WriteLine("\n-----------------\n\n");
        PrintEm(top5Cheeters);
        Console.WriteLine("\n-----------------\n\n");

        // if condition with assignment (both do the same thing)
        Participant? participant;

        if ((participant = Participants.FirstOrDefault()) is null)
        {
            Console.WriteLine("Couldn't find that");
        }
        else
        {
            Console.WriteLine($"Found {participant}");
        }

        Console.WriteLine((participant = Participants
            .FirstOrDefault(p => p.Name == "xxx")) is null
            ? "Couldn't find that"
            : $"Found {participant}");
    }

    private static void PrintEm(IEnumerable<Participant> peeps)
    {
        foreach (var p in peeps)
        {
            Console.WriteLine(p);
        }
    }
}