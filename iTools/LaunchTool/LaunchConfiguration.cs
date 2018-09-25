using System;
using System.Linq;
using System.Xml;

namespace iTools.LaunchTool
{
	public class LaunchConfiguration
	{
		private static readonly string XmlPath = AppDomain.CurrentDomain.BaseDirectory + "\\iToolsConfiguration.xml";
		private static XmlDocument _doc;
		private static LaunchGroups _detailGroups;

		public static LaunchGroups LaunchGroups
		{
			get
			{
				Initialize();
				return _detailGroups;
			}
		}

		#region Initialize
		public LaunchConfiguration()
		{
			Initialize();
		}

		private static void Initialize()
		{
			_detailGroups = new LaunchGroups();
			LoadXmlDocument();
			PrepareData();
		}

		private static void LoadXmlDocument()
		{
			_doc = new XmlDocument();
			_doc.Load(XmlPath);
		}

		private static void PrepareData()
		{
			var nodes = _doc.SelectNodes(@"root/groups/group");
			if (nodes == null) return;

			for (var i = 0; i < nodes.Count; i++)
			{
				GenerateGroupsData(nodes[i]);
			}
		}
		#endregion

		#region Group
		private static void GenerateGroupsData(XmlNode node)
		{
			_detailGroups.Groups.Add(GenerateGroupData(node));
		}

		private static LaunchGroup GenerateGroupData(XmlNode node)
		{
			var group = new LaunchGroup();
			group.Name = GetAttributesValue(node, "name");
			group.Index = GetAttributesValue(node, "index");
			group.Title = GetAttributesValue(node, "title");
			for (var i = 0; i < node.ChildNodes.Count; i++)
			{
				group.Items.Add(GenerateItemData(node.ChildNodes[i]));
			}

			return group;
		}

		public static bool AddNewGroup(LaunchGroup group)
		{
			var node = _doc.SelectSingleNode("root/groups");
			var child = _doc.CreateElement("group");

			AddAttribute(child, "name", group.Name);
			AddAttribute(child, "index", group.Index);
			AddAttribute(child, "title", group.Title);

			if (node != null) node.AppendChild(child);
			_doc.Save(XmlPath);

			return true;
		}

		public static bool RemoveGroup(string groupName)
		{
			var node = _doc.SelectSingleNode("root/groups");
			var childnode = _doc.SelectSingleNode("root/groups/group[@name='" + groupName + "']");
			if (childnode != null) if (node != null) node.RemoveChild(childnode);
			_doc.Save(XmlPath);
			return true;
		}
		#endregion

		#region Item

		public static string GetItemValue(string guid, string groupName)
		{
			return LaunchGroups.Groups.Single(p => p.Name.Equals(groupName)).Items.First(p => p.Name.Equals(guid)).Value;
		}

		private static LaunchItem GenerateItemData(XmlNode node)
		{
			return new LaunchItem { Name = GetAttributesValue(node, "name"), Value = GetAttributesValue(node, "value") };
		}

		public static bool AddNewItem(LaunchItem item, string groupName)
		{
			var node = _doc.SelectSingleNode("root/groups/group[@name='" + groupName + "']");
			var child = _doc.CreateElement("item");

			AddAttribute(child, "name", item.Name);
            AddAttribute(child, "value", FormatSavedItem(item.Value));

			if (node != null) node.AppendChild(child);
			_doc.Save(XmlPath);

			return true;
		}

        private static string FormatSavedItem(string val)
        {
            var res = val;

            try
            {
                var temp = val;
                temp = temp.Replace("\"", "");
                res = string.Format("\"{0}\"", temp);
            }
            catch(Exception ex)
            {
            }

            return res;
        }

        public static bool UpdateItem(LaunchItem item, string groupName)
        {
            var node = _doc.SelectSingleNode("root/groups/group[@name='" + groupName + "']/item[@name='"+ item.Name +"']");

            if (node != null && node.Attributes["value"] != null) node.Attributes["value"].Value = FormatSavedItem(item.Value);
            _doc.Save(XmlPath);

            return true;
        }

		public static bool DeleteItem(LaunchItem item, string groupName)
		{
			var rootnode = _doc.SelectSingleNode("root/groups/group[@name='" + groupName + "']");
			var node = _doc.SelectSingleNode("root/groups/group/item[@name='" + item.Name + "']");
			if (rootnode != null) if (node != null) rootnode.RemoveChild(node);
			_doc.Save(XmlPath);

			return true;
		}

		public static bool MoveUpItem(LaunchItem item, string groupName)
		{
            var rootnode = _doc.SelectSingleNode("root/groups/group[@name='" + groupName + "']");
            var node = _doc.SelectSingleNode("root/groups/group[@name='" + groupName + "']/item[@name='" + item.Name + "']");

			if (node != null)
			{
				if (node.PreviousSibling != null)
				{
					var node1 = node.PreviousSibling.PreviousSibling;
					if (node1 != null)
					{
						if (rootnode != null)
						{
							rootnode.RemoveChild(node);
							rootnode.InsertAfter(node, node1);
						}
					}
				}
			}

			_doc.Save(XmlPath);

			return true;
		}

        public static bool MoveDownItem(LaunchItem item, string groupName)
		{
            var rootnode = _doc.SelectSingleNode("root/groups/group[@name='" + groupName + "']");
            var node = _doc.SelectSingleNode("root/groups/group[@name='" + groupName + "']/item[@name='" + item.Name + "']");

			if (node != null)
			{
				var node1 = node.NextSibling;
				if (node1 != null)
				{
					if (rootnode != null)
					{
						rootnode.RemoveChild(node);
						rootnode.InsertAfter(node, node1);
					}
				}
			}

			_doc.Save(XmlPath);

			return true;
		}

		#endregion

		private static void AddAttribute(XmlElement child, string key, string value)
		{
			var attr = _doc.CreateAttribute(key);
			attr.Value = value;
			child.Attributes.Append(attr);
		}
		
		private static string GetAttributesValue(XmlNode node, string key)
		{
			var result = string.Empty;
			if (node.Attributes != null && node.Attributes[key] != null)
				result = node.Attributes[key].Value;

			return result;
		}
	}
}
