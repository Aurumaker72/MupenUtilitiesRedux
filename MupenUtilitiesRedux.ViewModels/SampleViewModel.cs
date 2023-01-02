using CommunityToolkit.Mvvm.ComponentModel;
using MupenUtilitiesRedux.Models;

namespace MupenUtilitiesRedux.ViewModels;

/// <summary>
/// </summary>
[INotifyPropertyChanged(IncludeAdditionalHelperMethods = false)]
public partial class SampleViewModel
{
	private readonly Sample _sample;

	internal SampleViewModel(Sample sample)
	{
		_sample = sample;
	}

	/// <summary>
	///     Whether <c>D➡️</c> is held
	/// </summary>
	public bool DPadRight
	{
		get => _sample.DPadRight;
		set
		{
			_sample.DPadRight = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether <c>D⬅️</c> is held
	/// </summary>
	public bool DPadLeft
	{
		get => _sample.DPadLeft;
		set
		{
			_sample.DPadLeft = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether <c>D⬇️</c> is held
	/// </summary>
	public bool DPadDown
	{
		get => _sample.DPadDown;
		set
		{
			_sample.DPadDown = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether <c>D⬆️</c> is held
	/// </summary>
	public bool DPadUp
	{
		get => _sample.DPadUp;
		set
		{
			_sample.DPadUp = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether <c>Create</c> is held
	/// </summary>
	public bool Start
	{
		get => _sample.Start;
		set
		{
			_sample.Start = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether the <c>Z</c> trigger is held
	/// </summary>
	public bool Z
	{
		get => _sample.Z;
		set
		{
			_sample.Z = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether the <c>B</c> button is held
	/// </summary>
	public bool B
	{
		get => _sample.B;
		set
		{
			_sample.B = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether the <c>A</c> button is held
	/// </summary>
	public bool A
	{
		get => _sample.A;
		set
		{
			_sample.A = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether <c>C➡️</c> is held
	/// </summary>
	public bool CPadRight
	{
		get => _sample.CPadRight;
		set
		{
			_sample.CPadRight = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether <c>C⬅️</c> is held
	/// </summary>
	public bool CPadLeft
	{
		get => _sample.CPadLeft;
		set
		{
			_sample.CPadLeft = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether <c>C⬇️</c> is held
	/// </summary>
	public bool CPadDown
	{
		get => _sample.CPadDown;
		set
		{
			_sample.CPadDown = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether <c>C⬆️</c> is held
	/// </summary>
	public bool CPadUp
	{
		get => _sample.CPadUp;
		set
		{
			_sample.CPadUp = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether the <c>R</c> trigger is held
	/// </summary>
	public bool R
	{
		get => _sample.R;
		set
		{
			_sample.R = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     Whether the <c>L</c> trigger is held
	/// </summary>
	public bool L
	{
		get => _sample.L;
		set
		{
			_sample.L = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     The joystick's <c>X</c> axis' value
	/// </summary>
	public sbyte X
	{
		get => _sample.X;
		set
		{
			_sample.X = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	///     The joystick's <c>Y</c> axis' value
	/// </summary>
	public sbyte Y
	{
		get => _sample.Y;
		set
		{
			_sample.Y = value;
			OnPropertyChanged();
		}
	}
}