using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.DigitalAssets
{
    public class DigitalAssetApiModel
    {        
        public Guid DigitalAssetId { get; set; }
        public string Name { get; set; }
        public string RelativePath { get { return $"api/digitalassets/serve/{DigitalAssetId}"; } }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public static DigitalAssetApiModel FromDigitalAsset(DigitalAsset digitalAsset)
            => new DigitalAssetApiModel
            {
                DigitalAssetId = digitalAsset.DigitalAssetId,
                Name = digitalAsset.Name,
                Bytes = digitalAsset.Bytes,
                ContentType = digitalAsset.ContentType
            };
    }
}
