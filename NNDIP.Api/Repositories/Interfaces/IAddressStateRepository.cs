using NNDIP.Api.Dtos.AddressState;
using NNDIP.Api.Entities;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface IAddressStateRepository : IGenericRepository<AddressState>
    {
        AddressStateResultDto GetAddressStateResult();
        Task<AddressStateResultDto> GetAddressStateResultAsync();
    }
}
