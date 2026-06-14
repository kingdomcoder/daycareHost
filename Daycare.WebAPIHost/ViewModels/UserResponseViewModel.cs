using System;
using Daycare.Domain.Entities;

namespace Daycare.WebAPIHost.ViewModels {
    /// <summary>
    /// API応答用のユーザーDTO。
    /// ApplicationUser エンティティを直接返すと PasswordHash / SecurityStamp 等の
    /// 機密フィールドまで漏れてしまうため、フロント(DaycareUser.fromJson)が実際に読む
    /// 項目だけを公開する。
    /// プロパティは PascalCase。System.Text.Json の明示オプション
    /// (new JsonSerializerOptions{...}) はプロパティ名をそのまま(PascalCase)出力するため、
    /// フロントの fromJson(json["Id"] 等)と一致する。
    /// </summary>
    public class UserResponseViewModel {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public string Shimei { get; set; }
        public string Myoji { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string TelNo { get; set; }
        public string Token { get; set; }
        public string OrganizationName { get; set; }
        public int? OrganizationId { get; set; }
        public string UserType { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public DateTime? LastAccessedDate { get; set; }

        public static UserResponseViewModel FromEntity(ApplicationUser user) {
            if (user == null) { return null; }
            return new UserResponseViewModel {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Prefix = user.Prefix,
                Shimei = user.Shimei,
                Myoji = user.Myoji,
                Street = user.Street,
                Street2 = user.Street2,
                City = user.City,
                State = user.State,
                Zip = user.Zip,
                TelNo = user.TelNo,
                Token = user.Token,
                OrganizationName = user.OrganizationName,
                OrganizationId = user.OrganizationId,
                UserType = user.UserType,
                RegisteredDate = user.RegisteredDate,
                LastAccessedDate = user.LastAccessedDate,
            };
        }
    }
}
