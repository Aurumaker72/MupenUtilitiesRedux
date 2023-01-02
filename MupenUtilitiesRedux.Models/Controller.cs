using System.Diagnostics;
using CommunityToolkit.HighPerformance.Helpers;

namespace MupenUtilitiesRedux.Models;

/// <summary>
///     A <see langword="class" /> which represents a logical controller in the emulator
/// </summary>
public class Controller
{
	private readonly Movie movie;

	public Controller(Movie movie, int index)
	{
		Trace.Assert(index is >= 0 and < Movie.MaxControllers);
		this.movie = movie;
		Index = index;
	}

	/// <summary>
	///     The index
	///     <para>
	///         Expected values: <c>1, 2, 3, 4</c>
	///     </para>
	/// </summary>
	public int Index { get; }

	/// <summary>
	///     Whether <see langword="this" /> is present
	/// </summary>
	public bool IsPresent
	{
		get => ControllerHelper.GetIsPresent(movie.ControllerFlags, Index);
		set => movie.ControllerFlags = ControllerHelper.SetIsPresent(movie.ControllerFlags, Index, value);
	}

	/// <summary>
	///     Whether <see langword="this" /> has a mempak attached
	/// </summary>
	public bool IsMempakAttached
	{
		get => ControllerHelper.GetIsMempakAttached(movie.ControllerFlags, Index);
		set => movie.ControllerFlags = ControllerHelper.SetIsMempakAttached(movie.ControllerFlags, Index, value);
	}

	/// <summary>
	///     Whether <see langword="this" /> has a rumblepak attached
	/// </summary>
	public bool IsRumblepakAttached
	{
		get => ControllerHelper.GetIsRumblepakAttached(movie.ControllerFlags, Index);
		set => movie.ControllerFlags = ControllerHelper.SetIsRumblepakAttached(movie.ControllerFlags, Index, value);
	}

	/// <summary>
	///     This controller's input sequence, or <see langword="null" /> by default if <see cref="IsPresent" /> is
	///     <see langword="false" />
	/// </summary>
	public List<Sample>? Samples { get; set; }
}

file static class ControllerHelper
{
	public static bool GetIsPresent(uint controllerFlags, int controllerIndex)
	{
		return BitHelper.HasFlag(controllerFlags, controllerIndex);
	}

	public static bool GetIsMempakAttached(uint controllerFlags, int controllerIndex)
	{
		return BitHelper.HasFlag(controllerFlags, controllerIndex + 4);
	}

	public static bool GetIsRumblepakAttached(uint controllerFlags, int controllerIndex)
	{
		return BitHelper.HasFlag(controllerFlags, controllerIndex + 8);
	}

	public static uint SetIsPresent(uint controllerFlags, int controllerIndex, bool flag)
	{
		var _val = controllerFlags;
		BitHelper.SetFlag(ref _val, controllerIndex, flag);
		return _val;
	}

	public static uint SetIsMempakAttached(uint controllerFlags, int controllerIndex, bool flag)
	{
		var _val = controllerFlags;
		BitHelper.SetFlag(ref _val, 4 + controllerIndex, flag);
		return _val;
	}

	public static uint SetIsRumblepakAttached(uint controllerFlags, int controllerIndex, bool flag)
	{
		var _val = controllerFlags;
		BitHelper.SetFlag(ref _val, 8 + controllerIndex, flag);
		return _val;
	}
}