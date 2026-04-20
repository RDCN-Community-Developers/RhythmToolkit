using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace RhythmBase.Adofai.Components;

partial class ADLevel
{
    internal static class Deserializer
    {
        public static ADLevel Deserialize(IJsonDataSource dataSource, JsonSerializerOptions options)
        {
            if (dataSource.CanGetMemoryDirectly)
            {
                ReadOnlyMemory<byte> jsonData = dataSource.GetMemory();
                Utf8JsonReader reader = new(jsonData.Span, new() { AllowTrailingCommas = true });
                return ConverterHub.Read<ADLevel>(ref reader, options) ?? [];
            }
            else
            {
                ReadOnlyMemory<byte> jsonData = dataSource.GetMemoryAsync().GetAwaiter().GetResult();
                Utf8JsonReader reader = new(jsonData.Span, new() { AllowTrailingCommas = true });
                return ConverterHub.Read<ADLevel>(ref reader, options) ?? [];
            }
        }
        public static async Task<ADLevel> DeserializeAsync(IJsonDataSource dataSource, JsonSerializerOptions options, CancellationToken token = default)
        {
            if (dataSource.CanGetMemoryDirectly)
            {
                ReadOnlyMemory<byte> jsonData = dataSource.GetMemory();
                Utf8JsonReader reader = new(jsonData.Span, new() { AllowTrailingCommas = true });
                return ConverterHub.Read<ADLevel>(ref reader, options) ?? [];
            }
            else
            {
                ReadOnlyMemory<byte> jsonData = await dataSource.GetMemoryAsync(token);
                Utf8JsonReader reader = new(jsonData.Span, new() { AllowTrailingCommas = true });
                return ConverterHub.Read<ADLevel>(ref reader, options) ?? [];
            }
        }
    }
    /// <inheritdoc/>
    public static ADLevel FromFile(string filepath, LevelReadSettings? settings = null)
    {
        settings ??= new();
        string extension = Path.GetExtension(filepath);
        ADLevel? level;
        if (extension != ".zip")
        {
            if (extension != ".adofai")
                throw new RhythmBaseException("File not supported.");
            using FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
            level = FromStream(stream, settings);
            level.Filepath = level.ResolvedPath = Path.GetFullPath(filepath);
            return level;
        }
        switch (settings.ZipFileProcessMethod)
        {
            case ZipFileProcessMethod.AllFiles:
                DirectoryInfo tempDirectory = new(Path.Combine(GlobalSettings.CachePath, "RhythmBaseTemp_" + Path.GetRandomFileName()));
                tempDirectory.Create();
                try
                {
#if NET8_0_OR_GREATER
                    using Stream stream = File.OpenRead(filepath);
                    ZipFile.ExtractToDirectory(stream, tempDirectory.FullName, overwriteFiles: true);
#elif NETSTANDARD2_0_OR_GREATER
                    ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
#endif
                    string? adlevelPath = null;
                    foreach (FileInfo file in tempDirectory.GetFiles())
                    {
                        if (file.Extension == ".adofai")
                        {
                            adlevelPath = file.FullName;
                            break;
                        }
                    }
                    if (adlevelPath == null)
                        throw new RhythmBaseException("No Adofai file has been found.");
                    level = FromFile(adlevelPath, settings);
                    level.ResolvedPath =Path.GetFullPath(adlevelPath);
                    level.Filepath =  Path.GetFullPath(filepath);
                    level.isZip = true;
                    level.isExtracted = true;
                }
                catch (Exception ex2)
                {
                    tempDirectory.Delete(true);
                    throw new RhythmBaseException("Cannot extract the file.", ex2);
                }
                break;
            case ZipFileProcessMethod.LevelFileOnly:
                try
                {
                    using FileStream zipStream = new(filepath, FileMode.Open, FileAccess.Read);
                    using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);
                    ZipArchiveEntry? entry = archive.GetEntry("main.rdlevel") ?? throw new RhythmBaseException("Cannot find the level file.");
                    using Stream stream = entry.Open();
                    level = FromStream(stream, settings);
                    level.Filepath = Path.GetFullPath(filepath);
                    level.isZip = true;
                    level.isExtracted = false;
                }
                catch (Exception ex2)
                {
                    throw new RhythmBaseException("Cannot extract the file.", ex2);
                }
                break;
            default:
                throw new RhythmBaseException(extension + " is not supported.");
        }
        return level;
    }
    /// <inheritdoc/>
    public static async Task<ADLevel> FromFileAsync(string filepath, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
    {
        settings ??= new();
        string extension = Path.GetExtension(filepath);
        ADLevel? level;
        if (extension != ".zip")
        {
            if (extension != ".adofai")
                throw new RhythmBaseException("File not supported.");
            using FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
            level = FromStream(stream, settings);
            level.Filepath = level.ResolvedPath = Path.GetFullPath(filepath);
            return level;
        }
        switch (settings.ZipFileProcessMethod)
        {
            case ZipFileProcessMethod.AllFiles:
                DirectoryInfo tempDirectory = new(Path.Combine(GlobalSettings.CachePath, "RhythmBaseTemp_" + Path.GetRandomFileName()));
                tempDirectory.Create();
                try
                {
#if NET8_0_OR_GREATER
                    using Stream stream = File.OpenRead(filepath);
                    ZipFile.ExtractToDirectory(stream, tempDirectory.FullName, overwriteFiles: true);
#elif NETSTANDARD2_0_OR_GREATER
                    ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
#endif
                    string? adlevelPath = null;
                    foreach (FileInfo file in tempDirectory.GetFiles())
                    {
                        if (file.Extension == ".adofai")
                        {
                            adlevelPath = file.FullName;
                            break;
                        }
                    }
                    if (adlevelPath == null)
                        throw new RhythmBaseException("No Adofai file has been found.");
                    level = FromFile(adlevelPath, settings);
                    level.ResolvedPath = Path.GetFullPath(adlevelPath);
                    level.Filepath = Path.GetFullPath(filepath);
                    level.isZip = true;
                    level.isExtracted = true;
                }
                catch (Exception ex2)
                {
                    tempDirectory.Delete(true);
                    throw new RhythmBaseException("Cannot extract the file.", ex2);
                }
                break;
            case ZipFileProcessMethod.LevelFileOnly:
                try
                {
                    using FileStream zipStream = new(filepath, FileMode.Open, FileAccess.Read);
                    using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);
                    ZipArchiveEntry? entry = archive.GetEntry("main.rdlevel") ?? throw new RhythmBaseException("Cannot find the level file.");
                    using Stream stream = entry.Open();
                    level = await FromStreamAsync(stream, settings, cancellationToken);
                    level.Filepath = Path.GetFullPath(filepath);
                    level.isZip = true;
                    level.isExtracted = false;
                }
                catch (Exception ex2)
                {
                    throw new RhythmBaseException("Cannot extract the file.", ex2);
                }
                break;
            default:
                throw new RhythmBaseException(extension + " is not supported.");
        }
        return level;
    }
    /// <inheritdoc/>
    public static ADLevel FromStream(Stream adlevelStream, LevelReadSettings? settings = null)
    {
        settings ??= new();
        JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings: settings);
        ADLevel? level;
        settings.OnBeforeReading();
        level = Deserializer.Deserialize(new StreamDataSource(adlevelStream), options);
        settings.OnAfterReading();
        return level ?? [];
    }
    /// <inheritdoc/>
    public static async Task<ADLevel> FromStreamAsync(Stream stream, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
    {
        settings ??= new();
        JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings: settings);
        ADLevel? level;
        settings.OnBeforeReading();
        level = await Deserializer.DeserializeAsync(new StreamDataSource(stream), options, cancellationToken);
        settings.OnAfterReading();
        return level ?? [];
    }
    /// <inheritdoc/>
    public static ADLevel FromJsonString(string json, LevelReadSettings? settings = null)
    {
        settings ??= new();
        JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings: settings);
        ADLevel? level;
        settings.OnBeforeReading();
        level = JsonSerializer.Deserialize<ADLevel>(json, options);
        settings.OnBeforeReading();
        return level ?? [];
    }
    /// <inheritdoc/>
    public void SaveToStream(Stream stream, LevelWriteSettings? settings = null)
    {
        settings ??= new();
        JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings: settings);
        settings.OnBeforeWriting();
        JsonSerializer.Serialize(stream, this, options);
        settings.OnAfterWriting();
    }
    /// <inheritdoc/>
    public async void SaveToStreamAsync(Stream stream, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
    {
        settings ??= new();
        JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings: settings);
        settings.OnBeforeWriting();
        await JsonSerializer.SerializeAsync(stream, this, options, cancellationToken);
        settings.OnAfterWriting();
    }
    /// <inheritdoc/>
    public void SaveToFile(string filepath, LevelWriteSettings? settings = null)
    {
        settings ??= new();
        JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(filepath, settings);
        settings.OnBeforeWriting();
        using (FileStream stream = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.Write))
        {
            stream.SetLength(0);
            JsonSerializer.Serialize(stream, this, options);
        }
        settings.OnAfterWriting();
    }
    /// <inheritdoc/>
    public async void SaveToFileAsync(string filepath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
    {
        settings ??= new();
        JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(filepath, settings);
        settings.OnBeforeWriting();
        using (FileStream stream = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.Write))
        {
            await JsonSerializer.SerializeAsync(stream, this, options, cancellationToken);
        }
        settings.OnAfterWriting();
    }
    /// <inheritdoc/>
    public string ToJsonString(LevelWriteSettings? settings = null)
    {
        settings ??= new();
        JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings: settings);
        string json;
        settings.OnBeforeWriting();
        json = JsonSerializer.Serialize(this, options);
        return json;
    }
    /// <inheritdoc/>
    public static ADLevel FromJsonDocument(JsonDocument jsonDocument, LevelReadSettings? settings = null)
    {
        throw new NotImplementedException();
    }
    /// <inheritdoc/>
    public JsonDocument ToJsonDocument(LevelWriteSettings? settings = null)
    {
        throw new NotImplementedException();
    }
    /// <inheritdoc/>
    public void SaveToZip(string filepath, LevelWriteSettings? settings = null)
    {
        throw new NotImplementedException();
    }
    /// <inheritdoc/>
    public void SaveToZipAsync(string filepath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}