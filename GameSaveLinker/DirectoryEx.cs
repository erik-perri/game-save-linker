using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GameSaveLinker
{
	static class DirectoryEx
	{
		public static Boolean IsHidden(String path)
		{
			FileAttributes fileAttributes = File.GetAttributes(path);
			if ((fileAttributes & FileAttributes.System) == FileAttributes.System && (fileAttributes & FileAttributes.Hidden) == FileAttributes.Hidden)
			{
				return true;
			}
			return false;
		}

		public static Boolean Hide(String path)
		{
			File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.System | FileAttributes.Hidden);
			return DirectoryEx.IsHidden(path);
		}

		public static Boolean Show(String path)
		{
			File.SetAttributes(path, File.GetAttributes(path) & ~(FileAttributes.System | FileAttributes.Hidden));
			return !DirectoryEx.IsHidden(path);
		}

		public static Boolean HasNonHidden(String path)
		{
			IEnumerable<FileInfo> files = new DirectoryInfo(path).GetFiles().Where(x => (x.Attributes & FileAttributes.Hidden) == 0);
			IEnumerable<DirectoryInfo> directories = new DirectoryInfo(path).GetDirectories().Where(x => (x.Attributes & FileAttributes.Hidden) == 0);
			return files.Count() > 0 || directories.Count() > 0;
		}

		public static String FixStringForPath(String input, String replace = "-")
		{
			String output = input;
			String invalid =
				new String(Path.GetInvalidFileNameChars()) +
				new String(Path.GetInvalidPathChars())
			;

			foreach (Char c in invalid)
			{
				input = input.Replace(c.ToString(), replace);
			}

			return input;
		}
	}
}
