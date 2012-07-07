using System;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using Utilities;
using MpqReader;
using System.Collections.Generic;
using System.IO;
using BNet.Objects;

namespace MPQParser
{
	public sealed partial class MainForm : Form
	{
		public MainForm() { InitializeComponent(); }

		private void Start_Click(object sender, EventArgs e) { new Thread(Parse).Start(); }

		private void SetStatus(int progress, string message)
		{
			if(StatusLabel.InvokeRequired || StatusPB.InvokeRequired)
				StatusLabel.Invoke((MethodInvoker)(() => SetStatus(progress, message)));
			StatusLabel.Text = message;
			StatusPB.Value = progress;
		}

		private void SetPB(int max)
		{
			if(StatusPB.InvokeRequired)
				StatusPB.Invoke((MethodInvoker)(() => SetPB(max)));
			StatusPB.Minimum = 0;
			StatusPB.Maximum = max;
		}

		private void Parse()
		{
			var elapsed = ParsePatch("patch_d2.mpq", SetPB, SetStatus);

			SetPB(1);
			SetStatus(1, "Done in {0}ms!".Compose(elapsed.TotalMilliseconds));
		}

		public static TimeSpan ParsePatch(string patchFile, Action<int> maxCount, Action<int, string> status)
		{
			var watch = new System.Diagnostics.Stopwatch();
			watch.Start();
			var listfile = Path.Combine(Application.StartupPath, "mpq.txt");
			var fileNames = new[] { "Game.exe", "Bnclient.dll", "D2Client.dll" };
			var listfileExists = File.Exists(listfile);
			maxCount(fileNames.Length);
			status(0, "Extracting files");

			using(var arch = new MpqArchive(Path.Combine(Application.StartupPath, patchFile)))
			{
				if(listfileExists)
					arch.ExternalListFile = listfile;

				Directory.CreateDirectory("Dependencies");

				for(var count = 0; count < fileNames.Length; count++)
				{
					var fileName = fileNames[count];
					if(!arch.FileExists(fileName)) continue;

					var f = arch.Files.FirstOrDefault((file) => file.Name == fileName);

					status(count+1, "Extracting " + fileName);
					using(var stream = arch.OpenFile(f.Name))
					{
						// TODO: un-hardcode this...
						var contents = new byte[f.UncompressedSize];
						stream.BlockRead(contents);
						File.WriteAllBytes(Path.Combine("Dependencies", fileName), contents);
					}
				}
			}
			watch.Stop();

			return watch.Elapsed + ParseData(patchFile, maxCount, status);
		}

		private static TimeSpan ParseData(string patchFile, Action<int> maxCount, Action<int, string> status)
		{
			var watch = new System.Diagnostics.Stopwatch();
			var files = new Dictionary<string, MemoryStream>();
			var listfile = Path.Combine(Application.StartupPath, "mpq.txt");
			var fileNames = new[] {"itemstatcost", "armor", "weapons", "misc", "monstats"};
			const string fileName = @"DATA\GLOBAL\EXCEL\{0}.txt";
			const string patchFileName = @"{0}.txt";
			var listfileExists = File.Exists(listfile);
			maxCount(fileNames.Length * 2);

			using(var arch = new MpqArchive(Path.Combine(Application.StartupPath, patchFile)))
			{
				if(listfileExists)
					arch.ExternalListFile = listfile;

				var expbin = File.ReadAllBytes("monstats.bin");
				var table = new BNet.MonstatsTable(expbin);

				for(var i = 0; i < fileNames.Length; i++)
				{
					var fname = String.Format(fileName, fileNames[i]);
					var pfname = String.Format(patchFileName, fileNames[i]);
					if(!arch.FileExists(fname))
					{
						fname = pfname;
						if(!arch.FileExists(pfname)) continue;
					}

					var f = arch.Files.FirstOrDefault((file) => file.Name == fileName);

					status(i+1, "Extracting " + fileNames[i]);
					var contents = new byte[f.UncompressedSize];
					using(var stream = arch.OpenFile(f.Name))
						stream.BlockRead(contents);
					files.Add(fileNames[i], new MemoryStream(contents));
				}
			}

			status(fileNames.Length+1, "Parsing files");

			var count = 0;
			var output = new Dictionary<string, List<ITableRow>>() {
			            {"items", new List<ITableRow>()},
			            {"stats", new List<ITableRow>()},
			            {"monstats", new List<ITableRow>()},
			    };

			foreach(var file in files)
			{
				status(fileNames.Length + (++count), "Parsing " + file.Key);
				switch(file.Key)
				{
					case "itemstatcost":
						output["stats"].AddRange(DataManager.Parse<StatData>(new StreamReader(file.Value), '\t'));
						break;
					case "armor":
						output["items"].AddRange(DataManager.Parse<ArmorData>(new StreamReader(file.Value), '\t'));
						break;
					case "weapons":
						output["items"].AddRange(DataManager.Parse<WeaponData>(new StreamReader(file.Value), '\t'));
						break;
					case "misc":
						output["items"].AddRange(DataManager.Parse<MiscItemData>(new StreamReader(file.Value), '\t'));
						break;
					case "monstats":
						output["monstats"].AddRange(DataManager.Parse<MonsterData>(new StreamReader(file.Value), '\t'));
						break;
				}
			}
			watch.Stop();

			foreach(var file in output)
				DataManager.SaveTo(file.Key + ".dat", file.Value.ToArray());

			return watch.Elapsed;
		}
	}
}
