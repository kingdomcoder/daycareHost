using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;

namespace Daycare.Domain.Services.Concrete {
    public class DeviceTokenService : IDeviceTokenService {
        private readonly IDeviceTokenRepository deviceTokenRepository;

        public DeviceTokenService(IDeviceTokenRepository deviceTokenRepository) {
            this.deviceTokenRepository = deviceTokenRepository;
        }

        public void Upsert(DeviceToken model) {
            deviceTokenRepository.Upsert(model);
        }
    }
}
