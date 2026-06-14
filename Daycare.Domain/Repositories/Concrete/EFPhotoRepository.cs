using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Daycare.Domain.Repositories.Concrete {
    public class EFPhotoRepository : IPhotoRepository {
        private readonly MyDbContext context;

        public EFPhotoRepository(MyDbContext context) {
            this.context = context;
        }

        public Photo SavePhoto(Photo model) {
            try {
                model.CreatedDate = DateTime.UtcNow;
                model.ActiveStatus = true;
                context.Photo.Add(model);
                context.SaveChanges();
                return model;
            } catch (Exception) {
                // Preserve original exception type and stack trace for diagnostics.
                throw;
            }
        }

        public IEnumerable<Photo> GetPhotosByChild(int childId) {
            try {
                var photos = (from table in context.Photo
                              where table.ChildId == childId &&
                              table.ActiveStatus == true
                              orderby table.CreatedDate descending
                              select table).ToList();
                return photos;
            } catch (Exception) {
                // Preserve original exception type and stack trace for diagnostics.
                throw;
            }
        }

        public Photo GetPhotoById(int photoId) {
            try {
                var photo = (from table in context.Photo
                             where table.PhotoId == photoId
                             select table).FirstOrDefault();
                return photo;
            } catch (Exception) {
                // Preserve original exception type and stack trace for diagnostics.
                throw;
            }
        }

        public bool IsParentOfChild(string userId, int childId) {
            try {
                var child = (from table in context.Child
                             where table.ChildId == childId &&
                             (table.Parent1Id == userId || table.Parent2Id == userId)
                             select table).FirstOrDefault();
                return child != null;
            } catch (Exception) {
                // Preserve original exception type and stack trace for diagnostics.
                throw;
            }
        }

        public bool IsChildInOrganization(int childId, int organizationId) {
            try {
                var child = (from table in context.Child
                             where table.ChildId == childId &&
                             table.OrganizationId == organizationId
                             select table).FirstOrDefault();
                return child != null;
            } catch (Exception) {
                // Preserve original exception type and stack trace for diagnostics.
                throw;
            }
        }

        public void SoftDeletePhoto(int photoId) {
            try {
                var photo = (from table in context.Photo
                             where table.PhotoId == photoId
                             select table).FirstOrDefault();
                if (photo != null) {
                    photo.ActiveStatus = false;
                    context.SaveChanges();
                }
            } catch (Exception) {
                // Preserve original exception type and stack trace for diagnostics.
                throw;
            }
        }
    }
}
