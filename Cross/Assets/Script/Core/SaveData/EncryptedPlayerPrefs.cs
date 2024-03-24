using UnityEditor;
using UnityEngine;

namespace Core.SaveData
{
    public class EncryptedPlayerPrefs
    {
        /// <summary>
        /// セーブ
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Save(string key, string value)
        {
            string encKey = Encode.EncryptString(key);
            string encValue = Encode.EncryptString(value);
            PlayerPrefs.SetString(encKey, encValue);
            PlayerPrefs.Save();
        }

        #region ロード

        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>　bool : 成功・失敗　</returns>
        public bool TryLoad(string key, out string value)
        {
            string encKey = Encode.EncryptString(key);
            string encString = PlayerPrefs.GetString(encKey, string.Empty);
            if (string.IsNullOrEmpty(encString))
            {
                value = string.Empty;
                return false;
            }
            value = Encode.DecryptString(encString);
            return true;
        }
        #endregion ロード
        
        # region 暗号化/複合化
        /// <summary>
        /// 文字列の暗号化・復号化
        /// 参考：http://dobon.net/vb/dotnet/string/encryptstring.html
        /// </summary>
        private static class Encode
        {
            private static readonly string Pass = "ydhfUJNqih3sIGDVy2ug";
            private static readonly string Salt = "Fss3tb6srf3AeKG9XxC5";
            
            static readonly System.Security.Cryptography.RijndaelManaged rijndael;

            static Encode()
            {
                rijndael = new System.Security.Cryptography.RijndaelManaged();
                byte[] key, iv;
                GenerateKeyFromPassword(rijndael.KeySize, out key, rijndael.BlockSize, out iv);
                rijndael.Key = key;
                rijndael.IV = iv;
            }


            /// <summary>
            /// 文字列を暗号化する
            /// </summary>
            /// <param name="sourceString">暗号化する文字列</param>
            /// <returns>暗号化された文字列</returns>
            public static string EncryptString(string sourceString)
            {
                byte[] strBytes = System.Text.Encoding.UTF8.GetBytes(sourceString);
                System.Security.Cryptography.ICryptoTransform encryptor = rijndael.CreateEncryptor();
                byte[] encBytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
                encryptor.Dispose();
                return System.Convert.ToBase64String(encBytes);
            }

            /// <summary>
            /// 暗号化された文字列を復号化する
            /// </summary>
            /// <param name="sourceString">暗号化された文字列</param>
            /// <returns>復号化された文字列</returns>
            public static string DecryptString(string sourceString)
            {
                byte[] strBytes = System.Convert.FromBase64String(sourceString);
                System.Security.Cryptography.ICryptoTransform decryptor = rijndael.CreateDecryptor();
                //復号化に失敗すると例外CryptographicExceptionが発生
                byte[] decBytes = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
                decryptor.Dispose();
                return System.Text.Encoding.UTF8.GetString(decBytes);
            }

            /// <summary>
            /// パスワードから共有キーと初期化ベクタを生成する
            /// </summary>
            /// <param name="password">基になるパスワード</param>
            /// <param name="keySize">共有キーのサイズ（ビット）</param>
            /// <param name="key">作成された共有キー</param>
            /// <param name="blockSize">初期化ベクタのサイズ（ビット）</param>
            /// <param name="iv">作成された初期化ベクタ</param>
            private static void GenerateKeyFromPassword(int keySize, out byte[] key, int blockSize, out byte[] iv)
            {
                byte[] salt = System.Text.Encoding.UTF8.GetBytes(Salt);
                System.Security.Cryptography.Rfc2898DeriveBytes deriveBytes =
                    new System.Security.Cryptography.Rfc2898DeriveBytes(Pass, salt);
                deriveBytes.IterationCount = 1000;
                key = deriveBytes.GetBytes(keySize / 8);
                iv = deriveBytes.GetBytes(blockSize / 8);
            }
        }
        # endregion 暗号化/複合化

#if UNITY_EDITOR
        [MenuItem("SaveData/Delete")]
        public static void Delete()
        {
            PlayerPrefs.DeleteAll();
        }
#endif           
    }
}