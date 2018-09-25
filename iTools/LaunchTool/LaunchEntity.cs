using System.Collections.Generic;

namespace iTools.LaunchTool
{
	public class LaunchItem
	{
		public string Name { get; set; }
		public string Value { get; set; }
	}

	public class LaunchGroup
	{
		public LaunchGroup()
		{
			Items = new List<LaunchItem>();
		}

		public string Name { get; set; }

		public string Index { get; set; }

		public string Title { get; set; }

		public IList<LaunchItem> Items { get; set; }
	}

	public class LaunchGroups
	{
		public LaunchGroups()
		{
			Groups = new List<LaunchGroup>();
		}

		public IList<LaunchGroup> Groups { get; set; }
	}
}
