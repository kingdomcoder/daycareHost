using Daycare.Domain.Entities.Daycare;

namespace Daycare.Domain.Services.Abstract {
    public interface IDeviceTokenService {
        void Upsert(DeviceToken model);
    }
}
