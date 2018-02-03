Imports System.Security.Cryptography

Public Class VSTripleDES

    Private Shared m_key() As Byte = {12, 23, 34, 45, 56, 67, 78, 89,
                                      90, 101, 112, 123, 134, 145, 156,
                                      167, 178, 189, 190, 201, 212, 223, 234, 245}

    Private Shared m_iv() As Byte = {65, 110, 68, 26, 69, 178, 200, 219}

    Private Shared m_tripledes As New TripleDESCryptoServiceProvider

    Public Shared Function EncryptData(ByVal plaintext As String) As String

        m_tripledes.Key = m_key
        m_tripledes.IV = m_iv

        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New CryptoStream(ms, m_tripledes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)

    End Function

    Public Shared Function DecryptData(ByVal encryptedtext As String) As String

        m_tripledes.Key = m_key
        m_tripledes.IV = m_iv

        ' Convert the encrypted text string to a byte array.
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New CryptoStream(ms, m_tripledes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)

    End Function

End Class