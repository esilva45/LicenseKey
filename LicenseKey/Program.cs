using System;
using System.Security.Cryptography;
using System.Text;

namespace LicenseKey {
    class Program {
        static void Main(string[] args) {
            try {
                string productIdentifier = Convert.ToString(args[0]);

                if (productIdentifier != "") {
                    Console.WriteLine(string.Format("Product Identifier: {0}", productIdentifier));
                    Console.WriteLine(string.Format("License Key: {0}", FormatLicenseKey(GetMd5Sum(productIdentifier))));
                } else {
                    Console.WriteLine("Informe o codigo");
                }
            }
            catch {
                Console.WriteLine("Informe o codigo");
            }
        }

        static string GenerateLicenseKey(string productIdentifier) {
            return FormatLicenseKey(GetMd5Sum(productIdentifier));
        }

        static string GetMd5Sum(string productIdentifier) {
            System.Text.Encoder enc = System.Text.Encoding.Unicode.GetEncoder();
            byte[] unicodeText = new byte[productIdentifier.Length * 2];
            enc.GetBytes(productIdentifier.ToCharArray(), 0, productIdentifier.Length, unicodeText, 0, true);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++) {
                sb.Append(result[i].ToString("X2"));
            }
            return sb.ToString();
        }

        static string FormatLicenseKey(string productIdentifier) {
            productIdentifier = productIdentifier.Substring(0, 28).ToUpper();
            char[] serialArray = productIdentifier.ToCharArray();
            StringBuilder licenseKey = new StringBuilder();

            int j = 0;
            for (int i = 0; i < 28; i++) {
                for (j = i; j < 4 + i; j++) {
                    licenseKey.Append(serialArray[j]);
                }
                if (j == 28) {
                    break;
                } else {
                    i = (j) - 1;
                    licenseKey.Append("-");
                }
            }
            return licenseKey.ToString();
        }
    }
}
