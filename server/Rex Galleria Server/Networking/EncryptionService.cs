using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Rex_Galleria_Server.Networking
    {
        class EncryptionService
        {

            //Help Mentally - https://stackoverflow.com/questions/17128038/c-sharp-rsa-encryption-decryption-with-transmission
            // https://stackoverflow.com/questions/18485715/how-to-use-public-and-private-key-encryption-technique-in-c-sharp
            private string publicKey;
            private string privateKey;


            public EncryptionService()
            {
                var rsa = new RSACryptoServiceProvider(2048);
                privateKey = rsa.ToXmlString(true);
                publicKey = rsa.ToXmlString(false);
            }

        public string GetPublicKey()
        {
            string returnValue = "";
            string str = publicKey.Replace("<RSAKeyValue><Modulus>", "");
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != '<')
                {
                    returnValue += str[i];
                }
                else
                {
                    break;
                }
            }
            return returnValue;
        }

        public string GetPEMFormattedPublicKey()
        {
            string returnValue = "-----BEGIN RSA PUBLIC KEY-----";
            string key = GetPublicKey();
            for (int i = 0; i < key.Length; i++)
            {
                bool final = i == (key.Length - 1);
                if (i % 64 == 0 && !final)
                {
                    returnValue += '\n';
                }
                returnValue += key[i];
                if (final)
                {
                    returnValue += '\n';
                }
            }
            returnValue += "-----END RSA PUBLIC KEY-----";
            return returnValue;
        }
        public EncryptionService(string _publicKey)
            {
                publicKey = _publicKey;
            }

            bool encrypt = false;
            public byte[] encryptMessage(string data)
            {
                if (encrypt)
                {
                    return Encrypt(data);
                }
                else
                {
                    return System.Text.Encoding.UTF8.GetBytes(data);
                }
            }

            public string decryptMessage(byte[] data)
            {
                if (encrypt)
                {
                    return Decrypt(data);
                }
                else
                {
                    return System.Text.Encoding.UTF8.GetString(data);
                }
            }

            private string Decrypt(byte[] data)
            {
                var rsa = new RSACryptoServiceProvider();
                byte[] dataByte = data;

                rsa.FromXmlString(privateKey);
                var decryptedByte = rsa.Decrypt(dataByte, false);
                return System.Text.Encoding.UTF8.GetString(decryptedByte);
            }

            private byte[] Encrypt(string data)
            {
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(publicKey);
                var dataToEncrypt = System.Text.Encoding.UTF8.GetBytes(data);
                var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();


                return encryptedByteArray;
            }

            private string MaxLength(string text, int length = 10)
            {
                if (text == null)
                {
                    text = "null";
                }
                if (text.Length > length)
                {
                    return text.Substring(0, length - 3) + "...";
                }
                return text;
            }
            public override string ToString()
            {
            return "(EncryptionService.cs [" + publicKey.Length + " bytes][" + MaxLength(privateKey) + "," + MaxLength(publicKey) + "])";
            }

    }
    }

