using AssestManagement.DTO;
using AssestManagement.Models;

namespace AssestManagement.Interfaces
{
    public interface IAssetRepository
    {
       Task<int> AddAsset(Asset asset);
        Task UpdateAsset(Asset asset);
        Task<Asset> GetAssetById(int id);
        Task<IEnumerable<Asset>> GetAssets();
        Task<bool> BulkUpdateAssets(IEnumerable<AssetBulkUpdateRequest> request);

        Task<int> AddAssetImage(AssetImage assetImage);
        Task UpdateAssetImage(AssetImage assetImage);
        Task<int> AddAssetDocument(Document document);
        Task UpdateAssetDocument (Document document);

    }
}
