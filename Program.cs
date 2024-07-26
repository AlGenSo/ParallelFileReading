using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        string folderPath = @"D:\Books\Кодекс Охотника";
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        int totalSpaces = await CountSpacesInFilesAsync(folderPath);

        stopwatch.Stop();

        Console.WriteLine($"Общее количество пробелов: {totalSpaces}");
        Console.WriteLine($"Затраченное время: {stopwatch.ElapsedMilliseconds} ms");
    }

    static async Task<int> CountSpacesInFilesAsync(string folderPath)
    {
        string[] filePaths = Directory.GetFiles(folderPath);
        var tasks = filePaths.Select(filePath => CountSpacesInFileAsync(filePath)).ToArray();

        int[] results = await Task.WhenAll(tasks);

        return results.Sum();
    }

    static async Task<int> CountSpacesInFileAsync(string filePath)
    {
        return await Task.Run(() =>
        {
            string content = File.ReadAllText(filePath);
            return content.Count(c => c == ' ');
        });
    }
}

