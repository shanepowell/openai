using System.Globalization;
using System.Net.Http.Headers;
using Betalgo.Ranul.OpenAI.Extensions;
using Betalgo.Ranul.OpenAI.Interfaces;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels.ImageResponseModel;

namespace Betalgo.Ranul.OpenAI.Managers;

public partial class OpenAIService : IImageService
{
    /// <summary>
    ///     Creates an image given a prompt.
    /// </summary>
    /// <param name="imageCreateModel"></param>
    /// <returns></returns>
    public async Task<ImageCreateResponse> CreateImage(ImageCreateRequest imageCreateModel, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAndReadAsAsync<ImageCreateResponse>(_endpointProvider.ImageCreate(), imageCreateModel, _providerType, cancellationToken);
    }

    /// <summary>
    ///     Creates an edited or extended image given an original image and a prompt.
    /// </summary>
    /// <param name="imageEditCreateRequest"></param>
    /// <returns></returns>
    public async Task<ImageCreateResponse> CreateImageEdit(ImageEditCreateRequest imageEditCreateRequest, CancellationToken cancellationToken = default)
    {
        var multipartContent = new MultipartFormDataContent();
        if (imageEditCreateRequest.User != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.User), "user");
        }

        if (imageEditCreateRequest.ResponseFormat != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.ResponseFormat), "response_format");
        }

        if (imageEditCreateRequest.Size != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.Size), "size");
        }

        if (imageEditCreateRequest.N != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.N.Value.ToString(CultureInfo.InvariantCulture)), "n");
        }

        if (imageEditCreateRequest.Background != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.Background), "background");
        }

        if (imageEditCreateRequest.InputFidelity != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.InputFidelity), "input_fidelity");
        }

        if (imageEditCreateRequest.OutputCompression != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.OutputCompression.ToString()!), "output_compression");
        }

        if (imageEditCreateRequest.OutputFormat != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.OutputFormat), "output_format");
        }

        if (imageEditCreateRequest.PartialImages != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.PartialImages.ToString()!), "partial_images");
        }

        if (imageEditCreateRequest.Quality != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.Quality), "quality");
        }

        if (imageEditCreateRequest.Stream != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.Stream.ToString()!), "stream");
        }

        if (imageEditCreateRequest.Model != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.Model!), "model");
        }

        if (imageEditCreateRequest.Mask != null)
        {
            multipartContent.Add(ToImageFileContent(imageEditCreateRequest.Mask), "mask", imageEditCreateRequest.Mask.FileName);
        }

        multipartContent.Add(new StringContent(imageEditCreateRequest.Prompt), "prompt");

        if (imageEditCreateRequest.Images.Count == 1)
        {
            multipartContent.Add(ToImageFileContent(imageEditCreateRequest.Images[0]), "image", imageEditCreateRequest.Images[0].FileName);
        }
        else
        {
            foreach (var image in imageEditCreateRequest.Images)
            {
                multipartContent.Add(ToImageFileContent(image), "image[]", image.FileName);
            }
        }

        return await _httpClient.PostFileAndReadAsAsync<ImageCreateResponse>(_endpointProvider.ImageEditCreate(), multipartContent, _providerType, cancellationToken);
    }

    private static ByteArrayContent ToImageFileContent(ImageEditFile image)
    {
        var content = new ByteArrayContent(image.Content);
        if (!string.IsNullOrEmpty(image.MimeType))
        {
            content.Headers.ContentType = new MediaTypeHeaderValue(image.MimeType);
        }

        return content;
    }

    /// <summary>
    ///     Creates a variation of a given image.
    /// </summary>
    /// <param name="imageEditCreateRequest"></param>
    /// <returns></returns>
    public async Task<ImageCreateResponse> CreateImageVariation(ImageVariationCreateRequest imageEditCreateRequest, CancellationToken cancellationToken = default)
    {
        var multipartContent = new MultipartFormDataContent();
        if (imageEditCreateRequest.User != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.User), "user");
        }

        if (imageEditCreateRequest.ResponseFormat != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.ResponseFormat), "response_format");
        }

        if (imageEditCreateRequest.Size != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.Size), "size");
        }

        if (imageEditCreateRequest.N != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.N.Value.ToString(CultureInfo.InvariantCulture)), "n");
        }

        if (imageEditCreateRequest.Model != null)
        {
            multipartContent.Add(new StringContent(imageEditCreateRequest.Model!), "model");
        }

        multipartContent.Add(ToImageFileContent(imageEditCreateRequest.Image), "image", imageEditCreateRequest.Image.FileName);

        return await _httpClient.PostFileAndReadAsAsync<ImageCreateResponse>(_endpointProvider.ImageVariationCreate(), multipartContent, _providerType, cancellationToken);
    }
}