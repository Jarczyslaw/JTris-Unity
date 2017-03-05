using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;

public static class AES
{
	private const string _SALT = "HM5v5ZZE";
	private const string _INITVECTOR = "+@fd!G>Z96x49oJ{";

	private static byte[] _saltBytes;
	private static byte[] _initVectorBytes;

	static AES()
	{
		_saltBytes = Encoding.UTF8.GetBytes(_SALT);
		_initVectorBytes = Encoding.UTF8.GetBytes(_INITVECTOR);
	}
		   
	public static string Encrypt(string plainText, string password, string salt = null, string initialVector = null)
	{
		return Convert.ToBase64String(EncryptToBytes(plainText, password, salt, initialVector));
	}
		
	public static byte[] EncryptToBytes(string plainText, string password, string salt = null, string initialVector = null)
	{
		byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
		return EncryptToBytes(plainTextBytes, password, salt, initialVector);
	}
		
	public static byte[] EncryptToBytes(byte[] plainTextBytes, string password, string salt = null, string initialVector = null)
	{
		int keySize = 256;

		byte[] initialVectorBytes = string.IsNullOrEmpty(initialVector) ? _initVectorBytes : Encoding.UTF8.GetBytes(initialVector);
		byte[] saltValueBytes = string.IsNullOrEmpty(salt) ? _saltBytes : Encoding.UTF8.GetBytes(salt);
		byte[] keyBytes = new Rfc2898DeriveBytes(password, saltValueBytes).GetBytes(keySize / 8);

		using (RijndaelManaged symmetricKey = new RijndaelManaged())
		{
			symmetricKey.Mode = CipherMode.CBC;

			using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initialVectorBytes))
			{
				using (MemoryStream memStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
					{
						cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
						cryptoStream.FlushFinalBlock();

						return memStream.ToArray();
					}
				}
			}
		}
	}
		
	public static string Decrypt(string cipherText, string password, string salt = null, string initialVector = null)
	{
		byte[] cipherTextBytes = Convert.FromBase64String(cipherText.Replace(' ','+'));
		return Decrypt(cipherTextBytes, password, salt, initialVector).TrimEnd('\0');
	}
		
	public static string Decrypt(byte[] cipherTextBytes, string password, string salt = null, string initialVector = null)
	{
		int keySize = 256;

		byte[] initialVectorBytes = string.IsNullOrEmpty(initialVector) ? _initVectorBytes : Encoding.UTF8.GetBytes(initialVector);
		byte[] saltValueBytes = string.IsNullOrEmpty(salt) ? _saltBytes : Encoding.UTF8.GetBytes(salt);
		byte[] keyBytes = new Rfc2898DeriveBytes(password, saltValueBytes).GetBytes(keySize / 8);
		byte[] plainTextBytes = new byte[cipherTextBytes.Length];

		using (RijndaelManaged symmetricKey = new RijndaelManaged())
		{
			symmetricKey.Mode = CipherMode.CBC;

			using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
			{
				using (MemoryStream memStream = new MemoryStream(cipherTextBytes))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
					{
						int byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

						return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
					}
				}
			}
		}
	}
}