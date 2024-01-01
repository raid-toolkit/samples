using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Raid.Toolkit.Extensibility;

/**
 * This implementation is derived from BackgroundWorker
 * https://referencesource.microsoft.com/#System/compmod/system/componentmodel/BackgroundWorker.cs
 */
#nullable enable
public class RTKBackgroundWorker : IDisposable
{
	private IAppUI _appUI;
	private bool _disposed;
	private CancellationTokenSource _cancellation;

	public bool IsBusy { get; private set; }
	public bool WorkerReportsProgress { get; set; }
	public bool WorkerSupportsCancellation { get; set; }
	public bool CancellationPending => _cancellation.IsCancellationRequested;

	public event DoWorkEventHandler? DoWork;
	public event ProgressChangedEventHandler? ProgressChanged;
	public event RunWorkerCompletedEventHandler? RunWorkerCompleted;

	public static RTKBackgroundWorker Create(IExtensionHost host)
	{
		return host.CreateInstance<RTKBackgroundWorker>();
	}

	[Obsolete("Do not call directly, use static Create method instead")]
	public RTKBackgroundWorker(IAppUI appUI)
	{
		_appUI = appUI;
		_cancellation = new();
	}

	private void OnCompleted(RunWorkerCompletedEventArgs e)
	{
		IsBusy = false;
		_appUI.Dispatch(() => RunWorkerCompleted?.Invoke(this, e));
	}

	private void OnDoWork(DoWorkEventArgs e)
	{
		DoWork?.Invoke(this, e);
	}

	public void Cancel()
	{
		_cancellation.Cancel();
	}

	public void ReportProgress(int percentProgress)
	{
		ReportProgress(percentProgress, null);
	}

	private void OnProgressChanged(ProgressChangedEventArgs e)
	{
		_appUI.Dispatch(() => ProgressChanged?.Invoke(this, e));
	}

	public void ReportProgress(int percentProgress, object? userState)
	{
		if (!WorkerReportsProgress)
		{
			throw new InvalidOperationException("BackgroundWorker doesn't report progress");
		}

		ProgressChangedEventArgs args = new ProgressChangedEventArgs(percentProgress, userState);
		OnProgressChanged(args);
	}

	public void RunWorkerAsync()
	{
		RunWorkerAsync(null);
	}

	public void RunWorkerAsync(object? argument)
	{
		if (IsBusy)
			throw new InvalidOperationException("BackgroundWorker is already running");
		IsBusy = true;
		_cancellation = new();
		Task.Factory.StartNew(
			arg => WorkerThreadStart(arg),
			argument,
			_cancellation.Token,
			TaskCreationOptions.DenyChildAttach,
			TaskScheduler.Default);
	}

	private void WorkerThreadStart(object? argument)
	{
		object? result = null;
		Exception? error = null;

		try
		{
			DoWorkEventArgs doWorkArgs = new(argument);
			OnDoWork(doWorkArgs);
			if (doWorkArgs.Cancel)
			{
				_cancellation.Cancel();
			}
			else
			{
				result = doWorkArgs.Result;
			}
		}
		catch (TaskCanceledException) { }
		catch (Exception ex)
		{
			error = ex;
		}

		OnCompleted(new(result, error, _cancellation.IsCancellationRequested));
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects)
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			_disposed = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
#nullable restore