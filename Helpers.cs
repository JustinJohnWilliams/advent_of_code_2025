global using System.Text;
global using System.Text.RegularExpressions;
global using System.Diagnostics;
global using ConsoleTables;
global using static Extensions;

public class Input(string input, string example)
{
    public string Empty => string.Empty;
    public string Input_SingleLineText => Extensions.SafeExecute(() =>
        File.ReadAllText(input), string.Empty);
    public string[] Input_MultiLineTextArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(input), []);
    public char[][] Input_TwoDCharArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(input).Select(c => c.ToArray()).ToArray(), []);
    public int[][] Input_TwoDIntArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(input).Select(c => c.Select(d => d.ToString().ToInt32()).ToArray()).ToArray(), []);
    public string[] Input_SingleLineTextSplitEntriesBy(char c) => Extensions.SafeExecute(() =>
        File.ReadAllText(input).Split(c), []);
    public string Input_RawFile => input;
    public string Example_SingleLineText => Extensions.SafeExecute(() =>
        File.ReadAllText(example), string.Empty);
    public string[] Example_MultiLineTextArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(example), []);
    public char[][] Example_TwoDCharArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(example).Select(c => c.ToArray()).ToArray(), []);
    public int[][] Example_TwoDIntArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(example).Select(c => c.Select(d => d.ToString().ToInt32()).ToArray()).ToArray(), []);
    public string[] Example_SingleLineTextSplitEntriesBy(char c) => Extensions.SafeExecute(() =>
        File.ReadAllText(example).Split(c), []);
    public IEnumerable<string[]> Example_MultiLineTextSplitEntriesBy(char c) => Extensions.SafeExecute(() =>
        File.ReadAllLines(example).Select(s => s.SplitAndRemoveEmpty(c)), []);
    public string Example_RawFile => example;
}

public static class Extensions
{
    extension<T>(T t)
    {
        public T Print()
        {
            Console.WriteLine(t);
            return t;
        }
    }

    extension(string str)
    {
        public bool IsStringUniqueCharacters() => str.GroupBy(c => c).All(c => c.Count() <= 1);
        public bool IsStringNullOrEmpty() => string.IsNullOrEmpty(str);
        public bool IsStringNotNullOrEmpty() => !string.IsNullOrEmpty(str);
        public int ToInt32() => Convert.ToInt32(str);
        public long ToInt64() => Convert.ToInt64(str);
        public string[] SplitAndRemoveEmpty(char c) => str.Split(c, StringSplitOptions.RemoveEmptyEntries);
    }

    extension<T>(IEnumerable<T> source)
    {
        public bool IsSubsetOf(IEnumerable<T> target) => source.All(c => target.Contains(c));
        public bool ContainsAny(IEnumerable<T> target) => source.Any(c => target.Contains(c));
        public IEnumerable<T2> Map<T2>(Func<T, T2> func)
        {
            var asList = source.ToList();
            for (int i = 0; i < asList.Count; i++) yield return func(asList[i]);
        }
        public IEnumerable<T2> MapWithIndex<T2>(Func<int, T, T2> func)
        {
            var asList = source.ToList();
            for (int i = 0; i < asList.Count; i++) yield return func(i, asList[i]);
        }
        public T2 Fold<T2>(T2 initialValue, Func<T2, T2> func)
        {
            var result = initialValue;
            foreach (var _ in source)
                result = func(result);
            return result;
        }
        public T2 Fold<T2>(T2 initialValue, Func<T, T2, T2> func)
        {
            var result = initialValue;
            foreach (var e in source)
                result = func(e, result);
            return result;
        }
        public string Join(string separator) => string.Join(separator, source);
        public IEnumerable<T> Prints()
        {
            Console.WriteLine(source.Join(", "));
            return source;
        }
    }

    extension(int n)
    {
        public IEnumerable<int> Map() => Enumerable.Range(0, n);
        public IEnumerable<T> Map<T>(Func<int, T> func) => n.Map().Map(func);
        public void Times(Action<int> action)
        {
            for(int i = 0; i < n; i++) action(i);
        }
    }

    extension<T>(T[] array)
    {
        public T SafeIndex(int index)
        {
            if(index < 0 || index >= array.Length) return default!;
            return array[index];
        }
    }

    public static T SafeExecute<T>(Func<T> fileOperation, T defaultValue = default!)
    {
        try
        {
            return fileOperation();
        }
        catch
        {
            Console.WriteLine($"Error: Processing {fileOperation.Method.Name}");
            return defaultValue;
        }
    }

    extension<T, TResult>(T)
    {
        public static TResult operator | (T source, Func<T, TResult> func) => func(source);
    }

    extension<TIn, TMid, TOut>(Func<TIn, TMid>)
    {
        public static Func<TIn, TOut> operator >>(Func<TIn, TMid> f, Func<TMid, TOut> g) => x => g(f(x));
    }

    public static Func<IEnumerable<TSource>, IEnumerable<TResult>> Map<TSource, TResult>(
        Func<TSource, TResult> selector
    ) => source => source.Select(selector);

    public static Func<string, string[]> SplitAndRemove(char separator) => s => s.SplitAndRemoveEmpty(separator);
    public static Func<IEnumerable<TSource>, Dictionary<TKey, TValue>> ToDictionary<TSource, TKey, TValue>(
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector
    ) where TKey : notnull => source => source.ToDictionary(keySelector, valueSelector);

}

public static class EnumerableEx
{
    public static IEnumerable<long> Range(long start, long count)
    {
        var end = start + count - 1; // mimic Enumerable.Range
        for(long i = start; i <= end; i++) yield return i;
    }
}
