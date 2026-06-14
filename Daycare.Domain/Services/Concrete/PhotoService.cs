using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using System.Collections.Generic;

namespace Daycare.Domain.Services.Concrete {
    public class PhotoService : IPhotoService {
        public IPhotoRepository photoRepository;

        public PhotoService(IPhotoRepository photoRepository) {
            this.photoRepository = photoRepository;
        }

        public Photo SavePhoto(Photo model) {
            return photoRepository.SavePhoto(model);
        }

        public IEnumerable<Photo> GetPhotosByChild(int childId) {
            return photoRepository.GetPhotosByChild(childId);
        }

        public Photo GetPhotoById(int photoId) {
            return photoRepository.GetPhotoById(photoId);
        }

        public bool IsParentOfChild(string userId, int childId) {
            return photoRepository.IsParentOfChild(userId, childId);
        }

        public bool IsChildInOrganization(int childId, int organizationId) {
            return photoRepository.IsChildInOrganization(childId, organizationId);
        }

        public void SoftDeletePhoto(int photoId) {
            photoRepository.SoftDeletePhoto(photoId);
        }
    }
}
