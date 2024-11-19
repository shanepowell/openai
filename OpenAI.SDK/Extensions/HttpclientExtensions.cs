﻿    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels;

    namespace Betalgo.Ranul.OpenAI.Extensions;

    internal static class HttpClientExtensions
    {
        public static async Task<TResponse> GetReadAsAsync<TResponse>(this HttpClient client, string uri, ProviderType providerType, CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
        {
            var response = await client.GetAsync(uri, cancellationToken);
            return await HandleResponseContent<TResponse>(response, providerType, cancellationToken);
        }

        public static async Task<TResponse> PostAndReadAsAsync<TResponse>(this HttpClient client, string uri, object? requestModel, ProviderType providerType, CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
        {
            var response = await client.PostAsJsonAsync(uri, requestModel, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            }, cancellationToken);
            return await HandleResponseContent<TResponse>(response, providerType, cancellationToken);
        }

        public static string? GetHeaderValue(this HttpResponseHeaders headers, string headerName)
        {
            return headers.Contains(headerName) ? headers.GetValues(headerName).FirstOrDefault() : null;
        }

        public static async Task<TResponse> PostAndReadAsDataAsync<TResponse, TData>(this HttpClient client, string uri, object? requestModel, ProviderType providerType, CancellationToken cancellationToken = default) where TResponse : DataBaseResponse<TData>, new()
        {
            var response = await client.PostAsJsonAsync(uri, requestModel, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            }, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return await HandleResponseContent<TResponse>(response, providerType, cancellationToken);
            }

            TData data;
            if (typeof(TData) == typeof(byte[]))
            {
                data = (TData)(object)await response.Content.ReadAsByteArrayAsync(cancellationToken);
            }
            else if (typeof(TData) == typeof(Stream))
            {
                data = (TData)(object)await response.Content.ReadAsStreamAsync(cancellationToken);
            }
            else if (typeof(TData) == typeof(string))
            {
                data = (TData)(object)await response.Content.ReadAsStringAsync(cancellationToken);
            }
            else
            {
                throw new NotSupportedException("Unsupported type for TData");
            }

            return new() { Data = data };
        }


        public static HttpResponseMessage PostAsStreamAsync(this HttpClient client, string uri, object requestModel, CancellationToken cancellationToken = default)
        {
            var settings = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            var content = JsonContent.Create(requestModel, null, settings);

            using var request = CreatePostEventStreamRequest(uri, content);

    #if NET6_0_OR_GREATER
            try
            {
                return client.Send(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            }
            catch (PlatformNotSupportedException)
            {
                using var newRequest = CreatePostEventStreamRequest(uri, content);

                return SendRequestPreNet6(client, newRequest, cancellationToken);
            }
    #else
            return SendRequestPreNet6(client, request, cancellationToken);
    #endif
        }

        private static HttpResponseMessage SendRequestPreNet6(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseTask = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            var response = responseTask.GetAwaiter().GetResult();
            return response;
        }

        private static HttpRequestMessage CreatePostEventStreamRequest(string uri, HttpContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Accept.Add(new("text/event-stream"));
            request.Content = content;

            return request;
        }

        public static async Task<TResponse> PostFileAndReadAsAsync<TResponse>(this HttpClient client, string uri, HttpContent content, ProviderType providerType, CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
        {
            var response = await client.PostAsync(uri, content, cancellationToken);
            return await HandleResponseContent<TResponse>(response, providerType, cancellationToken);
        }

        public static async Task<(string? stringResponse, TResponse baseResponse)> PostFileAndReadAsStringAsync<TResponse>(this HttpClient client, string uri, HttpContent content, ProviderType providerType, CancellationToken cancellationToken = default)
            where TResponse : BaseResponse, new()
        {
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var tResponse = new TResponse
                {
                    HttpStatusCode = response.StatusCode,
                    HeaderValues = response.ParseHeaders()
                };
                return (await response.Content.ReadAsStringAsync(cancellationToken), tResponse);
            }
            else
            {
                return (null, await HandleResponseContent<TResponse>(response, providerType, cancellationToken));
            }
        }

        public static async Task<TResponse> DeleteAndReadAsAsync<TResponse>(this HttpClient client, string uri, ProviderType providerType, CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
        {
            var response = await client.DeleteAsync(uri, cancellationToken);
            return await HandleResponseContent<TResponse>(response, providerType, cancellationToken);
        }

        public static async Task<TResponse> HandleResponseContent<TResponse>(this HttpResponseMessage response, ProviderType providerType, CancellationToken cancellationToken) where TResponse : BaseResponse, new()
        {
            TResponse result;
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
            try
            {
                if (!response.Content.Headers.ContentType?.MediaType?.Equals("application/json", StringComparison.OrdinalIgnoreCase) ?? true)
                {
                    result = new()
                    {
                        Error = new()
                        {
                            MessageObject = responseJson
                        }
                    };
                }
                else
                {
                    if (providerType == ProviderType.XAI)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            result = JsonSerializer.Deserialize<TResponse>(responseJson) ?? throw new InvalidOperationException();
                        }
                        else
                        {
                            var error = JsonSerializer.Deserialize<XaiError>(responseJson) ?? throw new InvalidOperationException();
                            result = new()
                            {
                                Error = new()
                                {
                                    CodeObject = error.CodeObject,
                                    MessageObject = error.Message!,
                                }
                            };
                        }
                    }
                    else
                    {
                        result = JsonSerializer.Deserialize<TResponse>(responseJson) ?? throw new InvalidOperationException();
                    }
                }

            }
            catch (Exception e)
            {
                throw new ApplicationException($"Failed to parse response: {responseJson}", e);
            }

            result.HttpStatusCode = response.StatusCode;
            result.HeaderValues = response.ParseHeaders();

            return result;
        }

        public static ResponseHeaderValues ParseHeaders(this HttpResponseMessage response)
        {
            return new()
            {
                Date = response.Headers.Date,
                Connection = response.Headers.Connection?.ToString(),
                AccessControlAllowOrigin = response.Headers.GetHeaderValue("access-control-allow-origin"),
                CacheControl = response.Headers.GetHeaderValue("cache-control"),
                Vary = response.Headers.Vary?.ToString(),
                XRequestId = response.Headers.GetHeaderValue("x-request-id"),
                StrictTransportSecurity = response.Headers.GetHeaderValue("strict-transport-security"),
                CFCacheStatus = response.Headers.GetHeaderValue("cf-cache-status"),
                SetCookie = response.Headers.Contains("set-cookie") ? response.Headers.GetValues("set-cookie").ToList() : null,
                Server = response.Headers.Server?.ToString(),
                CF_RAY = response.Headers.GetHeaderValue("cf-ray"),
                AltSvc = response.Headers.GetHeaderValue("alt-svc"),
                All = response.Headers.ToDictionary(x => x.Key, x => x.Value.AsEnumerable()),

                RateLimits = new()
                {
                    LimitRequests = response.Headers.GetHeaderValue("x-ratelimit-limit-requests"),
                    LimitTokens = response.Headers.GetHeaderValue("x-ratelimit-limit-tokens"),
                    LimitTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-limit-tokens_usage_based"),
                    RemainingRequests = response.Headers.GetHeaderValue("x-ratelimit-remaining-requests"),
                    RemainingTokens = response.Headers.GetHeaderValue("x-ratelimit-remaining-tokens"),
                    RemainingTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-remaining-tokens_usage_based"),
                    ResetRequests = response.Headers.GetHeaderValue("x-ratelimit-reset-requests"),
                    ResetTokens = response.Headers.GetHeaderValue("x-ratelimit-reset-tokens"),
                    ResetTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-reset-tokens_usage_based")
                },

                OpenAI = new()
                {
                    Model = response.Headers.GetHeaderValue("openai-model"),
                    Organization = response.Headers.GetHeaderValue("openai-organization"),
                    ProcessingMs = response.Headers.GetHeaderValue("openai-processing-ms"),
                    Version = response.Headers.GetHeaderValue("openai-version")
                }
            };
        }

    #if NETSTANDARD2_0
        public static async Task<string> ReadAsStringAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            var stream = await content.ReadAsStreamAsync().WithCancellation(cancellationToken);
            using var sr = new StreamReader(stream);
            return await sr.ReadToEndAsync().WithCancellation(cancellationToken);
        }

        public static async Task<AsyncDisposableStream> ReadAsStreamAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            var stream = await content.ReadAsStreamAsync().WithCancellation(cancellationToken);
            return new AsyncDisposableStream(stream);
        }

        public static async Task<byte[]> ReadAsByteArrayAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            return await content.ReadAsByteArrayAsync().WithCancellation(cancellationToken);
        }

        public static async Task<Stream> GetStreamAsync(this HttpClient client, string requestUri, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync(requestUri, cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }

        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }

            return await task;
        }
    #endif
    }