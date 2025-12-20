using System.Diagnostics;

public interface IMetadataGenerator
{
    Task GenerateAsync();
}

public class NswagMetadataGenerator : IMetadataGenerator
{
    private readonly string _swaggerUrl;
    private readonly string _outputPath;

    public NswagMetadataGenerator(string swaggerUrl, string outputPath)
    {
        _swaggerUrl = swaggerUrl;
        _outputPath = Path.GetFullPath(outputPath);
    }

    public async Task GenerateAsync()
    {
        var exePath = ResolveNswagExecutable();
        Directory.CreateDirectory(Path.GetDirectoryName(_outputPath)!);

        var psi = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = $"openapi2tsclient /input:{_swaggerUrl} /output:{_outputPath} /template:Fetch /TypeStyle:Interface",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi);
        if (process == null)
            throw new Exception("NSwag process could not be started.");

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            var error = await process.StandardError.ReadToEndAsync();
            throw new Exception($"NSwag failed (ExitCode {process.ExitCode}): {error}");
        }

        Console.WriteLine($"[MetadataGenerator] metadata.ts created at {_outputPath}");
    }

    private static string ResolveNswagExecutable()
    {
        var toolsDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".dotnet",
            "tools");

        var exeName = OperatingSystem.IsWindows() ? "nswag.exe" : "nswag";
        var fullPath = Path.Combine(toolsDir, exeName);

        if (File.Exists(fullPath))
        {
            return fullPath;
        }

        return exeName;
    }
}

public static class MetadataGeneratorExtensions
{
    public static void UseMetadataGenerator(this IHost app)
    {
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        lifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                var generator = app.Services.GetRequiredService<IMetadataGenerator>();
                try
                {
                    await generator.GenerateAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MetadataGenerator] Failed: {ex}");
                }
            });
        });
    }
}
