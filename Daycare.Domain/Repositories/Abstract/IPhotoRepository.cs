using Daycare.Domain.Entities.Daycare;
using System.Collections.Generic;

namespace Daycare.Domain.Repositories.Abstract {
    public interface IPhotoRepository {
        Photo SavePhoto(Photo model);

        IEnumerable<Photo> GetPhotosByChild(int childId);

        Photo GetPhotoById(int photoId);

        bool IsParentOfChild(string userId, int childId);

        bool IsChildInOrganization(int childId, int organizationId);

        void SoftDeletePhoto(int photoId);
    }
}
