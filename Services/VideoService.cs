using System.Diagnostics;
using System.Text;

namespace VideoConvertor.Services;

public class VideoService : IDisposable
{
    private bool disposedValue;

    public async Task ConvertHevcToMp4(string inputPath, string outputPath)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = $"-i \"{inputPath}\" -c:v libx264 -crf 23 -preset fast -c:a aac -b:a 128k \"{outputPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();

            string standardOutput = process.StandardOutput.ReadToEnd();
            string standardError = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"FFmpeg exited with code {process.ExitCode}\nError Output: {standardError}");
            }
        }

        await Task.CompletedTask;
    }
    public async Task ConvertHevcToMp4(StringBuilder inputPath, StringBuilder outputPath)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = $"-i \"{inputPath}\" -c:v libx264 -crf 23 -preset fast -c:a aac -b:a 128k \"{outputPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();

            string standardOutput = process.StandardOutput.ReadToEnd();
            string standardError = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"FFmpeg exited with code {process.ExitCode}\nError Output: {standardError}");
            }
        }

        await Task.CompletedTask;
    }
    public async Task ConvertVideoToMp4(string inputPath, string outputPath)
    {
        try
        {
            var ffmpegArgs = $"-i \"{inputPath}\" \"{outputPath}\"";
            var startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = ffmpegArgs,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var process = Process.Start(startInfo))
            {
                using (var reader = process.StandardError)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }

                process.WaitForExit();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("succcessfully done !");
        }
        catch (Exception exp)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exp.Message);
            
        }

        Console.ResetColor();
        await Task.CompletedTask;
    }
    public async Task ConvertMp4ToHevc(string inputPath, string outputPath)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg", 
            Arguments = $"-i \"{inputPath}\" -c:v libx265 \"{outputPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();

            process.WaitForExit();
        }

        await Task.CompletedTask;
    }
    public async Task RemuxMkvToMp4(string inputPath, string outputPath)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg", 
            Arguments = $"-i \"{inputPath}\" -c copy \"{outputPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"FFmpeg exited with code {process.ExitCode}");
            }
        }

        await Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
            disposedValue = true;
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
