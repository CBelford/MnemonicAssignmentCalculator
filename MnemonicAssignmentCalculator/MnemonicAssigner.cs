using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace MnemonicAssignmentCalculator
{
	internal class MnemonicAssigner
	{
		#region --Constants--

		/// <summary>
		/// 200
		/// </summary>
		private const long DEFAULT_MAX_OUTPUT = 200;

		/// <summary>
		/// 10
		/// </summary>
		private const long DEFAULT_PROGRESS_THRESHOLD = 10;

		#endregion --Constants--

		#region --Events--

		public event EventHandler<WorkerProgressChangedEventArgs> ProgressChanged;

		public event EventHandler<WorkCompleteEventArgs> WorkCompleted;

		private delegate void AssignMnemonicsDelegate(IList<string> currentItems, IList<string> remainingLetters);

		#endregion --Events--

		#region --Fields--

		private bool _cancellationPending;
		private IAsyncResult _currentAsync;
		private string _errorMessage;
		private int _foundAssignmentCount = 0;
		private int _iterationCount = 0;
		private long _maxOutput;
		private string _outputPath = null;
		private long _progressThreshold;
		private bool _stop = false;
		private bool _success;
		private long _thresholdReachedCount;

		#endregion --Fields--

		#region --Ctor(s) & Setup--

		public MnemonicAssigner()
			: this(MnemonicAssigner.DEFAULT_PROGRESS_THRESHOLD, MnemonicAssigner.DEFAULT_MAX_OUTPUT)
		{
		}

		public MnemonicAssigner(long progressThreshold, long maxOutput)
		{
			this._progressThreshold = progressThreshold;
			this._maxOutput = maxOutput;
		}

		#endregion --Ctor(s) & Setup--

		#region --Public Properties--

		public long ProgressThreshold
		{
			get
			{
				return this._progressThreshold;
			}
		}

		#endregion --Public Properties--

		#region --Public Methods--

		public void CancelWork()
		{
			this._cancellationPending = true;
		}

		public void Process(IList<string> items, string outputPath, IList<string> lettersToIgnore)
		{
			/* Reset counters */
			this._foundAssignmentCount = 0;
			this._iterationCount = 0;
			this._stop = false;
			this._outputPath = outputPath;
			this._cancellationPending = false;
			this._success = false;
			this._errorMessage = null;

			/* Determine unique characters across input */
			IList<string> uniqueLetters = CharacterLogic.FindUniqueLetters(items);

			/* Remove characters to be ignored */
			if ((lettersToIgnore != null) && (lettersToIgnore.Count > 0))
			{
				uniqueLetters = uniqueLetters.Except(lettersToIgnore).ToList();
			}

			if (items.Count <= uniqueLetters.Count)
			{
				this._currentAsync = null;
				AssignMnemonicsDelegate delToInvoke = new AssignMnemonicsDelegate(this.AssignMnemonics);
				this._currentAsync = delToInvoke.BeginInvoke(items, uniqueLetters, this.AssignmentComplete, null);
			}
			else
			{
				/* Set error message - too few characters */
				this._errorMessage = string.Format(MessageResource.NotEnoughCharactersFormat, items.Count, uniqueLetters.Count);
				this.OnWorkCompleted();
			}
		}

		#endregion --Public Methods--

		#region --Private Methods--

		private void AssignMnemonic(IList<string> currentItems, IList<string> remainingLetters, Assignment parent)
		{
			const string lineSeparator = "----------------------";

			if (this._cancellationPending || this._stop)
			{
				return;
			}

			this._iterationCount++;

			if ((this._iterationCount % this._progressThreshold) == 0)
			{
				this._thresholdReachedCount++;
				this.OnProgressChanged(this._thresholdReachedCount, this._progressThreshold);
				this._iterationCount = 0; //Resetting iteration will allow for more iterations (count stored as long)
			}

			if (currentItems.Count == 0)
			{
				this._success = true;
				this.OutputFoundAssignments(parent, lineSeparator);
			}
			else if (remainingLetters.Count > 0)
			{
				this.DetermineAssignments(currentItems, remainingLetters, parent);
			}
		}

		private void AssignMnemonics(IList<string> items, IList<string> uniqueLetters)
		{
			/* Overwrite existing file */
			if (File.Exists(this._outputPath))
			{
				File.WriteAllText(this._outputPath, string.Empty);
			}

			/* Build root parent */
			Assignment root = Assignment.GenerateRoot();

			/* Create local collections */
			List<string> currentItems = items.OrderBy(x => x.Length).ToList();
			List<string> remainingLetters = new List<string>(uniqueLetters);

			/* Begin recursive assignment */
			this.AssignMnemonic(currentItems, remainingLetters, root);
		}

		private void AssignmentComplete(IAsyncResult asyncResult)
		{
			this.EndAsync(asyncResult);
		}

		private void DetermineAssignments(IList<string> currentItems, IList<string> remainingLetters, Assignment parent)
		{
			/* Unfortunately need to create new collections below else we change the underlying collection
			 * as we enumerate over it, and we'll get an error. */
			string item = currentItems.First();
			List<string> newCurrentNames = new List<string>(currentItems);
			newCurrentNames.Remove(item);

			foreach (string identifier in remainingLetters)
			{
				if (this._cancellationPending)
				{
					break;
				}

				if (item.Contains(identifier))
				{
					List<string> newRemainingIdentifiers = new List<string>(remainingLetters);
					newRemainingIdentifiers.Remove(identifier);

					Assignment assignment = new Assignment()
					{
						AssignedIdentifier = identifier,
						Item = item,
						Parent = parent
					};

					this.AssignMnemonic(newCurrentNames, newRemainingIdentifiers, assignment);
				}
			}
		}

		private void EndAsync(IAsyncResult asyncResult)
		{
			AsyncResult result = asyncResult as AsyncResult;
			AssignMnemonicsDelegate invokedDelegate = result.AsyncDelegate as AssignMnemonicsDelegate;
			invokedDelegate.EndInvoke(asyncResult);
			this.OnWorkCompleted();
		}

		private void OutputFoundAssignments(Assignment parent, string lineSeparator)
		{
			this._foundAssignmentCount++;

			List<Assignment> assignments = new List<Assignment>();

			Assignment currentAssignment = parent;

			do
			{
				assignments.Add(currentAssignment);
				currentAssignment = currentAssignment.Parent;
			} while (currentAssignment.Parent != null);

			assignments.Reverse();

			if (this._foundAssignmentCount <= this._maxOutput)
			{
				StringBuilder output = new StringBuilder(lineSeparator);
				output.Append(this._foundAssignmentCount.ToString());
				output.Append(lineSeparator + Environment.NewLine);

				foreach (Assignment helper in assignments)
				{
					output.Append(helper.Item);
					output.Append(" , Identifier: ");
					output.Append(helper.AssignedIdentifier);
					output.Append(Environment.NewLine);
				}

				File.AppendAllText(this._outputPath, output.ToString());
			}
			else
			{
				this._stop = true;
			}
		}

		#endregion --Private Methods--

		#region --Protected Methods--

		protected virtual void OnProgressChanged(long threshholdReachedCount, long threshhold)
		{
			EventHandler<WorkerProgressChangedEventArgs> progressChanged = this.ProgressChanged;
			if (progressChanged != null)
			{
				progressChanged(this, new WorkerProgressChangedEventArgs(threshholdReachedCount, threshhold));
			}
		}

		protected virtual void OnWorkCompleted()
		{
			EventHandler<WorkCompleteEventArgs> workCompleted = this.WorkCompleted;
			if (workCompleted != null)
			{
				workCompleted(this, new WorkCompleteEventArgs()
				{
					Success = this._success,
					ErrorMessage = this._errorMessage,
					Cancelled = this._cancellationPending,
				});
			}
		}

		#endregion --Protected Methods--

		#region --Helper Classes--

		private class Assignment
		{
			#region --Constants--

			public const string DEFAULT_ASSIGNED_IDENTIFIER = "NONE";
			public const string ROOT = "ROOT";

			#endregion --Constants--

			#region --Public Static Members--

			public static Assignment GenerateRoot()
			{
				return new Assignment()
				{
					AssignedIdentifier = Assignment.DEFAULT_ASSIGNED_IDENTIFIER,
					Item = Assignment.ROOT,
					Parent = null,
				};
			}

			#endregion --Public Static Members--

			#region --Public Properties--

			public string AssignedIdentifier
			{
				get;
				set;
			}

			public string Item
			{
				get;
				set;
			}

			public Assignment Parent
			{
				get;
				set;
			}

			#endregion --Public Properties--
		}

		internal class WorkCompleteEventArgs : EventArgs
		{
			#region --Public Properties--

			public bool Cancelled
			{
				get;
				set;
			}

			public string ErrorMessage
			{
				get;
				set;
			}

			public bool Success
			{
				get;
				set;
			}

			#endregion --Public Properties--
		}

		internal class WorkerProgressChangedEventArgs : EventArgs
		{
			#region --Ctor(s) & Setup--

			public WorkerProgressChangedEventArgs(long threshholdReachedCount, long threshhold)
			{
				this.Threshold = threshhold;
				this.ThresholdReachedCount = threshholdReachedCount;
			}

			#endregion --Ctor(s) & Setup--

			#region --Public Properties--

			public long Threshold
			{
				get;
				private set;
			}

			public long ThresholdReachedCount
			{
				get;
				private set;
			}

			#endregion --Public Properties--
		}

		#endregion --Helper Classes--

		#region --Other--

		/* REFACTOR:  Memory footprint of this could probably be improved.  We're storing chained Assignment classes
		 * in memory until we run out of mnemonics to assign, or we reach the max output.  Perhaps we could write
		 * to file as we find them.  Also, currently creating new List<T> on each recursion to get around error of
		 * "changing collection while enumerating."  Maybe we can do something about that. */

		#endregion --Other--
	}
}