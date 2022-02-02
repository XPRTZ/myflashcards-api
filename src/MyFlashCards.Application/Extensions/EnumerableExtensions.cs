namespace MyFlashCards.Application.Extensions;

public static class EnumerableExtensions
{
    private static readonly Random _rng = new((int)DateTime.Now.Ticks);  

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
    {
        var list = collection.ToList();
        var n = list.Count;  
        while (n > 1) {  
            n--;  
            var k = _rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
}
