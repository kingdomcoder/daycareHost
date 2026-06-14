using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Daycare.Domain.Services.Concrete {
    public class UserService : IUserService {
        IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration) {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        // 設定欠落時は秘密を含まない安全なメッセージで例外を投げる（キー名のみ提示）。
        private string RequireConfig(string key) {
            var value = configuration[key];
            if (string.IsNullOrWhiteSpace(value)) {
                throw new InvalidOperationException(
                    $"必要な設定が見つかりません（または空です）: '{key}'. appsettings.Development.json または環境変数で設定してください。");
            }
            return value;
        }

        IEnumerable<ApplicationUser> User {
            get { return userRepository.User; }
        }

        public ApplicationUser GetUserProfileById(string id) {
            return userRepository.GetUserProfileById(id);
        }

        public ApplicationUser updateUser(ApplicationUser user, string password) {
            return userRepository.updateUser(user, password);
        }

        public void UpdateUser(ApplicationUser user) {
            userRepository.UpdateUser(user);
        }

        public void PasswordRecoveryTokenRequest(string email, string token) {
            try {
                // SMTP 認証情報は設定から取得（実値は appsettings.Development.json / 環境変数）。
                string host = RequireConfig("Smtp:PasswordRecovery:Host");
                string user = RequireConfig("Smtp:PasswordRecovery:User");
                string password = RequireConfig("Smtp:PasswordRecovery:Password");
                string from = RequireConfig("Smtp:PasswordRecovery:From");
                int port = int.TryParse(configuration["Smtp:PasswordRecovery:Port"], out var prPort) ? prPort : 25;

                using (SmtpClient client = new SmtpClient(host, port)) {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(user, password);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(from);

                    mailMessage.To.Add(email.Trim());
                    mailMessage.Bcc.Add("oozeki@kingdomcoders.net");
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body =
                    "パスワード変更のリクエストをされた方にこのメールを送らせていただいております。下の指示に従ってパスワード変更の手続きを行ってください。" +
                    "もしパスワード変更のリクエストをされていない場合はこのメールは無視していただいて結構です。あなたのアカウントの安全性は保たれています。" + "<br>" +
                    "We received a request to reset the password associated with this email address. If you made this request, please follow the instructions below." +
                    "If you did not request to have your password reset you can safely ignore this email. Rest assured your customer account is safe." + "<br><br>" +

                    "下のリンクをクリックしてパスワード変更手続きをしてください" + "<br>" +
                    "Click the link below to reset your password:" + "<br><br>" +

                     "<a href='https://membership.jcfn.org/#/enter-new-password/token" + token.Replace('/', '$').Replace('+', '&').Replace("==", "#") +
                     "email" + email + "'><b>パスワード変更 Reset Password </b></a> " + "<br><br>" +

                    "このメールは自動送信されたものです。このメールに返信しないでください。ヘルプが必要な場合は以下までご連絡ください" + "<br>" +
                    "This is an automatically generated email, please do not reply. If you need a help, please contact below." + "<br><br>" +

                    "JCFN事務局" + "<br>" +
                    "JCFN Office " + "<br>" +
                    "Email: info@jcfn.org";
                    mailMessage.Subject = "パスワード変更手続き　Reset Password Assistance";
                    client.Send(mailMessage);
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }


        public void updateLastAccessedDate(ApplicationUser user) {
            userRepository.updateLastAccessedDate(user);
        }

        public void DeleteUserById(string Id) {
            userRepository.DeleteUserById(Id);
        }

        public void SendErrorMessageToAdmin(ApplicationUser model, string errorMessage) {
            try {
                // 管理者アラート用 SMTP 認証情報は設定から取得（実値は appsettings.Development.json / 環境変数）。
                string host = RequireConfig("Smtp:AdminAlert:Host");
                string user = RequireConfig("Smtp:AdminAlert:User");
                string password = RequireConfig("Smtp:AdminAlert:Password");
                string from = RequireConfig("Smtp:AdminAlert:From");
                int port = int.TryParse(configuration["Smtp:AdminAlert:Port"], out var aaPort) ? aaPort : 25;

                using (SmtpClient client = new SmtpClient(host, port)) {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(user, password);
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(from);
                    mailMessage.To.Add(from);
                    mailMessage.Bcc.Add("yasu_ozeki@hotmail.com");
                    mailMessage.Bcc.Add("oozeki@kingdomcoders.net");
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = errorMessage + "***********************************" +
                    "Email:" + model.Email + "<br>" +
                    "UserName:" + model.Email + "<br>" +
                    "CreatedDate:" + DateTime.Today + "<br>" +
                    "LastModifiedDate:" + DateTime.Today + "<br>" +
                    "FirstName:" + model.FirstName + "<br>" +
                    "LastName:" + model.LastName + "<br>" +
                    "Prefix:" + model.Prefix + "<br>" +
                    "Shimei:" + model.Shimei + "<br>" +
                    "Myoji:" + model.Myoji + "<br>" +
                    "Yubin_Bango:" + model.Zip + "<br>" +
                    "To_Do_Fu_Ken & To_Do_Fu_Ken_JP :" + model.State + "<br>" +

                    "Shi_Gun_Ku:" + model.City + "<br>" +
                    "Cho_Son:" + model.Street + "<br>" +
                    "Apartment_Etc:" + model.Street2 + "<br>" +
                    "Country:" + "Japan" + "<br>" +
                    "Yubin_Bango:" + model.Zip + "<br>" +
                    "Shi_Gun_Ku_JP:" + model.City + "<br>" +
                    "Cho_Son_JP:" + model.Street + "<br>" +
                    "Apartment_Etc_JP:" + model.Street2 + "<br>" +
                    "Country:" + "Japan" + "<br>" +
                    "Street:" + model.Street + "<br>" +
                    "Street2:" + model.Street2 + "<br>" +
                    "City:" + model.City + "<br>" +
                    "State:" + model.State + "<br>" +
                    "Zip:" + model.Zip + "<br>" +
                    "Country:" + model.Country + "<br>" +
                    "TelNo:" + model.TelNo + "<br>";

                    mailMessage.Subject = "ERROR - Childcare Registration";
                    client.Send(mailMessage);
                }
            } catch (Exception ex) {
                throw ex;
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
            return userRepository.GetUsersBySearchKey(model);
        }

        public ApplicationUser GetUserByTelNo(string telNo)
        {
            return userRepository.GetUserByTelNo(telNo);
        }

    }
}
