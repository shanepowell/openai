﻿using Betalgo.Ranul.OpenAI.Extensions;
using Betalgo.Ranul.OpenAI.Interfaces;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Betalgo.Ranul.OpenAI.ObjectModels.ResponseModels.VectorStoreResponseModels;
using Betalgo.Ranul.OpenAI.ObjectModels.SharedModels;

namespace Betalgo.Ranul.OpenAI.Managers;

public partial class OpenAIService : IVectorStores
{
    /// <inheritdoc />
    public async Task<VectorStoreListObjectResponse> ListVectorStores(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetReadAsAsync<VectorStoreListObjectResponse>(_endpointProvider.VectorStoreList(request), _providerType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<VectorStoreObjectResponse> CreateVectorStore(CreateVectorStoreRequest requestBody, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAndReadAsAsync<VectorStoreObjectResponse>(_endpointProvider.VectorStoreCreate(), requestBody, _providerType, cancellationToken);
    }

    public async Task<VectorStoreObjectResponse> RetrieveVectorStore(string vectorStoreId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetReadAsAsync<VectorStoreObjectResponse>(_endpointProvider.VectorStoreRetrieve(vectorStoreId), _providerType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<VectorStoreObjectResponse> ModifyVectorStore(string vectorStoreId, UpdateVectorStoreRequest requestBody, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAndReadAsAsync<VectorStoreObjectResponse>(_endpointProvider.VectorStoreModify(vectorStoreId), requestBody, _providerType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DeletionStatusResponse> DeleteVectorStore(string vectorStoreId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.DeleteAndReadAsAsync<DeletionStatusResponse>(_endpointProvider.VectorStoreDelete(vectorStoreId), _providerType, cancellationToken);
    }
}

public partial class OpenAIService : IVectorStoreFiles
{
    public async Task<VectorStoreFileListObject> ListVectorStoreFiles(string vectorStoreId, VectorStoreFileListRequest vectorStoreFileListRequest, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetReadAsAsync<VectorStoreFileListObject>(_endpointProvider.VectorStoreFileList(vectorStoreId, vectorStoreFileListRequest), _providerType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<VectorStoreFileObject> CreateVectorStoreFile(string vectorStoreId, CreateVectorStoreFileRequest requestBody, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAndReadAsAsync<VectorStoreFileObject>(_endpointProvider.VectorStoreFileCreate(vectorStoreId), requestBody, _providerType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<VectorStoreFileObject> GetVectorStoreFile(string vectorStoreId, string fileId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetReadAsAsync<VectorStoreFileObject>(_endpointProvider.VectorStoreFileRetrieve(vectorStoreId, fileId), _providerType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DeletionStatusResponse> DeleteVectorStoreFile(string vectorStoreId, string fileId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.DeleteAndReadAsAsync<DeletionStatusResponse>(_endpointProvider.VectorStoreFileDelete(vectorStoreId, fileId), _providerType, cancellationToken);
    }

    public async Task<VectorStoreFileBatchObject> CreateVectorStoreFileBatch(string vectorStoreId, CreateVectorStoreFileBatchRequest requestBody, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAndReadAsAsync<VectorStoreFileBatchObject>(_endpointProvider.VectorStoreFileBatchCreate(vectorStoreId), requestBody, _providerType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<VectorStoreFileBatchObject> GetVectorStoreFileBatch(string vectorStoreId, string batchId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetReadAsAsync<VectorStoreFileBatchObject>(_endpointProvider.VectorStoreFileBatchRetrieve(vectorStoreId, batchId), _providerType, cancellationToken);
    }


    /// <inheritdoc />
    public async Task<VectorStoreFileBatchObject> CancelVectorStoreFileBatch(string vectorStoreId, string batchId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAndReadAsAsync<VectorStoreFileBatchObject>(_endpointProvider.VectorStoreFileBatchCancel(vectorStoreId, batchId), null, _providerType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<VectorStoreFileBatchListObjectResponse> ListFilesInVectorStoreBatch(string vectorStoreId, string batchId, PaginationRequest baseListRequest, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetReadAsAsync<VectorStoreFileBatchListObjectResponse>(_endpointProvider.VectorStoreFileBatchList(vectorStoreId, batchId, baseListRequest), _providerType, cancellationToken);
    }
}