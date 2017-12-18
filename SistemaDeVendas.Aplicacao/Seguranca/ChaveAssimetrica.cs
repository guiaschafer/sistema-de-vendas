using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Aplicacao.Seguranca
{
    public static class ChaveAssimetrica
    {
        public static void GenKey_SaveInContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container   
            // name used to store the RSA key pair.  
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses  
            // the key container MyKeyContainerName.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            // Display the key information to the console.  
            //Console.WriteLine("Key added to container: \n  {0}", rsa.ToXmlString(true));
        }

        public static string GetKeyPrivateFromContainer(string ContainerName)
        {
             
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;
            
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            return rsa.ToXmlString(true);
        }

        public static string GetKeyPublicFromContainer(string ContainerName)
        {

            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            return rsa.ToXmlString(false);
        }

        public static void DeleteKeyFromContainer(string ContainerName)
        {
            // Create the CspParameters object and set the key container   
            // name used to store the RSA key pair.  
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses  
            // the key container.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            // Delete the key entry in the container.  
            rsa.PersistKeyInCsp = false;

            // Call Clear to release resources and delete the key from the container.  
            rsa.Clear();

            //Console.WriteLine("Key deleted.");
        }

        public static byte[] RSAEncrypt(byte[] plaintext, string destKey)
        {
            byte[] encryptedData;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(destKey);
            encryptedData = rsa.Encrypt(plaintext, true);
            rsa.Dispose();
            return encryptedData;
        }

        public static byte[] RSADecrypt(byte[] ciphertext, string srcKey)
        {
            byte[] decryptedData;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(srcKey);
            decryptedData = rsa.Decrypt(ciphertext, true);
            rsa.Dispose();
            return decryptedData;
        }
    }
}
