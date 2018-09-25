using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace iTools
{
	public class UtilityHelper
	{
		public static void SendCommand(string command)
		{
			var procStartInfo = new ProcessStartInfo("cmd.exe", @"/c " + command.Replace(Environment.NewLine, "&"))
			{
				UseShellExecute = false,
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			};

			Process.Start(procStartInfo);
		}

		public static IEnumerable<TranslationString> GetResourceStrings(string fileName)
		{
			var result = new List<TranslationString>();

			var excelApp = new Excel.Application();
			var worksheet = ExcelUtility.GetWorksheet(fileName, excelApp, "Sheet1");

			var index = 2;
			while (true)
			{
				var ts = new TranslationString();
				var enstring = ExcelUtility.GetCellValue(worksheet, "C", index);
				if (string.IsNullOrEmpty(enstring)) break;

				ts.String = enstring;
				ts.Key = ExcelUtility.GetCellValue(worksheet, "A", index);
				ts.What = ExcelUtility.GetCellValue(worksheet, "F", index);

				result.Add(ts);
				index++;
			}

			return result;
		}

		public static IList<string> GetExcelFiles(string folder)
		{
			return Directory.GetFiles(folder, "*.xlsx").Where(file => !file.Contains("~")).ToList();
		}

		public static void SaveContent(string file, string newFolder, IEnumerable<TranslationString> content)
		{
			var excelApp = new Excel.Application();

			var sheetName = Path.GetFileNameWithoutExtension(file);
			var worksheet = ExcelUtility.GetWorksheet(file, excelApp, sheetName);

			var row = 2;
			foreach (var item in content)
			{
				ExcelUtility.SetCellValue(worksheet, "A", row, item.Key);
				ExcelUtility.SetCellValue(worksheet, "C", row, item.String);
				row++;
			}

			var newFile = newFolder + Path.GetFileName(file);
			worksheet.SaveAs(newFile);

			excelApp.Quit();
			ExcelUtility.KillExcelApp(excelApp);
		}

		public static void UpdateTranslationFromXlsxToConfig(string gitFolder, string targetFileName, string folder)
		{
			gitFolder = string.IsNullOrEmpty(gitFolder) ? @"D:\Home\TonyZhao" : gitFolder;
			targetFileName = string.IsNullOrEmpty(targetFileName) ? @"E:\Translation\Target.xlsx" : targetFileName;
			folder = string.IsNullOrEmpty(folder) ? @"E:\Translation\DR-Translation\Test" : folder;

			var docs = CacheDoc(gitFolder);

			int index;

			// read the target content from Target.xlsx
			var targetItems = ReadTargetContent(targetFileName, out index);

			targetItems = ReBuildTargetItem(targetItems, docs);

			if (targetItems.Count < 1) return;

			// generate all the config files dictionary
			var templeFiles = ConfigFileList(gitFolder);

			var fileTypes = new List<string>();

			foreach (var item in targetItems.Where(item => !fileTypes.Contains(item.Item2)))
			{
				fileTypes.Add(item.Item2);
			}

			var files = GetExcelFiles(folder);

			if (files.Count < 1) return;

			var languages = new List<string>();
			var fileTypeKeyTranslationDic = new Dictionary<string, Dictionary<string, string>>();

			var count = index;
			GetLanTranslation(files, count, fileTypeKeyTranslationDic, languages);

			// each file type, e.g. DR, DRErrorMessages, SundanceLongBone...
			foreach (var fileType in fileTypes)
			{
				var keyLocationDic = targetItems.Where(p => p.Item2.Equals(fileType)).ToDictionary(item => item.Item1, item => item.Item3);

				// each language
				foreach (var language in languages)
				{
					UpdateTranslation(templeFiles, fileType, language, keyLocationDic, fileTypeKeyTranslationDic);
				}
			}
		}

		private static IList<Tuple<string, string, string>> ReBuildTargetItem(IEnumerable<Tuple<string, string, string>> targetItems, IList<XmlDocument> docs)
		{
			var targetItemFinal = new List<Tuple<string, string, string>>();

			foreach (var targetItem in targetItems)
			{
				var location = targetItem.Item3;
				if (string.IsNullOrEmpty(location))
				{
					foreach (var doc in docs)
					{
						var nsMgr = new XmlNamespaceManager(doc.NameTable);
						nsMgr.AddNamespace("ns", "http://tempuri.org/ResourceStrings.xsd");

						var node = doc.SelectSingleNode(String.Format("//ns:resourceString[text()=\"{0}\"]", targetItem.Item1), nsMgr);

						if (node == null || node.ParentNode == null || node.ParentNode.NextSibling == null ||
							node.ParentNode.NextSibling.ChildNodes[0] == null) continue;

						location = node.ParentNode.NextSibling.ChildNodes[0].InnerText;

						break;
					}
				}

				targetItemFinal.Add(new Tuple<string, string, string>(targetItem.Item1, targetItem.Item2, location));
			}

			return targetItemFinal;
		}

		private static IList<XmlDocument> CacheDoc(string gitFolder)
		{
			var docs = new List<XmlDocument>();
			foreach (var fileName in EnglishConfigFileList(gitFolder))
			{
				var doc = new XmlDocument();
				try
				{
					doc.Load(fileName);
					docs.Add(doc);
				}
				catch (Exception exception)
				{
					Trace.WriteLine(exception.Message);
				}
			}
			return docs;
		}

		private static void UpdateTranslation(Dictionary<string, string> templeFiles, string fileType, string language,
			Dictionary<string, string> keyLocationDic, Dictionary<string, Dictionary<string, string>> fileTypeKeyTranslationDic)
		{
			string fileName;
			templeFiles.TryGetValue(fileType, out fileName);

			if (string.IsNullOrEmpty(fileName))
				return;

			fileName = fileName.Replace("<Lan>", language);

			var doc = new XmlDocument();
			try
			{
				doc.Load(fileName);
			}
			catch (Exception)
			{
				return;
			}

			var nsMgr = new XmlNamespaceManager(doc.NameTable);
			nsMgr.AddNamespace("ns", "http://tempuri.org/ResourceStrings.xsd");

			// each key translation pair
			foreach (var keyLocation in keyLocationDic)
			{
				var key = keyLocation.Key;

				var keyNode = doc.SelectSingleNode(String.Format("//ns:resourceString[text()=\"{0}\"]", key), nsMgr);

				var location = keyLocation.Value;

				var locationNode = doc.SelectSingleNode(String.Format("//ns:resourceString[text()=\"{0}\"]", location), nsMgr);

				var rootNode = doc.SelectSingleNode("//ns:ResourceStrings", nsMgr);

				var newNode = doc.CreateElement("ResourceString");

				var resourceStringNode = doc.CreateElement("resourceString");
				resourceStringNode.InnerText = key;

				var translationNode = doc.CreateElement("translation");

				string translation = string.Empty;

				foreach (var lanKeyTranslation in (from dic in fileTypeKeyTranslationDic where dic.Key.Equals(language) select dic.Value))
				{

					lanKeyTranslation.TryGetValue(key, out translation);
					break;
				}

				if (keyNode != null)
				{
					if (keyNode.NextSibling != null) keyNode.NextSibling.InnerText = translation ?? string.Empty;
					continue;
				}
				translationNode.InnerText = translation ?? string.Empty;

				var commentNode = doc.CreateElement("comment");
				commentNode.InnerText = "Translated";

				newNode.AppendChild(resourceStringNode);
				newNode.AppendChild(translationNode);
				newNode.AppendChild(commentNode);

				if (rootNode == null)
					continue;

				if (locationNode == null)
				{
					rootNode.AppendChild(newNode);
				}
				else
				{
					rootNode.InsertAfter(newNode, locationNode.ParentNode);
				}
			}

			doc.Normalize();
			doc.PreserveWhitespace = true;
			doc.Save(fileName);
		}

		private static void FormatFiletoRaw(string fileName)
		{
			string content;
			using (var streamReader = File.OpenText(fileName))
			{
				content = streamReader.ReadToEnd();

				content = content.Replace("<LogMessage>\r\n    </LogMessage>", "<LogMessage></LogMessage>")
					.Replace("<comment>\r\n    </comment>", "<comment></comment>")
					.Replace("<translation>\r\n    </translation>", "<translation></translation>").
					Replace("<LogMessage>\r\n    </LogMessage>", "<LogMessage></LogMessage>").
					Replace("One or more of the detector's wireless network credentials will expire soon.", "One or more of the detector&apos;s wireless network credentials will expire soon.").
					Replace("Couldn't clamp the cassette correctl", "Couldn&apos;t clamp the cassette correctl").
					Replace("The laser reading is over power.  Please call service.\"", "The laser reading is over power.  Please call service.&quot;").
					Replace("Couldn&apos;t clamp the cassette correctly.", "Couldn't clamp the cassette correctly.").
						Replace("<ResourceString xmlns=\"\">", "<ResourceString>");

				content = fileName.Contains("DRErrorConfig")
					? content.Replace("Cassette task didn&apos;t respond to a command.", "Cassette task didn't respond to a command.")
					: content.Replace("Cassette task didn't respond to a command.", "Cassette task didn&apos;t respond to a command.");
				//content = content.Replace("One or more of the detector's wireless network credentials will expire soon.",
						//"One or more of the detector&apos;s wireless network credentials will expire soon.");

			}

			File.WriteAllText(fileName, content);
		}

		private static void GetLanTranslation(IEnumerable<string> files, int count, IDictionary<string, Dictionary<string, string>> fileTypeKeyTranslationDic, IList<string> languages)
		{
			var excelApp = new Excel.Application();
			foreach (var file in files)
			{
				var keyTranslationDic = new Dictionary<string, string>();
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
				if (fileNameWithoutExtension != null)
				{
					var lan = fileNameWithoutExtension.Replace("DR", "");

					var sn = lan + "DR";
					if (lan.Contains("Chinese"))
						sn = "ChineseDR";
					var worksheet = ExcelUtility.GetWorksheet(file, excelApp, sn);

					for (var i = 2; i <= count; i++)
					{
						var key = ExcelUtility.GetCellValue(worksheet, "A", i);
						var translation = ExcelUtility.GetCellValue(worksheet, "E", i);
						if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(translation)) break;

						switch (key)
						{
							case "DetectorNotActivated":
								key = "DetectorAwaitingPowerOn";
								keyTranslationDic.Add(key, translation);
								break;
							case "0x1":
								key = "DR-18401161";
								keyTranslationDic.Add(key, translation);
								break;
							case "0x2":
								key = "DR-18401162";
								keyTranslationDic.Add(key, translation);
								break;
							case "0x3":
								key = "DR-18401163";
								keyTranslationDic.Add(key, translation);
								key = "DR-18401164";
								keyTranslationDic.Add(key, translation);
								break;
							case "0x4":
								key = "DR-18401165";
								keyTranslationDic.Add(key, translation);
								key = "DR-18401166";
								keyTranslationDic.Add(key, translation);
								break;
							case "0x5":
								key = "DR-18401167";
								keyTranslationDic.Add(key, translation);
								key = "DR-18401168";
								keyTranslationDic.Add(key, translation);
								break;
							case "0x6":
								for (int k = 169; k < 215; k++)
								{
									key = "DR-18401" + k;
									keyTranslationDic.Add(key, translation);
								}
								break;
							case "0x7":
								break;
							case "0x8":
								key = "DR-18401225";
								keyTranslationDic.Add(key, translation);
								break;
							case "0x9":
								key = "DR-18401226";
								keyTranslationDic.Add(key, translation);
								break;
							default:
								if (key != null) keyTranslationDic.Add(key, translation);
								break;
						}
					}

					if (!fileTypeKeyTranslationDic.ContainsKey(lan))
						fileTypeKeyTranslationDic.Add(lan, keyTranslationDic);

					if (!languages.Contains(lan))
						languages.Add(lan);
				}
			}

			excelApp.Quit();
			ExcelUtility.KillExcelApp(excelApp);
		}

		private static IList<Tuple<string, string, string>> ReadTargetContent(string targetFileName, out int index)
		{
			var excelApp = new Excel.Application();
			var worksheet = ExcelUtility.GetWorksheet(targetFileName, excelApp, "Sheet1");

			var result = new List<Tuple<string, string, string>>();


			var doc1 = new XmlDocument();
			var nsMgr1 = new XmlNamespaceManager(doc1.NameTable);
			nsMgr1.AddNamespace("ns", "http://tempuri.org/ResourceStrings.xsd");
			var con = string.Concat(@"D:\Home\TonyZhao", @"\DR\EK\Capture\DR\Common\Configuration", "\\EnglishDR.config");
			doc1.Load(con);

			con = string.Concat(@"D:\Home\TonyZhao", @"\DR\EK\Capture\DR\Common\Configuration", "\\EnglishDRErrorMessages.config");
			var doc2 = new XmlDocument();
			var nsMgr2 = new XmlNamespaceManager(doc2.NameTable);
			nsMgr2.AddNamespace("ns", "http://tempuri.org/ResourceStrings.xsd");
			doc2.Load(con);

			index = 2;
			while (true)
			{
				var key = ExcelUtility.GetCellValue(worksheet, "A", index);
				var filType = ExcelUtility.GetCellValue(worksheet, "B", index);
				var location = ExcelUtility.GetCellValue(worksheet, "C", index);
				if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(filType)) break;

				switch (key)
				{
					case "18300059(Varian)":
						key = "DR-18300059";
						break;
					case "60611106":
						key = "DR-60611106";
						break;
					case "There are detectors enabled with a previous SSID and Passphrase.  Are you sure you want to save the Access Point Settings?":
						key = "IfForceUpdateAPSettings";
						break;
				}

				if (string.IsNullOrEmpty(location))
				{
					if (filType.Equals("DR"))
					{
						var node = doc1.SelectSingleNode(string.Format("//ns:resourceString[text()=\"{0}\"]", key), nsMgr1);
						if (node != null && node.ParentNode != null && node.ParentNode.PreviousSibling != null &&
							node.ParentNode.PreviousSibling.FirstChild != null &&
							!string.IsNullOrEmpty(node.ParentNode.PreviousSibling.FirstChild.InnerText))
						{
							location = node.ParentNode.PreviousSibling.FirstChild.InnerText;
						}
					}
					else if (filType.Equals("DRErrorMessages"))
					{
						var node = doc2.SelectSingleNode(string.Format("//ns:resourceString[text()=\"{0}\"]", key), nsMgr2);
						if (node != null && node.ParentNode != null && node.ParentNode.PreviousSibling != null &&
							node.ParentNode.PreviousSibling.FirstChild != null &&
							!string.IsNullOrEmpty(node.ParentNode.PreviousSibling.FirstChild.InnerText))
						{
							location = node.ParentNode.PreviousSibling.FirstChild.InnerText;
						}
					}
				}

				result.Add(new Tuple<string, string, string>(key, filType, location));
				index++;
			}

			excelApp.Quit();

			ExcelUtility.KillExcelApp(excelApp);

			return result;
		}



		private static Dictionary<string, string> ConfigFileList(string gitFolder)
		{
			var templeFiles = new Dictionary<string, string>
			{
				{"DR", string.Concat(gitFolder, @"\DR\EK\Capture\DR\Common\Configuration", "\\<Lan>DR.config")},
				{"DRErrorMessages",string.Concat(gitFolder, @"\DR\EK\Capture\DR\Common\Configuration", "\\<Lan>DRErrorMessages.config")},
				{"LongStrings", string.Concat(gitFolder, @"\DR\EK\Capture\DR\Common\Configuration", "\\<Lan>DRLongStrings.config")},
				{"OEM", string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration", "\\<Lan>OEM.config")},
				{"Sundance", string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration", "\\<Lan>Sundance.config")},
				{"SundanceCalibrationInstructions", string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration","\\<Lan>SundanceCalibrationInstructions.config")},
				{"SundanceLongBone",string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration", "\\<Lan>SundanceLongBone.config")},
				{"SundanceLongStrings",string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration", "\\<Lan>SundanceLongStrings.config")},
				{"Vi", string.Concat(gitFolder, @"\Evolution\EK\Capture\VI\Common\Configuration", "\\<Lan>Vi.config")},
				{"ViCalibrationInstructions",string.Concat(gitFolder, @"\Evolution\EK\Capture\VI\Common\Configuration","\\<Lan>ViCalibrationInstructions.config")},
				{"Segway", string.Concat(gitFolder, @"\Segway\EK\Capture\Segway\Common\Configuration", "\\<Lan>Segway.config")}
			};
			return templeFiles;
		}

		private static IEnumerable<string> EnglishConfigFileList(string gitFolder)
		{
			var templeFiles = new List<string>
			{
				string.Concat(gitFolder, @"\DR\EK\Capture\DR\Common\Configuration", "\\EnglishDR.config"),
				string.Concat(gitFolder, @"\DR\EK\Capture\DR\Common\Configuration", "\\EnglishDRErrorMessages.config"),
				string.Concat(gitFolder, @"\DR\EK\Capture\DR\Common\Configuration", "\\EnglishDRLongStrings.config"),
				string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration", "\\EnglishOEM.config"),
				string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration", "\\EnglishSundance.config"),
				string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration","\\EnglishSundanceCalibrationInstructions.config"),
				string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration", "\\EnglishSundanceLongBone.config"),
				string.Concat(gitFolder, @"\DRX-1\EK\Capture\Sundance\Common\Configuration", "\\EnglishSundanceLongStrings.config"),
				string.Concat(gitFolder, @"\Evolution\EK\Capture\VI\Common\Configuration", "\\EnglishVi.config"),
				string.Concat(gitFolder, @"\Evolution\EK\Capture\VI\Common\Configuration","\\EnglishViCalibrationInstructions.config"),
				string.Concat(gitFolder, @"\Segway\EK\Capture\Segway\Common\Configuration", "\\EnglishSegway.config")
			};
			return templeFiles;
		}

		private static string GetLanguageName(string file)
		{
			//15-080_DV5.7D Translation Strings_20150306_tr.xlsx
			var lan = file.Substring(file.LastIndexOf("_", StringComparison.Ordinal) + 1,
				file.LastIndexOf(".", StringComparison.Ordinal) - file.LastIndexOf("_", StringComparison.Ordinal) - 1);

			var language = string.Empty;
			switch (lan)
			{
				case "cs": language = "Czech"; break;
				case "da": language = "Danish"; break;
				case "de": language = "German"; break;
				case "el": language = "Greek"; break;
				case "es": language = "Spanish"; break;
				case "fi": language = "Finnish"; break;
				case "fr": language = "French"; break;
				case "hu": language = "Hungarian"; break;
				case "it": language = "Italian"; break;
				case "ja": language = "Japanese"; break;
				case "ko": language = "Korean"; break;
				case "nl": language = "Dutch"; break;
				case "no": language = "Norwegian"; break;
				case "pl": language = "Polish"; break;
				case "pt": language = "PortugueseIberian"; break;
				case "pt-br": language = "Portuguese"; break;
				case "ro": language = "Romanian"; break;
				case "ru": language = "Russian"; break;
				case "sv": language = "Swedish"; break;
				case "tr": language = "Turkish"; break;
				case "zh-cn": language = "ChineseS"; break;
				case "zh-tw": language = "ChineseT"; break;
			}
			return language;
		}

		public static void CreateErrorCodeMessage(string gitFolder, string fileName)
		{
			var drErrorConfig = gitFolder + @"\DR\EK\Capture\DR\Common\Configuration\DRErrorConfig.xml";
			var drErrorMessages = gitFolder + @"\DR\EK\Capture\DR\Common\Configuration\EnglishDRErrorMessages.config";

			if (string.IsNullOrEmpty(fileName)) return;

			var excelApp = new Excel.Application();
			var worksheet = ExcelUtility.GetWorksheet(fileName, excelApp, "Sheet1");

			var errorCodeMessages = new List<Tuple<string, string, string, string, string, string>>();

			var index = 1;
			var ierrorCodeIndex = 18401215;
			while (true)
			{
				var faultCode = ExcelUtility.GetCellValue(worksheet, "A", index);
				var description = ExcelUtility.GetCellValue(worksheet, "D", index);
				var action = ExcelUtility.GetCellValue(worksheet, "C", index);

				if (action.Equals("Action")) break;
				if (!action.Equals("0x7"))
				{
					index++;
					continue;
				}

				var uIMessage = ExcelUtility.GetCellValue(worksheet, "G", index).TrimEnd();
				var errorNumber = (ierrorCodeIndex++).ToString(CultureInfo.InvariantCulture);
				var drMessageIndex = "DR-" + errorNumber;

				errorCodeMessages.Add(new Tuple<string, string, string, string, string, string>(errorNumber, faultCode, description, action, drMessageIndex, uIMessage));
				index++;
			}


			var drErrorConfigDoc = new XmlDocument();
			drErrorConfigDoc.Load(drErrorConfig);

			var drErrorMessagesDoc = new XmlDocument();
			drErrorMessagesDoc.Load(drErrorMessages);

			var rootDrErrorConfigNode = drErrorConfigDoc.SelectSingleNode("//DRErrorConfig");


			var nsMgr = new XmlNamespaceManager(drErrorMessagesDoc.NameTable);
			nsMgr.AddNamespace("ns", "http://tempuri.org/ResourceStrings.xsd");
			var rootDrErrorMessagesNode = drErrorMessagesDoc.SelectSingleNode("//ns:ResourceStrings", nsMgr);

			foreach (var errorCodeMessage in errorCodeMessages)
			{
				var isSpecial = (errorCodeMessage.Item4.Equals("0x0"));

				var newDrErrorConfigNode = drErrorConfigDoc.CreateElement("DRErrorConfigItem");

				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "ErrorNumber", errorCodeMessage.Item1));
				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "Subsystem", "Carestream DRXPlus Detector (184)"));
				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "LogMessage", errorCodeMessage.Item3));
				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "DisplayAtConsole", isSpecial ? "False" : "True"));
				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "DisplayPriority", "E"));
				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "Modal", "False"));
				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "ResponseButtons", "Continue"));
				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "DisplayAtPositioner", "False"));
				newDrErrorConfigNode.AppendChild(CreateNewNode(drErrorConfigDoc, "DisplayInStatusBar", "False"));

				if (rootDrErrorConfigNode != null) rootDrErrorConfigNode.AppendChild(newDrErrorConfigNode);

				if (isSpecial) continue;

				var newDrErrorMessagesNode = drErrorMessagesDoc.CreateElement("ResourceString");

				newDrErrorMessagesNode.AppendChild(CreateNewNode(drErrorConfigDoc, "resourceString", errorCodeMessage.Item5));
				newDrErrorMessagesNode.AppendChild(CreateNewNode(drErrorConfigDoc, "translation", errorCodeMessage.Item6 + " Fault Code: " + errorCodeMessage.Item2));
				newDrErrorMessagesNode.AppendChild(CreateNewNode(drErrorConfigDoc, "comment", ""));

				if (rootDrErrorMessagesNode != null) rootDrErrorMessagesNode.AppendChild(newDrErrorMessagesNode);
			}

			drErrorConfigDoc.Save(drErrorConfig);
			FormatFiletoRaw(drErrorConfig);

			drErrorMessagesDoc.Save(drErrorMessages);
			FormatFiletoRaw(drErrorMessages);

			ExcelUtility.KillExcelApp(excelApp);
		}


		private static XmlNode CreateNewNode(XmlDocument doc, string nodeName, string text)
		{
			var newNode = doc.CreateElement(nodeName);
			newNode.InnerText = text;
			return newNode;
		}

		private static XmlNode CreateNewNode(XmlDocument doc, string nodeName, IEnumerable<string> attributes)
		{
			var newNode = doc.CreateElement(nodeName);

			foreach (var attribute in attributes)
			{
				var key = attribute.Split('|')[0];
				var value = attribute.Split('|')[1];
				newNode.SetAttribute(key, value);
			}
			return newNode;
		}


		public static void CopyKeyToANewKey(string keyName)
		{
			foreach (var file in Directory.GetFiles(@"D:\Home\TonyZhao\DR\EK\Capture\DR\Common\Configuration", @"*DRLongStrings.config"))
			{
				var doc = new XmlDocument();
				doc.Load(file);

				var nsMgr = new XmlNamespaceManager(doc.NameTable);
				nsMgr.AddNamespace("ns", "http://tempuri.org/ResourceStrings.xsd");

				var rootNode = doc.SelectSingleNode("//ns:ResourceStrings", nsMgr);
				var node1 = doc.SelectSingleNode(string.Format("//ns:resourceString[text()=\"{0}\"]", "DrxXrayInstruction"), nsMgr);

				if (node1 != null)
				{
					if (node1.NextSibling != null)
					{
						var tran = node1.NextSibling.InnerText.Replace("16", "8");

						var newNode = doc.CreateElement("ResourceString");

						newNode.AppendChild(CreateNewNode(doc, "resourceString", "DrxDawnXrayInstruction"));
						newNode.AppendChild(CreateNewNode(doc, "translation", tran));
						newNode.AppendChild(CreateNewNode(doc, "comment", "NEW ENGLISH: TRANSLATE NOW!"));

						if (rootNode != null) rootNode.InsertAfter(newNode, node1.ParentNode);
					}
				}
				doc.Save(file);

				FormatFiletoRaw(file);
			}
		}

		public static void DeleteNodes(string xmlFile, IList<string> keys)
		{
			var doc = new XmlDocument();
			doc.Load(xmlFile);

			var rootNode = doc.SelectSingleNode("//DRErrorConfig");

			int count = 0;
			foreach (var node in keys.Select(key => doc.SelectSingleNode(string.Format("//ErrorNumber[text()=\"{0}\"]", key))).Where(node => node != null))
			{
				if (rootNode != null) if (node != null) if (node.ParentNode != null) rootNode.RemoveChild(node.ParentNode);
				count++;
			}

			doc.Save(xmlFile);

			FormatFiletoRaw(xmlFile);
		}

		public static void DeleteErrorCode(string path, IList<string> keys)
		{
			foreach (var file in Directory.GetFiles(path, @"*DRErrorMessages.config"))
			{
				var doc = new XmlDocument();
				doc.Load(file);

				var nsMgr = new XmlNamespaceManager(doc.NameTable);
				nsMgr.AddNamespace("ns", "http://tempuri.org/ResourceStrings.xsd");

				var rootNode = doc.SelectSingleNode("//ns:ResourceStrings", nsMgr);

				foreach (var key in keys)
				{
					var node = doc.SelectSingleNode(string.Format("//ns:resourceString[text()=\"DR-{0}\"]", key), nsMgr);

					if (node != null)
						if (rootNode != null) if (node.ParentNode != null) rootNode.RemoveChild(node.ParentNode);
				}

				doc.Save(file);

				FormatFiletoRaw(file);
			}
		}

		public static void GenerateSulotionsNote(IDictionary<string, string> dics)
		{
			var doc = new XmlDocument();
			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "iToolsConfiguration.xml");

			doc.Load(path);
			var groups = doc.SelectSingleNode("//groups");

			if (groups == null) return;

			var node = doc.SelectSingleNode("//group[@name='Studio']");

			if (node != null)
				groups.RemoveChild(node);

			var newNode = doc.CreateElement("group");
			newNode.SetAttribute("name", "Studio");
			newNode.SetAttribute("index", "3");
			newNode.SetAttribute("title", "Studio");
			foreach (var dic in dics)
			{
				newNode.AppendChild(CreateNewNode(doc, "item",
					new[] { string.Format("name|{0}", dic.Key), string.Format("value|{0}", dic.Value) }));
			}

			groups.AppendChild(newNode);

			doc.Save(path);

			MessageBox.Show("S");
		}

		public static void DuplecateKey(string sourceKey, string target)
		{
			foreach (var file in Directory.GetFiles(@"D:\Home\TonyZhao\DR1\DR\EK\Capture\DR\Common\Configuration", @"*DRErrorMessages.config"))
			{
				var doc = new XmlDocument();
				doc.Load(file);

				var nsMgr = new XmlNamespaceManager(doc.NameTable);
				nsMgr.AddNamespace("ns", "http://tempuri.org/ResourceStrings.xsd");

				var rootNode = doc.SelectSingleNode("//ns:ResourceStrings", nsMgr);
				var node1 = doc.SelectSingleNode(string.Format("//ns:resourceString[text()=\"{0}\"]", sourceKey), nsMgr);

				if (node1 != null)
				{
					var newNode = CreateNewNode(doc, "ResourceString", "");

					newNode.AppendChild(CreateNewNode(doc, "resourceString", target));
					newNode.AppendChild(CreateNewNode(doc, "translation", node1.NextSibling.InnerText));
					newNode.AppendChild(CreateNewNode(doc, "comment", node1.NextSibling.NextSibling.InnerText));

					if (rootNode != null) rootNode.InsertAfter(newNode, node1.ParentNode);

					doc.Save(file);

					FormatFiletoRaw(file);
				}
			}
		}
	}
}
