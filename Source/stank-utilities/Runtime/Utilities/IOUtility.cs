﻿using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that handles IO (input and output) operations.
    /// </summary>
    public static class IOUtility
    {
        /// <summary>
        /// Checks to see if a file ends with a specific file extension.
        /// </summary>
        /// <param name="file">File to check.</param>
        /// <param name="extension">File extension to check for.</param>
        /// <returns>Returns a bool that is true if the file ends with the provided extension. Returns false otherwise.</returns>
        public static bool IsFileExtension(string file, string extension)
        {
            // Check to make sure the file exists.
            if(!File.Exists(file))
            {
                //LogUtility.LogError("Couldn't check to see if the file was a " + extension + " file because the provided file doesn't exist!");
                return false;
            }
            
            // Check if the file's extension matches the one being looked for.
            return Path.GetExtension(file).ToLower() == extension.ToLower();
        }

        /// <summary>
        /// Checks to see if a file is a ZIP archive.
        /// </summary>
        /// <param name="file">File to check.</param>
        /// <returns>Returns a bool that is true if the file is a ZIP archive. Returns false otherwise.</returns>
        public static bool IsZipFile(string file)
        {
            return IsFileExtension(file, ".zip");
        }

        /// <summary>
        /// Copies a directory to another location.
        /// 
        /// Source: https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        /// </summary>
        /// <param name="sourceDirName">Source directory to copy.</param>
        /// <param name="destDirName">Destination to copy source to.</param>
        /// <param name="copySubDirs">Should sub-directories be copied?</param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo directory = new DirectoryInfo(sourceDirName);

            if(!directory.Exists)
            {
                DebuggerUtility.LogError("Couldn't copy directory because the source directory does not exist!");
                return;
            }

            // If the destination directory doesn't exist, create it.
            if(!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = directory.GetFiles();
            for(int i = 0; i < files.Length; i++)
            {
                string path = Path.Combine(destDirName, files[i].Name);

                // If file is a Unity .meta file, ignore it.
                if(IsFileExtension(path, ".meta"))
                {
                    continue;
                }

                files[i].CopyTo(path, true);
            }

            // If copying sub-directories, copy them and their contents to new location.
            if(copySubDirs)
            {
                DirectoryInfo[] subDirectories = directory.GetDirectories();
                for(int i = 0; i < subDirectories.Length; i++)
                {
                    string path = Path.Combine(destDirName, subDirectories[i].Name);
                    DirectoryCopy(subDirectories[i].FullName, path, copySubDirs);
                }
            }
        }

        /// <summary>
        /// Loads an array of bytes from a file.
        /// </summary>
        /// <param name="filePath">Path to the file to read.</param>
        /// <returns>Returns an array of bytes.</returns>
        public static byte[] ReadAllBytes(string filePath)
        {
            // Check to make sure the file exists.
            if(!File.Exists(filePath))
            {
                DebuggerUtility.LogError("Couldn't create a Texture from an image because the provided file doesn't exist!");
                return null;
            }

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Loads an array of bytes from a Stream of data.
        /// </summary>
        /// <param name="stream">Stream of data.</param>
        /// <returns>Returns an array of bytes.</returns>
        public static byte[] ReadAllBytes(Stream stream)
        {
            if(stream == null)
            {
                DebuggerUtility.LogError("Couldn't convert Stream to an array of byte[]! The provided Steam was null!");
                return null;
            }

            byte[] bytes = null;

            // Create a memory stream.
            using(MemoryStream memoryStream = new MemoryStream())
            {
                // Copy the stream to the memory stream.
                stream.CopyTo(memoryStream);

                // Convert the memory stream to an array of bytes.
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }

        /// <summary>
        /// Converts an object into an array of bytes.
        /// </summary>
        /// <param name="obj">Object to convert.</param>
        /// <returns>Returns an array of byte[] that was converted from an object.</returns>
        public static byte[] GetByteArrayFromObject(object obj)
        {
            byte[] byteArray = null;

            // Create a memory stream.
            using(MemoryStream memoryStream = new MemoryStream())
            {
                // Serialize the data into binary.
                new BinaryFormatter().Serialize(memoryStream, obj);
                
                // Convert the stream into an array of bytes.
                byteArray = memoryStream.ToArray();
            }

            return byteArray;
        }

        /// <summary>
        /// Opens a ZIP Archive and provides a callback to do whatever is needed while the file is open.
        /// </summary>
        /// <param name="path">Path to the ZIP Archive.</param>
        /// <param name="callback">Callback to execute while the ZIP file is open.</param>
        public static void OpenZIPArchive(string path, System.Action<FileStream, ZipArchive, ZipArchiveEntry, Stream> callback)
        {
            // Check to make sure the ZIP archive exists.
            if(!File.Exists(path))
            {
                DebuggerUtility.LogError("Couldn't open ZIP archive because the provided file doesn't exist!");
                return;
            }

            // Check to make sure the file is a ZIP archive.
            if(!IsZipFile(path))
            {
                DebuggerUtility.LogError("Couldn't open ZIP archive because the provided file is not a ZIP archive!");
                return;
            }

            // Open the file.
            using(FileStream file = File.OpenRead(path))
            {
                // Open the ZIP archive.
                using(ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
                {
                    // Loop through every file in the ZIP archive.
                    foreach(ZipArchiveEntry entry in zip.Entries)
                    {
                        // Create a Stream for the current file in the ZIP archive.
                        using(Stream stream = entry.Open())
                        {
                            // Execute the callback!
                            callback(file, zip, entry, stream);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Extracts a ZIP archive to a directory.
        /// </summary>
        /// <param name="path">Path of ZIP archive to extract.</param>
        /// <param name="destination">Destination of directory to extract ZIP archive to.</param>
        public static void ExtractZIPArchive(string path, string destination)
        {
            ZipFile.ExtractToDirectory(path, destination);
        }
    }
}
