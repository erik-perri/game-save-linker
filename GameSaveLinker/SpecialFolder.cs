using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace GameSaveLinker
{
	class SpecialFolder
	{
		public static string GetDownloads()
		{
			string path = GetFolderFromGuid(FolderDownloads);
			if (path == String.Empty)
			{
				path = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), "Downloads");
			}
			return path;
		}

		public static string GetMyGames()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games");
		}

		public static string GetSavedGames()
		{
			string path = GetFolderFromGuid(FolderSavedGames);
			if (path == String.Empty)
			{
				path = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), "Saved Games");
			}
			return path;
		}

		public static string GetFolderFromGuid(Guid folderGuid, int minimumMajorVersion = 6)
		{
			IntPtr path;
			if (SHGetKnownFolderPath(ref folderGuid, 0, IntPtr.Zero, out path) == 0)
			{
				string output = Marshal.PtrToStringUni(path);
				Marshal.FreeCoTaskMem(path);
				return output;
			}
			else
			{
				Console.WriteLine("SHGetKnownFolderPath({0}) failed", folderGuid);
			}
			return String.Empty;
		}

		private static Guid FolderSavedGames = new Guid("4C5C32FF-BB9D-43b0-B5B4-2D72E54EAAA4");
		private static Guid FolderDownloads  = new Guid("374DE290-123F-4565-9164-39C4925E467B");

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);
	}
}
