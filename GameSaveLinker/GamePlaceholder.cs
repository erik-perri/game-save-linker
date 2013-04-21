using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSaveLinker
{
	public class GamePlaceholder
	{
		protected static Dictionary<string, string> _placeholders = null;

		public static void SetupPlaceholder()
		{
			_placeholders = new Dictionary<string, string>();
			ResetPlaceholders();
		}

		public static void AddPlaceholder(string key, string value)
		{
			if (_placeholders == null)
			{
				SetupPlaceholder();
			}
			_placeholders.Add(key, value);
		}

		public static void ResetPlaceholders()
		{
			if (_placeholders == null)
			{
				SetupPlaceholder();
			}
			_placeholders.Clear();
			_placeholders.Add("UserProfile", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
			_placeholders.Add("AppData", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			_placeholders.Add("SavedGames", SpecialFolder.GetSavedGames());
			_placeholders.Add("MyGames", SpecialFolder.GetMyGames());
			_placeholders.Add("MyDocuments", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
		}

		public static string ReplacePlaceholders(string input)
		{
			if (_placeholders == null)
			{
				SetupPlaceholder();
			}

			foreach (KeyValuePair<String, String> entry in _placeholders)
			{
				input = input.Replace("{" + entry.Key + "}", entry.Value);
			}

			return input;
		}

		public static string RemovePlaceholders(string input)
		{
			if (_placeholders == null)
			{
				SetupPlaceholder();
			}

			foreach (KeyValuePair<String, String> entry in _placeholders)
			{
				input = input.Replace("{" + entry.Key + "}", "");
			}

			return input;
		}
	}
}
