using System.Text;
using VideoConvertor.Extentions;
using VideoConvertor.Services;

class Program
{
    static async Task Main(string[] args)
    {
        ConsoleKeyInfo cki = default;
        StringBuilder orginalFilePath = new StringBuilder();
        StringBuilder newPath = new StringBuilder();

        do
        {
            orginalFilePath.Clear();
            newPath.Clear();

            Print("enter orginal video path : ");
            orginalFilePath.Append(Console.ReadLine());

            if (!orginalFilePath.HasValue())
            {
                PrintError("enter valid address");
                continue;
            }

            if (!orginalFilePath.IsValidUrl())
                if (!orginalFilePath.IsValidPath())
                {
                    PrintError("enter valid address");
                    continue;
                }

            Print("enter new path :");
            newPath.Append(Console.ReadLine());

            if (!newPath.HasValue())
            {
                PrintError("enter valid address");
                continue;
            }

            if (!orginalFilePath.IsValidPath())
            {
                PrintError("enter valid address");
                continue;
            }

            using VideoService _videoService = new VideoService();
            Task.Run(() => _videoService.ConvertVideoToMp4(orginalFilePath.ToString(), newPath.ToString()));
            Print("The conversion process has started - note that it may take some time to complete");

            cki = Console.ReadKey();

            Console.ResetColor();
            Console.Clear();

        } while (cki.Key != ConsoleKey.Escape);
    }

    static void Print(string txt) => Console.WriteLine(txt);
    static void PrintLine(string txt) => Console.Write(txt);
    static void PrintError(string txt)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(txt);
        Console.ResetColor();
    }
    static void PrintSuccess(string txt)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(txt);
        Console.ResetColor();
    }
}