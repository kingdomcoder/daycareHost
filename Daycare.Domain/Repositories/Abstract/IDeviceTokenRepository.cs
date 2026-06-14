using Daycare.Domain.Entities.Daycare;

namespace Daycare.Domain.Repositories.Abstract {
    public interface IDeviceTokenRepository {
        void Upsert(DeviceToken model);
    }
}
