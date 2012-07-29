using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MnemonicAssignmentCalculator
{
	public partial class MainForm : Form
	{
		#region --Enums--

		private enum State
		{
			Uninitialized,
			Processing,
			Cancelling,
		}

		#endregion --Enums--

		#region --Fields--

		private MnemonicAssigner _assigner;
		private State _state = State.Uninitialized;

		#endregion --Fields--

		#region --Ctor(s) & Setup--

		public MainForm()
		{
			InitializeComponent();

			this.SetUpForm();
		}

		private void SetUpForm()
		{
			this.lblDistinctChars.Text = string.Empty;
			this.lblPermutaionsValue.Text = string.Empty;

			this._assigner = new MnemonicAssigner();
			this.lblIncrementMessage.Text = string.Format(MessageResource.IncrementMessageFormat, this._assigner.ProgressThreshold.ToString("N0"));

			this._assigner.WorkCompleted += new EventHandler<MnemonicAssigner.WorkCompleteEventArgs>(assigner_WorkCompleted);
			this._assigner.ProgressChanged += new EventHandler<MnemonicAssigner.WorkerProgressChangedEventArgs>(assigner_ProgressChanged);

			this.KeyDown += new KeyEventHandler(MainForm_KeyDown);

			this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
		}

		#endregion --Ctor(s) & Setup--

		#region --Event Handlers--

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyData == (Keys.Control | Keys.S)) && (this._state == State.Processing))
			{
				this.Cancel();
			}
		}

		private void assigner_ProgressChanged(object sender, MnemonicAssigner.WorkerProgressChangedEventArgs e)
		{
			this.UpdateProgess(e.Threshold, e.ThresholdReachedCount);
		}

		private void assigner_WorkCompleted(object sender, MnemonicAssigner.WorkCompleteEventArgs e)
		{
			this.UpdateUIForWorkCompleted(e.Success, e.ErrorMessage, e.Cancelled);
		}

		private void btnAction_Click(object sender, EventArgs e)
		{
			this.ToggleProcessing();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			this.BrowseToOutputFile();
		}

		#endregion --Event Handlers--

		#region --Private Methods--

		private void BrowseToOutputFile()
		{
			using (OpenFileDialog fileDialog = new OpenFileDialog())
			{
				DialogResult dialogResult = fileDialog.ShowDialog(this);
				if (dialogResult == System.Windows.Forms.DialogResult.OK)
				{
					this.txtOutputPath.Text = fileDialog.FileName;
				}
			}
		}

		private void Cancel()
		{
			this._state = State.Cancelling;
			this.btnAction.Text = MessageResource.CancellingButtonText;
			this.btnAction.Enabled = false;
			this._assigner.CancelWork();
		}

		private void EnableDisableInputControls(bool enabled)
		{
			this.txtInput.Enabled =
			this.txtCharsToIgnore.Enabled =
			this.btnBrowse.Enabled =
			this.txtOutputPath.Enabled = enabled;
		}

		private List<string> GetCharactersToIgnore()
		{
			List<string> charsToIgnore = new List<string>();
			string[] stringVals = this.txtCharsToIgnore.Text.Split(',');

			foreach (string stringVal in stringVals)
			{
				string temp = (stringVal ?? string.Empty).Trim();
				if (!string.IsNullOrEmpty(stringVal))
				{
					foreach (char character in temp.ToCharArray())
					{
						string newTemp = character.ToString().Trim();
						if (newTemp.Length > 0)
						{
							charsToIgnore.Add(newTemp);
						}
					}
				}
			}

			return charsToIgnore;
		}

		private List<string> GetItems()
		{
			string separator = ((char)22).ToString();
			string text = this.txtInput.Text.Trim().Replace(Environment.NewLine, separator);
			List<string> items = text.Split(separator.ToCharArray()).ToList();
			return items;
		}

		private void Initialize()
		{
			if (!this.ValidateForm())
			{
				return;
			}

			this._state = State.Processing;

			/* Update UI */
			this.picProcessing.Image = Properties.Resources.Spin;
			this.UpdateDistinctCharacterList();
			this.lblPermutaionsValue.Text = "0";

			this.EnableDisableInputControls(false);

			this.btnAction.Enabled = true;
			this.btnAction.Text = MessageResource.StopWorkButotnText;

			/* Run worker */
			this._assigner.Process(this.GetItems(), this.txtOutputPath.Text.Trim(), this.GetCharactersToIgnore());
		}

		private void ShowCompletionMessage(bool success, string errorMessage, bool cancelled)
		{
			string message = null;
			MessageBoxIcon icon = MessageBoxIcon.None;
			string header = null;

			if (!string.IsNullOrEmpty(errorMessage))
			{
				message = errorMessage;
				icon = MessageBoxIcon.Error;
				header = MessageResource.ErrorHeader;
			}
			else if (success)
			{
				message = MessageResource.SuccessMessage;
				icon = MessageBoxIcon.Information;
				header = MessageResource.SuccessHeader;
			}
			else if (cancelled)
			{
				message = MessageResource.ProcessingCancelledMessage;
				icon = MessageBoxIcon.Warning;
				header = MessageResource.CancelledHeader;
			}
			else
			{
				message = MessageResource.FailureMessage;
				icon = MessageBoxIcon.Warning;
				header = MessageResource.FailureHeader;
			}

			MessageBox.Show(this, message, header, MessageBoxButtons.OK, icon);
		}

		private void ToggleProcessing()
		{
			switch (this._state)
			{
				case State.Uninitialized:
					this.Initialize();
					break;
				case State.Processing:
					this.Cancel();
					break;
				case State.Cancelling:
				default:
					//Do nothing, wait for cancel to process
					break;
			}
		}

		private void UpdateDistinctCharacterList()
		{
			IList<string> distinctLetters = CharacterLogic.FindUniqueLetters(this.GetItems()) ?? new List<string>();
			IList<string> charsToIgnore = this.GetCharactersToIgnore();
			distinctLetters = distinctLetters.Except(charsToIgnore).OrderBy(x => x).ToList();
			this.lblDistinctChars.Text = (distinctLetters.Count > 0) ? distinctLetters.Aggregate((first, next) => first + ", " + next) : string.Empty;
		}

		private void UpdateProgess(long threshhold, long threshholdReachedCount)
		{
			if (this.InvokeRequired)
			{
				Action<long, long> delToInvoke = this.UpdateProgess;
				this.Invoke(delToInvoke, threshhold, threshholdReachedCount);
			}
			else
			{
				decimal numberOfIterations = threshhold * threshholdReachedCount;
				this.lblPermutaionsValue.Text = numberOfIterations.ToString("N0");
			}
		}

		private void UpdateUIForWorkCompleted(bool success, string errorMessage, bool cancelled)
		{
			if (this.InvokeRequired)
			{
				Action<bool, string, bool> delToInvoke = this.UpdateUIForWorkCompleted;
				this.Invoke(delToInvoke, success, errorMessage, cancelled);
			}
			else
			{
				this.picProcessing.Image = null;
				this.btnAction.Text = MessageResource.DoWorkButtonText;
				this._state = State.Uninitialized;

				this.ShowCompletionMessage(success, errorMessage, cancelled);

				this.EnableDisableInputControls(true);

				this.btnAction.Enabled = true;
			}
		}

		private bool ValidateForm()
		{
			if (this.txtInput.Text.Trim().Length == 0)
			{
				MessageBox.Show(MessageResource.InputValuesRequired, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.txtInput.Focus();
				return false;
			}

			if (this.txtOutputPath.Text.Trim().Length == 0)
			{
				MessageBox.Show(MessageResource.OutputPathRequired, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.txtOutputPath.Focus();
				return false;
			}

			return true;
		}

		#endregion --Private Methods--
	}
}