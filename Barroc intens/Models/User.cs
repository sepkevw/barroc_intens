using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Windows.Storage;

namespace Barroc_intens.Models
{
    internal class User
    {
        public static User LoggedInUser { get; set; }

        public int Id { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public DateTime Created_at { get; set; }
        public Role Role { get; set; }

        public string RememberToken { get; set; }

        public static String GenerateRememberToken()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = 10;
            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomChars[i] = characters[RandomNumberGenerator.GetInt32(0, characters.Length)];
            }

            return new string(randomChars);
        }

        public static async Task<bool> TryLoadUser()
        {
            using var connection = new AppDbContext();

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            try
            {
                var cookieFile = await storageFolder.GetFileAsync("remember_token.txt");
                string rememberToken = await FileIO.ReadTextAsync(cookieFile);

                User user = connection.Users.FirstOrDefault(u => u.RememberToken == rememberToken);

                if (user != null)
                {
                    return true;
                }

                return false;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
    }
}
