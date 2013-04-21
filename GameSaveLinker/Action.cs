using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSaveLinker
{
	public class Action
	{
		public int PathId { get; private set; }
		public int Id { get; private set; }
		public Game Game { get; private set; }
		public String Type { get; private set; }
		public ActionStatus Status { get; set; }

		public enum ActionStatus
		{
			None,
			Success,
			Failure
		};

		public Action(String type, Game game, int id)
			: this(type, game, -1, id)
		{
		}

		public Action(String type, Game game, int pathId, int id)
		{
			this.Type = type;
			this.Game = game;
			this.Id = id;
			this.Status = ActionStatus.None;
			this.PathId = pathId;
		}

		public String OrderAspect
		{
			get { return this.Id.ToString("000000"); }
		}

		public String GameAspect
		{
			get { return this.Game.Name.ToString(); }
		}

		public String StatusAspect
		{
			get { return GameSaveManager.PreviewAction(this.Game, this.Type, this.PathId); }
		}

		public String TypeAspect
		{
			get
			{
				switch (this.Type)
				{
					case "move-storage":
					case "move-original":
						return "Move";
					case "create-link":
						return "Link";
					case "delete-link":
						return "Unlink";
					case "hide":
						return "Hide";
					case "show":
						return "Show";
					default:
						return String.Empty;
				}
			}
		}

		public String TypeIconAspect
		{
			get
			{
				switch (this.Type)
				{
					case "move-storage":
					case "move-original":
						return "arrow";
					case "create-link":
						return "shortcut";
					case "delete-link":
						return "cross";
					case "hide":
						return "eye__minus";
					case "show":
						return "eye";
					default:
						return String.Empty;
				}
			}
		}

		public String StatusIconAspect
		{
			get
			{
				switch (this.Status)
				{
					case ActionStatus.Failure:
						return "cross";
					case ActionStatus.Success:
						return "tick";
					default:
						return String.Empty;
				}
			}
		}
	}

	public class ActionList : List<Action>
	{
		public void AddAction(String type, Game game)
		{
			Action newAction = new Action(type, game, this.Count() + 1);
			this.Add(newAction);
		}

		public void AddAction(String type, Game game, int pathId)
		{
			Action newAction = new Action(type, game, pathId, this.Count() + 1);
			this.Add(newAction);
		}
	}
}
