using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Windows.Storage;

namespace Barroc_intens.Models;

internal class User
{
    public static User LoggedInUser { get; set; }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
    public DateTime Created_at { get; set; }
    public Role Role { get; set; }

    public string RememberToken { get; set; }

    public static string GenerateRememberToken()
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var length = 10;
        var randomChars = new char[length];

        for (var i = 0; i < length; i++)
            randomChars[i] = characters[RandomNumberGenerator.GetInt32(0, characters.Length)];

        return new string(randomChars);
    }

    public static async Task<bool> TryLoadUser()
    {
        using var connection = new AppDbContext();

        var storageFolder = ApplicationData.Current.LocalFolder;

        try
        {
            var cookieFile = await storageFolder.GetFileAsync("remember_token.txt");
            var rememberToken = await FileIO.ReadTextAsync(cookieFile);

            var user = connection.Users.FirstOrDefault(u => u.RememberToken == rememberToken);

            if (user != null) return true;

            return false;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }
}