using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Daycare.Domain.Repositories.Concrete {
    public class EFDeviceTokenRepository : IDeviceTokenRepository {
        private readonly MyDbContext context;

        public EFDeviceTokenRepository(MyDbContext context) {
            this.context = context;
        }

        public void Upsert(DeviceToken model) {
            var existing = context.DeviceToken
                .FirstOrDefault(d => d.UserId == model.UserId && d.Platform == model.Platform);
            if (existing == null) {
                model.UpdatedAt = DateTime.UtcNow;
                context.DeviceToken.Add(model);
            } else {
                existing.Token = model.Token;
                existing.UpdatedAt = DateTime.UtcNow;
                context.Entry(existing).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
