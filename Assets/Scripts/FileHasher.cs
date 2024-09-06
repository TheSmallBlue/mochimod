using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Hashes every file and sub-file in a directory
/// </summary>
public static class FileHasher
{
    public static HashedDirectory HashDirectory(string source)
    {
        return new HashedDirectory(Directory.GetFiles(source, "*", SearchOption.AllDirectories)
        .Select(x =>
        {
            var bytes = File.ReadAllBytes(x);

            var hash = MD5.Create().TransformBlock(bytes, 0, bytes.Length, bytes, 0);

            return new HashedFile() { filePath = x.Substring(x.IndexOf("\\game")), fileHash = hash.ToString() };
        })
        .ToArray());
    }
}

[System.Serializable]
public struct HashedFile
{
    public string filePath;
    public string fileHash;
}

[System.Serializable]
public struct HashedDirectory
{
    public HashedFile[] hashedFiles;

    public HashedDirectory(HashedFile[] files)
    {
        hashedFiles = files;
    }
}
