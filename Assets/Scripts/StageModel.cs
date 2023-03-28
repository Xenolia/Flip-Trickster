// dnSpy decompiler from Assembly-CSharp.dll class: StageModel
using System;
using System.Collections.Generic;

public class StageModel
{
	static StageModel()
	{
		StageModel.All.Add(new StageModel.Stage(1, "Gym", "GymNew", 1, 7));
		StageModel.All.Add(new StageModel.Stage(2, "Mountain", "MountNew", 2, 7));
		StageModel.All.Add(new StageModel.Stage(3, "City", "City", 3, 7));
		StageModel.All.Add(new StageModel.Stage(4, "Fun House", "House", 4, 7));
		StageModel.All.Add(new StageModel.Stage(5, "Factory", "Gallery", 5, 7));
		StageModel.All.Add(new StageModel.Stage(6, "Ship", "Ship", 6, 7));
		StageModel.All.Add(new StageModel.Stage(7, "Island", "Thailand", 7, 7));
		StageModel.All.Add(new StageModel.Stage(8, "Haunted House", "Haunted", 8, 7));
		StageModel.All.Add(new StageModel.Stage(9, "Space", "Space", 9, 7));
		foreach (StageModel.Stage stage in StageModel.All)
		{
			StageModel.Stages[stage.Id] = stage;
			if (stage.Order > 1)
			{
				foreach (StageModel.Stage stage2 in StageModel.All)
				{
					if (stage2.Order == stage.Order - 1)
					{
						StageModel.PreviousStages[stage.Id] = stage2.Id;
					}
				}
			}
			else
			{
				StageModel.FirstStageId = stage.Id;
				StageModel.PreviousStages[stage.Id] = 0;
			}
			if (stage.Order < StageModel.All.Count)
			{
				foreach (StageModel.Stage stage3 in StageModel.All)
				{
					if (stage3.Order == stage.Order + 1)
					{
						StageModel.NextStages[stage.Id] = stage3.Id;
					}
				}
			}
			else
			{
				StageModel.LastStageId = stage.Id;
				StageModel.NextStages[stage.Id] = 0;
			}
		}
	}

	public static bool IsFirstLevel(int currentStageId, int currentLevelId)
	{
		return currentLevelId == 1;
	}

	public static bool IsLastLevel()
	{
		return StageModel.IsLastLevel(LvlBtnHandler.activeStage, LvlBtnHandler.activeLevel);
	}

	public static bool IsLastLevel(int currentStageId, int currentLevelId)
	{
		StageModel.Stage stage = StageModel.Stages[currentStageId];
		return currentLevelId == stage.LevelCount;
	}

	public static int NextLevelId()
	{
		return StageModel.NextLevelId(LvlBtnHandler.activeStage, LvlBtnHandler.activeLevel);
	}

	public static int NextLevelId(int currentStageId, int currentLevelId)
	{
		return (!StageModel.IsLastLevel(currentStageId, currentLevelId)) ? (currentLevelId + 1) : 0;
	}

	public static int PreviousLevelId(int currentStageId, int currentLevelId)
	{
		return (!StageModel.IsFirstLevel(currentStageId, currentLevelId)) ? (currentLevelId - 1) : 0;
	}

	public static int FirstLevelId(int currentStageId)
	{
		return 1;
	}

	public static int GetLastLevelId(int currentStageId)
	{
		StageModel.Stage stage = StageModel.Stages[currentStageId];
		return stage.LevelCount;
	}

	public static int FirstLevelIdOfNextStage()
	{
		return StageModel.FirstLevelIdOfNextStage(LvlBtnHandler.activeStage);
	}

	public static int FirstLevelIdOfNextStage(int currentStageId)
	{
		int currentStageId2 = StageModel.NextStageId(currentStageId);
		return StageModel.FirstLevelId(currentStageId2);
	}

	public static int NextStageId()
	{
		return StageModel.NextStageId(LvlBtnHandler.activeStage);
	}

	public static int NextStageId(int currentStageId)
	{
		return StageModel.NextStages[currentStageId];
	}

	public static int PreviousStageId(int currentStageId)
	{
		return StageModel.PreviousStages[currentStageId];
	}

	public static bool IsFirstStage(int currentStageId)
	{
		return StageModel.PreviousStageId(currentStageId) == 0;
	}

	public static bool IsLastStage()
	{
		return StageModel.IsLastStage(LvlBtnHandler.activeStage);
	}

	public static bool IsLastStage(int currentStageId)
	{
		return StageModel.NextStageId(currentStageId) == 0;
	}

	public static string GetSceneName(int stageId)
	{
		StageModel.Stage stage = StageModel.Stages[stageId];
		return stage.Scene;
	}

	public static string GetStageName(int stageId)
	{
		StageModel.Stage stage = StageModel.Stages[stageId];
		return stage.Name;
	}

	public static int GetFirstStageId()
	{
		return StageModel.FirstStageId;
	}

	public static int GetLastStageId()
	{
		return StageModel.LastStageId;
	}

	public static int GetSecondStageId()
	{
		return StageModel.NextStages[StageModel.FirstStageId];
	}

	public static readonly List<StageModel.Stage> All = new List<StageModel.Stage>();

	private static readonly Dictionary<int, int> NextStages = new Dictionary<int, int>();

	private static readonly Dictionary<int, int> PreviousStages = new Dictionary<int, int>();

	private static readonly Dictionary<int, StageModel.Stage> Stages = new Dictionary<int, StageModel.Stage>();

	private static readonly int FirstStageId = 0;

	private static readonly int LastStageId = 0;

	public class Stage
	{
		public Stage(int id, string name, string scene, int order, int levelCount)
		{
			this.Id = id;
			this.Name = name;
			this.Scene = scene;
			this.Order = order;
			this.LevelCount = levelCount;
		}

		public readonly int Id;

		public readonly string Name;

		public readonly string Scene;

		public readonly int Order;

		public readonly int LevelCount;
	}
}
