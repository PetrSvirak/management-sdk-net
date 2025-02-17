﻿using Kontent.Ai.Management.Models.Assets;
using Kontent.Ai.Management.Models.LanguageVariants;
using Kontent.Ai.Management.Models.StronglyTyped;
using Kontent.Ai.Management.Models.Workflow;

namespace Kontent.Ai.Management.Modules.ModelBuilders;

internal class ModelProvider : IModelProvider
{
    private readonly IElementModelProvider _elementModelProvider;

    internal ModelProvider()
    {
        _elementModelProvider = new ElementModelProvider();
    }

    public LanguageVariantModel<T> GetLanguageVariantModel<T>(LanguageVariantModel variant) where T : new() =>
        new()
        {
            Item = variant.Item,
            Language = variant.Language,
            LastModified = variant.LastModified,
            Workflow = variant.Workflow,
            Elements = _elementModelProvider.GetStronglyTypedElements<T>(variant.Elements)
        };

    public LanguageVariantUpsertModel GetLanguageVariantUpsertModel<T>(T variantElements, WorkflowStepIdentifier workflow = null) where T : new() =>
    new()
    {
        Elements = _elementModelProvider.GetDynamicElements(variantElements),
        Workflow = workflow,
    };

    public AssetModel<T> GetAssetModel<T>(AssetModel asset) where T : new() =>
        new()
        {
            Id = asset.Id,
            FileName = asset.FileName,
            Size = asset.Size,
            Type = asset.Type,
            Url = asset.Url,
            FileReference = asset.FileReference,
            Descriptions = asset.Descriptions,
            Title = asset.Title,
            ExternalId = asset.ExternalId,
            LastModified = asset.LastModified,
            ImageHeight = asset.ImageHeight,
            ImageWidth = asset.ImageWidth,
            Folder = asset.Folder,
            Elements = _elementModelProvider.GetStronglyTypedElements<T>(asset.Elements),
        };

    public AssetCreateModel GetAssetCreateModel<T>(AssetCreateModel<T> asset) where T : new() =>
        new()
        {
            FileReference = asset.FileReference,
            Descriptions = asset.Descriptions,
            Title = asset.Title,
            Folder = asset.Folder,
            ExternalId = asset.ExternalId,
            Elements = _elementModelProvider.GetDynamicElements(asset.Elements),
        };

    public AssetUpsertModel GetAssetUpsertModel<T>(AssetUpsertModel<T> asset) where T : new() =>
        new()
        {
            FileReference = asset.FileReference,
            Descriptions = asset.Descriptions,
            Title = asset.Title,
            Folder = asset.Folder,
            Elements = _elementModelProvider.GetDynamicElements(asset.Elements),
        };
}
