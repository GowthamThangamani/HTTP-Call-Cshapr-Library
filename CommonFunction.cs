class CommonFunction{
 void ExtractZipFile(string ZipPathToExtract, string ExtractPath)
        {

            // Ensures that the last character on the extraction path
            // is the directory separator char. 
            // Without this, a malicious zip file could try to traverse outside of the expected
            // extraction path.
            if (!ExtractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                ExtractPath += Path.DirectorySeparatorChar;

            using (ZipArchive archive = ZipFile.OpenRead(ZipPathToExtract))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Gets the full path to ensure that relative segments are removed.
                    string destinationPath = Path.GetFullPath(Path.Combine(ExtractPath, entry.FullName.Replace(Path.DirectorySeparatorChar == '\\' ? '/' : '\\', Path.DirectorySeparatorChar)));

                    if (!Directory.Exists(Path.GetDirectoryName(destinationPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                    }


                    try
                    {
                        // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                        // are case-insensitive.
                        // And checking if the destination path is directory
                        //      if Directoroy, then we have already created the path, so skiping that part. 
                        if (!Directory.Exists(destinationPath) && destinationPath.StartsWith(ExtractPath, StringComparison.Ordinal))
                            entry.ExtractToFile(destinationPath);
                    }
                    catch (IOException ex)
                    {
                        //Below handling is because
                        //In Android version 7 and below, there is bug in unziping
                        //For more details: https://github.com/xamarin/xamarin-android/issues/2005#issuecomment-419829419
                        if (!ex.StackTrace.Contains("SetLastWriteTime"))
                        {
                            throw ex;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Below handling is because
                        //In Android version 7 and below, there is bug in unziping
                        //For more details: https://github.com/xamarin/xamarin-android/issues/2005#issuecomment-419829419
                        if (!ex.StackTrace.Contains("SetLastWriteTime"))
                        {
                            throw ex;
                        }
                    }
                }
            }
        }


}
