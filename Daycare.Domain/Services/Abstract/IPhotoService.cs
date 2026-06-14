using Daycare.Domain.Entities.Daycare;
using System.Collections.Generic;

namespace Daycare.Domain.Services.Abstract {
    public interface IPhotoService {
        Photo SavePhoto(Photo model);

        IEnumerable<Photo> GetPhotosByChild(int childId);

        Photo GetPhotoById(int photoId);

        bool IsParentOfChild(string userId, int childId);

        bool IsChildInOrganization(int childId, int organizationId);

        // Logical (soft) delete: flips ActiveStatus to false. Used for personal-data removal.
        void SoftDeletePhoto(int photoId);
    }
}
