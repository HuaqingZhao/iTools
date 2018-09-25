using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace iTools
{
	public partial class UtilitiesUserControl : UserControl
	{
		private const string Success = "Success";

		public UtilitiesUserControl()
		{
			InitializeComponent();
            GenerateFolderName();
		}

        private void GenerateFolderName()
        {
            try
            {
                cmbFolderName.Items.Clear();
                cmbFolderName.Items.AddRange(Directory.GetDirectories(txtLocalPath.Text));
            }
            catch(Exception ex)
            {

            }
        }

		private void BtnNewGuidClick(object sender, EventArgs e)
		{
			txtNewGuid.Text = Guid.NewGuid().ToString().ToUpper();
		}

		private void BtnTimeGoClick(object sender, EventArgs e)
		{
			txtSpan.Text = (DateTime.Parse(txtTo.Text) - DateTime.Parse(txtFrom.Text)).ToString();
		}

		private void btnBackup_Click(object sender, EventArgs e)
		{
			var files = txtGitFileNames.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			var folder = txtLocalPath.Text;

			if (string.IsNullOrEmpty(folder))
			{
				MessageBox.Show("Target folder is not exist.");
				return;
			}

			var date = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
			folder += "\\" + txtDescription.Text + "\\" + date;
			lblTargetFolder.Text = folder;

			var secondaryFolder = txtRemotePath.Text + txtDescription.Text + "\\" + date;

			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			var ip = txtRemotePath.Text.Substring(txtRemotePath.Text.IndexOf(@"\\", StringComparison.Ordinal) + 2);
			ip = ip.Substring(0, ip.IndexOf(@"\", StringComparison.Ordinal + 1));

			var connected = Ping(ip);
			var result = string.Empty;
			if (connected)
			{
				try
				{
					if (!Directory.Exists(secondaryFolder))
						Directory.CreateDirectory(secondaryFolder);
				}
				catch (Exception)
				{
					result = "Error in create folder at remote file system.";
				}
			}

			foreach (var fileName in files)
			{
				if (string.IsNullOrEmpty(fileName)) continue;
				if (fileName.Contains(@"\bin\")) continue;
				if (fileName.Contains(@"\obj\")) continue;
				if (fileName.Contains(@"\bin\")) continue;

				var temp = fileName.Replace(@":\", "+").Replace(@"\", "_");
				var targetFile = string.Format(@"{0}\{1}", folder, temp);
				var secondaryFolderTargetFile = string.Format(@"{0}\{1}", secondaryFolder, temp);

				try
				{
					File.Copy(fileName, targetFile);
					if (connected) File.Copy(fileName, secondaryFolderTargetFile);
				}
				catch (Exception)
				{

				}
			}

			ShowSuccessMsg(result);
		}

		private bool Ping(string ip)
		{
			var p = new System.Net.NetworkInformation.Ping();
			var options = new System.Net.NetworkInformation.PingOptions { DontFragment = true };

			var buffer = Encoding.ASCII.GetBytes("Test Data!");
			var reply = p.Send(ip, 1000, buffer, options);

			if (reply != null && reply.Status == System.Net.NetworkInformation.IPStatus.Success)
				return true;
			else
				return false;
		}

		private void btnRestore_Click(object sender, EventArgs e)
		{
			var folder = string.Empty;
			folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				folder = folderBrowserDialog1.SelectedPath;
			}

			if (string.IsNullOrEmpty(folder)) return;

			var files = Directory.GetFiles(folder);

			foreach (var file in files)
			{
				var fileName = file.Replace("+", @":\").Replace("_", "\\");
				fileName = fileName.Substring(fileName.LastIndexOf(@":\", StringComparison.Ordinal) - 1);
				File.Copy(file, fileName, true);
			}

			ShowSuccessMsg();
		}

		private static void ShowSuccessMsg(string msg = "")
		{
			MessageBox.Show(Success + msg);
		}

		private void btnChangePath_Click(object sender, EventArgs e)
		{
			var folder = string.Empty;
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				txtLocalPath.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			UtilityHelper.SendCommand("explorer " + lblTargetFolder.Text);
		}

		private void btnFolder_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				var path = folderBrowserDialog1.SelectedPath;
				txtFolder.Text = path;
				txtDescription.Text = path.Substring(path.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase) + 1);

				var sb = new StringBuilder();
				foreach (var item in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
				{
					if (string.IsNullOrEmpty(item)) continue;
					if (item.Contains(@"\bin\")) continue;
					if (item.Contains(@"\obj\")) continue;
					if (item.Contains(@"\bin\")) continue;

					sb.AppendLine(item);
				}

				txtGitFileNames.Text = sb.ToString();
			}
		}

		private void btnRegGo_Click(object sender, EventArgs e)
		{
			var patterns = txtRegEx.Text.Split(new[] { "|" }, StringSplitOptions.None);

			var result = new StringBuilder();
			foreach (var pattern in patterns)
			{
				var reg = new Regex(txtRegEx.Text);
				var matches = reg.Matches(txtRegSource.Text);

				foreach (Match match in matches)
				{
					result.AppendLine(match.Value);
				}
			}

			txtRegResult.Text = result.ToString();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtRegSource.Clear();
		}

		private void btnGitClear_Click(object sender, EventArgs e)
		{
			txtGitFileNames.Clear();
		}

		private void btnTranslationTranSource_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
				txtTranslationSourceFile.Text = openFileDialog1.FileName;
		}

		private void btnTanslationTranTarget_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				txtTranslationTarget.Text = folderBrowserDialog1.SelectedPath;
		}

		private void btnTranslationToTranGo_Click(object sender, EventArgs e)
		{
			var strings = UtilityHelper.GetResourceStrings(txtTranslationSourceFile.Text);

			var files = UtilityHelper.GetExcelFiles(txtTranslationTarget.Text);

			var newFolder = Path.Combine(Path.GetDirectoryName(files[0]), DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));

			Directory.CreateDirectory(newFolder);

			foreach (var file in files)
			{
				UtilityHelper.SaveContent(file, newFolder, strings);
			}

			ShowSuccessMsg();
		}

		private void btntxtTranConfigGo_Click(object sender, EventArgs e)
		{
			try
			{
				if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				{
					UtilityHelper.UpdateTranslationFromXlsxToConfig(txtTranConfigTarget.Text, txtTranConfigSource.Text, folderBrowserDialog1.SelectedPath);

					ShowSuccessMsg();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnTranErrorCodeSource_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				txtTranErrorCodeSource.Text = openFileDialog1.FileName;
			}
		}

		private void btnTranErrorCodeGo_Click(object sender, EventArgs e)
		{
			UtilityHelper.CreateErrorCodeMessage(txtTranErrorCodeTarget.Text, txtTranErrorCodeSource.Text);

			ShowSuccessMsg();
		}

		private void btnTranErrorCodeTarget_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				txtTranErrorCodeTarget.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var files = new List<string>();
			files.AddRange(Directory.GetFiles(@"D:\Home\TonyZhao\DR", "*.cs", SearchOption.AllDirectories).ToList());
			files.AddRange(Directory.GetFiles(@"D:\Home\TonyZhao\DIP", "*.cs", SearchOption.AllDirectories).ToList());

			var raw = File.ReadAllText(@"D:\Home\TonyZhao\DR\EK\Capture\Core\DRP\Common\Enumerations\CCDRErrorCodeEnum.cs");

			var CCDRErrorCode = raw;
			CCDRErrorCode = CCDRErrorCode.Substring(CCDRErrorCode.IndexOf("Drx1ExposureSwitchInitializeFailed"));

			CCDRErrorCode = ">" + CCDRErrorCode.Replace(Environment.NewLine, "").Replace("\t\t", "");
			var properties = CCDRErrorCode.Split(new[] { "///" }, StringSplitOptions.None);

			var target = new Dictionary<string, string>();
			foreach (var property in properties)
			{
				var temp = property.Substring(property.LastIndexOf(">") + 1).Replace("\t\t", "").Replace(" ", "").Replace(",", "");
				var p = temp.Split('=');

				if (p.Length != 2) continue;

				target.Add(p[0], p[1]);
			}

			var missedErrorCodes = new Dictionary<string, string>();

			foreach (KeyValuePair<string, string> p in target)
			{
				if (!p.Value.StartsWith("185")) continue;

				var found = files.Select(File.ReadAllText).Any(content => content.Contains("CCDRErrorCode." + p.Key));

				if (!found)
				{
					missedErrorCodes.Add(p.Key, p.Value);
				}
			}

			var sb = new StringBuilder();
			var sb1 = new StringBuilder();
			foreach (var missedErrorCode in missedErrorCodes)
			{
				sb.AppendLine(missedErrorCode.Value);
				sb1.AppendLine(missedErrorCode.Key);
			}

			UtilityHelper.DeleteNodes(@"D:\Home\TonyZhao\DR\EK\Capture\DR\Common\Configuration\DRErrorConfig.xml", missedErrorCodes.Values.ToList());
			UtilityHelper.DeleteNodes(@"D:\Home\TonyZhao\DIP\Carestream\Integration\Common\Configuration\DRErrorConfig.xml", missedErrorCodes.Values.ToList());
			UtilityHelper.DeleteErrorCode(@"D:\Home\TonyZhao\DR\EK\Capture\DR\Common\Configuration", missedErrorCodes.Values.ToList());
			UtilityHelper.DeleteErrorCode(@"D:\Home\TonyZhao\DIP\Carestream\Integration\Common\Configuration", missedErrorCodes.Values.ToList());

			var fields = raw.Substring(raw.IndexOf("Drx1ExposureSwitchInitializeFailed", StringComparison.Ordinal));

			var rawFields = fields.Split(new[] { "/// " }, StringSplitOptions.None).ToList();

			foreach (var missedErrorCode in missedErrorCodes)
			{
				foreach (var field in rawFields)
				{
					if (field.Contains(missedErrorCode.Key) && field.Contains(missedErrorCode.Value))
					{
						raw = raw.Replace("/// " + field, "");
					}
				}
			}

			var bs = Encoding.Default.GetBytes(raw);

			using (var fs = File.OpenWrite(@"D:\Home\TonyZhao\DR\EK\Capture\Core\DRP\Common\Enumerations\CCDRErrorCodeEnum1.cs"))
			{
				fs.Write(bs, 0, bs.Length);
				fs.Flush();
			}

			File.Delete(@"D:\Home\TonyZhao\DR\EK\Capture\Core\DRP\Common\Enumerations\CCDRErrorCodeEnum.cs");
			File.Copy(@"D:\Home\TonyZhao\DR\EK\Capture\Core\DRP\Common\Enumerations\CCDRErrorCodeEnum1.cs", @"D:\Home\TonyZhao\DR\EK\Capture\Core\DRP\Common\Enumerations\CCDRErrorCodeEnum.cs");
			File.Delete(@"D:\Home\TonyZhao\DR\EK\Capture\Core\DRP\Common\Enumerations\CCDRErrorCodeEnum1.cs");

			ShowSuccessMsg();
		}

		private void btnStudioGenerate_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				var dics = new Dictionary<string, string>();
				var selectedPath = folderBrowserDialog1.SelectedPath;

				foreach (var file in Directory.GetFiles(selectedPath, "*.sln", SearchOption.AllDirectories))
				{
					var fileName = Path.GetFileNameWithoutExtension(file);

					if(dics.ContainsKey(fileName)) continue;
					dics.Add(fileName,file);
				}

				UtilityHelper.GenerateSulotionsNote(dics);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			UtilityHelper.DuplecateKey("DR-65500516", "DR-65501016");
			ShowSuccessMsg();
		}

        private void txtLocalPath_TextChanged(object sender, EventArgs e)
        {
            GenerateFolderName();
        }

        private void cmbFolderName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFolderName.SelectedIndex > -1)
            {
                var selectText = cmbFolderName.SelectedItem.ToString();
                txtDescription.Text = selectText.Substring(selectText.LastIndexOf("\\") + 1);
            }
        }
	}
}
