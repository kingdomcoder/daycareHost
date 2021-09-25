using Daycare.Domain.Entities;
using Daycare.Domain.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daycare.Domain.Repositories.Concrete {

        public class EFUserRepository : IUserRepository {
            private MyUserDbContext context;
            private readonly UserManager<ApplicationUser> _userManager;

            public EFUserRepository(MyUserDbContext context, UserManager<ApplicationUser> userManager) {
                this.context = context;
                this._userManager = userManager;
            }

            public IEnumerable<ApplicationUser> User {
                get { return context.Users; }
            }

            public ApplicationUser GetUserProfileById(string id) {
                try {
                    return context.Users.Where(x => x.Id == id).FirstOrDefault();
                } catch (Exception ex) {
                    throw new Exception("Error at GetUserProfileById " + ex.Message);
                }
            }

            public ApplicationUser updateUser(ApplicationUser user, string password) {
                var u = context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                //   u.PasswordHash = passwordHash;
                //  u.PasswordSalt = passwordSalt;
                u.PasswordHash = password;
                // Update the properties
                context.Entry(u).State = EntityState.Modified;
                context.SaveChanges();
                return u;
            }


            public void updateUserConference(ApplicationUser user) {
                try {
                    var u = context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                    u.LastModifiedDate = DateTime.Now;
                    context.Entry(u).State = EntityState.Modified;
                    context.SaveChanges();
                } catch (Exception ex) {
                    throw new Exception("Error at updateUserConference in EFUserRepository.cs " + ex.Message);
                }
            }

            public void UpdateUser(ApplicationUser model) {
                try {
                    //(1) ApplicationUser
                    var dbEntry = context.Users
                        .Where(x => x.Id == model.Id).FirstOrDefault();
                    if (dbEntry != null) {
                        dbEntry.UserName = model.UserName;
                        dbEntry.Email = model.Email;
                        dbEntry.FirstName = model.FirstName;
                        dbEntry.LastName = model.LastName;
                        dbEntry.Prefix = model.Prefix;
                        dbEntry.Shimei = model.Shimei;
                        dbEntry.Myoji = model.Myoji;
                        dbEntry.Street = model.Street;
                        dbEntry.Street2 = model.Street2;
                        dbEntry.City = model.City;
                        dbEntry.State = model.State;
                        dbEntry.Zip = model.Zip;
                        dbEntry.Country = model.Country;
                        dbEntry.Yubin_Bango = model.Yubin_Bango;
                        dbEntry.To_Do_Fu_Ken = model.To_Do_Fu_Ken;
                        dbEntry.Shi_Gun_Ku = model.Shi_Gun_Ku;
                        dbEntry.Cho_Son = model.Cho_Son;
                        dbEntry.Apartment_Etc = model.Apartment_Etc;
                        dbEntry.TelNo = model.TelNo;
                        dbEntry.TimezoneName = model.TimezoneName;
                        dbEntry.UTCdiff = model.UTCdiff;
                        dbEntry.Token = model.Token;
                        dbEntry.OrganizationName = model.OrganizationName;
                        dbEntry.UserType = model.UserType;
                        dbEntry.LastAccessedDate = DateTime.Now;
                        context.Entry(dbEntry).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                } catch (Exception ex) {
                    throw new Exception("Error at UpdateUser: " + ex.Message);
                }
            }

            private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
                if (password == null) throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

                using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }

            private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) {
                if (password == null) throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
                if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
                if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

                using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt)) {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    for (int i = 0; i < computedHash.Length; i++) {
                        if (computedHash[i] != storedHash[i]) return false;
                    }
                }
                return true;
            }

            public IEnumerable<ApplicationUser> GetUsersBySearchKey(ApplicationUser model) {
                try {
                    var result = (from users in context.Users
                                  where
                                    (model.Email == null ? null == null : users.Email == model.Email) &&
                                    (model.FirstName == null ? null == null : users.FirstName == model.FirstName) &&
                                    (model.LastName == null ? null == null : users.LastName == model.LastName) 
                                  select users).ToList().Distinct();
                    return result;
                } catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
            }

            public void updateLastAccessedDate(ApplicationUser user) {
                var person = context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
                if (person != null) {
                    person.LastAccessedDate = DateTime.Now;
                    context.Entry(person).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }

            public void DeleteUserById(string Id) {
                try {
                    var dbEntry = (from user in context.User
                                   where user.Id == Id
                                   select user).FirstOrDefault();
                    if (dbEntry != null) {
                        context.User.Remove(dbEntry);
                        context.SaveChanges();
                    }
                } catch (Exception ex) {
                    throw new Exception("Error at DeleteUserById: " + ex.Message);
                }
            }
        }

}
