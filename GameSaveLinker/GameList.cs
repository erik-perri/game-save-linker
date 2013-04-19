using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace GameSaveLinker
{
	public class GameList : List<Game>
	{
		protected Dictionary<string, string> _options;

		public GameList()
		{
			this._options = new Dictionary<string, string>();
		}

		public int LoadGames(String xmlFile)
		{
			this.Clear();
			GamePlaceholder.ResetPlaceholders();
			this._options.Clear();

			Console.WriteLine("{0}.LoadGames: Started loading games from {1}", this.GetType().Name, xmlFile);

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(xmlFile);

			using (XmlNodeList options = xmlDoc.GetElementsByTagName("Options"))
			{
				if (options.Count > 0)
				{
					foreach (XmlNode option in options[0].ChildNodes)
					{
						this._options[option.Name] = GamePlaceholder.ReplacePlaceholders(option.InnerText);
						GamePlaceholder.AddPlaceholder(option.Name, this._options[option.Name]);
					}
				}
			}

			Console.WriteLine("{0}.LoadGames: Found {1} options", this.GetType().Name, this._options.Count);

			using (XmlNodeList gamesData = xmlDoc.GetElementsByTagName("Game"))
			{
				foreach (XmlNode gameData in gamesData)
				{
					Game game = new Game();

					foreach (XmlAttribute attributeData in gameData.Attributes)
					{
						switch (attributeData.Name)
						{
							case "Name":
								game.Name = attributeData.Value;
								break;
							case "Saves":
								game.UnparsedSavePath = attributeData.Value;
								break;
							case "Link":
								game.UnparsedLinkPath = attributeData.Value;
								break;
						}
					}

					if (string.IsNullOrEmpty(game.Name) || string.IsNullOrEmpty(game.UnparsedSavePath))
					{
						Console.WriteLine("{0}.LoadGames: Invalid game entry {1}", this.GetType().Name, gameData.OuterXml);
						continue;
					}

					if (string.IsNullOrEmpty(game.UnparsedLinkPath))
					{
						game.UnparsedLinkPath = Path.Combine("{OutputPath}", this.FixForPath(game.Name));
					}

					game.LinkPath = GamePlaceholder.ReplacePlaceholders(game.UnparsedLinkPath);
					game.SavePath = GamePlaceholder.ReplacePlaceholders(game.UnparsedSavePath);

					this.Add(game);
				}
			}
			Console.WriteLine("{0}.LoadGames: Found {1} games", this.GetType().Name, this.Count);

			return this.Count;
		}

		public string FixForPath(string input, string replace = "-")
		{
			string output = input;
			string invalid = 
				new string(Path.GetInvalidFileNameChars()) + 
				new string(Path.GetInvalidPathChars())
			;

			foreach (char c in invalid)
			{
				input = input.Replace(c.ToString(), replace);
			}

			return input;
		}

		public static bool DirectoryHasNonHidden(string directory)
		{
			IEnumerable<FileInfo> files = new DirectoryInfo(directory).GetFiles().Where(x => (x.Attributes & FileAttributes.Hidden) == 0);
			IEnumerable<DirectoryInfo> directories = new DirectoryInfo(directory).GetDirectories().Where(x => (x.Attributes & FileAttributes.Hidden) == 0);
			return files.Count() > 0 || directories.Count() > 0;
		}
	}

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
	}
}
