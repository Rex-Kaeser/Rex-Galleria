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
            public string publicKey;
            public string privateKey;


            public EncryptionService()
            {
                var rsa = new RSACryptoServiceProvider();
                privateKey = rsa.ToXmlString(true);
                publicKey = rsa.ToXmlString(false);
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


        }
    }

