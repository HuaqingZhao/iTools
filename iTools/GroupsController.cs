using iTools.LaunchTool;

namespace iTools
{
	public class GroupsController
	{
		public LaunchGroups LaunchConfigurationGroups { get; set; }

		public void Load()
		{
			LaunchConfigurationGroups = LaunchConfiguration.LaunchGroups;
		}
	}
}
