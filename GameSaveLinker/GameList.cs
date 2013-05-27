using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace GameSaveLinker
{
	public class GameList : List<Game>
	{
		protected Dictionary<string, string> _options;

		public GameList()
		{
			this._options = new Dictionary<string, string>();
		}

		/**
		 * Checks if any games are checked
		 */
		public Boolean HasChecked()
		{
			foreach (Game game in this)
			{
				if (game.Checked)
				{
					return true;
				}
			}
			return false;
		}

		/**
		 * Loads the games and options from the specified XML file
		 */
		public int LoadGames(String xmlFile, Boolean includeMissing = false)
		{
			this.Clear();
			GamePlaceholder.ResetPlaceholders();
			this._options.Clear();

			Trace.WriteLine(String.Format("{0}.LoadGames: Started loading games from {1}", this.GetType(), xmlFile));

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
						Trace.WriteLine(String.Format("{0}.LoadGames: Option found: {1}, value: {2}", this.GetType(), option.Name, this._options[option.Name]));
					}
				}
			}

			Trace.WriteLine(String.Format("{0}.LoadGames: Found {1} options", this.GetType(), this._options.Count));

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
								game.OriginalPath = attributeData.Value;
								break;
							case "Storage":
								game.StoragePath = attributeData.Value;
								break;
						}
					}

					foreach (XmlNode nodeData in gameData.ChildNodes)
					{
						switch (nodeData.Name)
						{
							case "Name":
								game.Name = nodeData.InnerText;
								break;
							case "Saves":
								game.OriginalPath = nodeData.InnerText;
								break;
							case "Storage":
								game.StoragePath = nodeData.InnerText;
								break;
						}
					}

					if (String.IsNullOrEmpty(game.StoragePathFull))
					{
						game.StoragePath = Path.Combine("{StoragePath}", DirectoryEx.FixStringForPath(game.Name));
					}

					if (!game.IsValid())
					{
						Trace.WriteLine(String.Format("{0}.LoadGames: Invalid game entry {1}", this.GetType(), gameData.OuterXml));
						continue;
					}

					if (!includeMissing && game.State == Game.SaveState.Missing)
					{
						Trace.WriteLine(String.Format("{0}.LoadGames: No saves found for {1}, search path {2}", this.GetType(), game.ToString(), game.OriginalPathFull));
						continue;
					}

					this.Add(game);
				}
			}
			Trace.WriteLine(String.Format("{0}.LoadGames: Found {1} games", this.GetType(), this.Count));

			return this.Count;
		}
	}
}
