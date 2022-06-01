using System.Security.Cryptography;
using System.Text;

namespace BlazorApp.Config; 

public class Hash {
    public static string Hash_SHA(string input) {
        using (SHA1Managed sha1Managed = new SHA1Managed()) {
            var hash = sha1Managed.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (Byte b in hash) {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}