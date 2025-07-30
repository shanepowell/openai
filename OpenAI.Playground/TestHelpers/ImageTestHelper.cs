using Betalgo.Ranul.OpenAI.Interfaces;
using Betalgo.Ranul.OpenAI.ObjectModels;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using OpenAI.Playground.ExtensionsAndHelpers;
using System;
using System.Diagnostics;
using System.Threading;

namespace OpenAI.Playground.TestHelpers;

internal static class ImageTestHelper
{
    public static async Task RunSimpleCreateImageTest(IOpenAIService sdk)
    {
        ConsoleExtensions.WriteLine("Image Create Testing is starting:", ConsoleColor.Cyan);

        try
        {
            ConsoleExtensions.WriteLine("Image Create Test:", ConsoleColor.DarkCyan);
            var imageResult = await sdk.Image.CreateImage(new()
            {
                Model = "gpt-image-1",
                Prompt = "Laser cat eyes",
                N = 1,
                Size = StaticValues.ImageStatics.Size.Size256,
             //   ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "TestUser"
            });


            if (imageResult.Successful)
            {
                Console.WriteLine(string.Join("\n", imageResult.Results.Select(r => r.Url)));
            }
            else
            {
                if (imageResult.Error == null)
                {
                    throw new("Unknown Error");
                }

                Console.WriteLine($"{imageResult.Error.Code}: {imageResult.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task RunSimpleCreateImageEditTest(IOpenAIService sdk)
    {
        ConsoleExtensions.WriteLine("Image Edit Create Testing is starting:", ConsoleColor.Cyan);
        const string maskFileName = "image_edit_mask.png";
        const string originalFileName = "image_edit_original.png";

        // Images should be in png format with ARGB. I got help from this website to generate sample mask
        // https://www.online-image-editor.com/
        var maskFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{maskFileName}");
        var originalFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{originalFileName}");

        try
        {
            ConsoleExtensions.WriteLine("Image  Edit Create Test:", ConsoleColor.DarkCyan);
            var imageResult = await sdk.Image.CreateImageEdit(new()
            {
                Images = [new ImageEditFile{ Name = originalFileName, Content = originalFile}],
                Mask = new ImageEditFile{ Name = maskFileName, Content = maskFile},
                Prompt = "A sunlit indoor lounge area with a pool containing a cat",
                N = 4,
                Size = StaticValues.ImageStatics.Size.Size1024,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "TestUser"
            });


            if (imageResult.Successful)
            {
                Console.WriteLine(string.Join("\n", imageResult.Results.Select(r => r.Url)));
            }
            else
            {
                if (imageResult.Error == null)
                {
                    throw new("Unknown Error");
                }

                Console.WriteLine($"{imageResult.Error.Code}: {imageResult.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task RunSimpleCreateImageEditTest2(IOpenAIService sdk)
    {
        ConsoleExtensions.WriteLine("Image Edit Create Testing is starting:", ConsoleColor.Cyan);
        const string maskFileName = "image_edit_mask.png";
        const string originalFileName = "image_edit_original.png";

        // Images should be in png format with ARGB. I got help from this website to generate sample mask
        // https://www.online-image-editor.com/
        var maskFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{maskFileName}");
        var originalFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{originalFileName}");

        try
        {
            ConsoleExtensions.WriteLine("Image  Edit Create Test:", ConsoleColor.DarkCyan);
            var imageResult = await sdk.Image.CreateImageEdit(new()
            {
                Model = "gpt-image-1",
                Images = [new ImageEditFile{ Name = originalFileName, Content = originalFile, MimeType = "image/png"}],
                Mask = new ImageEditFile{ Name = maskFileName, Content = maskFile, MimeType = "image/png"},
                Prompt = "A sunlit indoor lounge area with a pool containing a cat",
                N = 4,
                Size = StaticValues.ImageStatics.Size.Size1024,
                User = "TestUser"
            });


            if (imageResult.Successful)
            {
                var index = 0;
                foreach (var image in imageResult.Results)
                {
                    var filePath = Path.Combine(Path.GetTempPath(), $"TempImage{index}.png");
                    index++;

                    ConsoleExtensions.WriteLine($"Image: {filePath}", ConsoleColor.DarkCyan);

                    File.WriteAllBytes(filePath, Convert.FromBase64String(image.B64));

                    var psi = new ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
            }
            else
            {
                if (imageResult.Error == null)
                {
                    throw new("Unknown Error");
                }

                Console.WriteLine($"{imageResult.Error.Code}: {imageResult.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task RunSimpleCreateImageEditTest3(IOpenAIService sdk)
    {
        ConsoleExtensions.WriteLine("Image Edit Create Testing is starting:", ConsoleColor.Cyan);
        const string logoFileName = "logo.png";

        // Images should be in png format with ARGB. I got help from this website to generate sample mask
        // https://www.online-image-editor.com/
        var logoFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{logoFileName}");

        try
        {
            ConsoleExtensions.WriteLine("Image  Edit Create Test:", ConsoleColor.DarkCyan);
            var imageResult = await sdk.Image.CreateImageEdit(new()
            {
                Model = "gpt-image-1",
                Images = [new ImageEditFile{ Name = "logo", Content = logoFile, MimeType = "image/png"}],
                Prompt = "A man with the logo (IPFX) (IP part is white) on it's black t-shirt centred",
                N = 1,
                Size = StaticValues.ImageStatics.Size.Size1024,
                User = "TestUser"
            });


            if (imageResult.Successful)
            {
                var index = 0;
                foreach (var image in imageResult.Results)
                {
                    var filePath = Path.Combine(Path.GetTempPath(), $"TempImage{index}.png");
                    index++;

                    ConsoleExtensions.WriteLine($"Image: {filePath}", ConsoleColor.DarkCyan);

                    File.WriteAllBytes(filePath, Convert.FromBase64String(image.B64));

                    var psi = new ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
            }
            else
            {
                if (imageResult.Error == null)
                {
                    throw new("Unknown Error");
                }

                Console.WriteLine($"{imageResult.Error.Code}: {imageResult.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task RunSimpleCreateImagesEditTest(IOpenAIService sdk)
    {
        ConsoleExtensions.WriteLine("Image Edit Create Testing is starting:", ConsoleColor.Cyan);
        const string maskFileName = "image_edit_mask.png";
        const string originalFileName = "image_edit_original.png";
        const string logoFileName = "logo.png";

        // Images should be in png format with ARGB. I got help from this website to generate sample mask
        // https://www.online-image-editor.com/
        var maskFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{maskFileName}");
        var originalFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{originalFileName}");
        var logoFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{logoFileName}");

        try
        {
            ConsoleExtensions.WriteLine("Image  Edit Create Test:", ConsoleColor.DarkCyan);
            var imageResult = await sdk.Image.CreateImageEdit(new()
            {
                Model = "gpt-image-1",
                Images = [new ImageEditFile{ Name = originalFileName, Content = originalFile}, new ImageEditFile { Name = logoFileName, Content = logoFile }],
                Mask = new ImageEditFile{ Name = maskFileName, Content = maskFile},
                Prompt = "A sunlit indoor lounge area with a pool containing the logo",
                User = "TestUser"
            });


            if (imageResult.Successful)
            {
                var index = 0;
                foreach (var image in imageResult.Results)
                {
                    var filePath = Path.Combine(Path.GetTempPath(), $"TempImage{index}.png");
                    index++;

                    ConsoleExtensions.WriteLine($"Image: {filePath}", ConsoleColor.DarkCyan);

                    File.WriteAllBytes(filePath, Convert.FromBase64String(image.B64));

                    var psi = new ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }

            }
            else
            {
                if (imageResult.Error == null)
                {
                    throw new("Unknown Error");
                }

                Console.WriteLine($"{imageResult.Error.Code}: {imageResult.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task RunSimpleCreateImageVariationTest(IOpenAIService sdk)
    {
        ConsoleExtensions.WriteLine("Image Variation Create Testing is starting:", ConsoleColor.Cyan);
        const string originalFileName = "image_edit_original.png";

        var originalFile = await FileExtensions.ReadAllBytesAsync($"SampleData/{originalFileName}");

        try
        {
            ConsoleExtensions.WriteLine("Image Variation Create Test:", ConsoleColor.DarkCyan);
            var imageResult = await sdk.Image.CreateImageVariation(new()
            {
                Image = new() { Name = originalFileName, Content = originalFile },
                N = 2,
                Size = StaticValues.ImageStatics.Size.Size1024,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "TestUser"
            });


            if (imageResult.Successful)
            {
                Console.WriteLine(string.Join("\n", imageResult.Results.Select(r => r.Url)));
            }
            else
            {
                if (imageResult.Error == null)
                {
                    throw new("Unknown Error");
                }

                Console.WriteLine($"{imageResult.Error.Code}: {imageResult.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}