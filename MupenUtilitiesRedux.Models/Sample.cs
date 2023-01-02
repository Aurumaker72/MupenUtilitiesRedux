using CommunityToolkit.HighPerformance.Helpers;

namespace MupenUtilitiesRedux.Models;

/// <summary>
///     A <see langword="class" /> representing an input sample from a controller
/// </summary>
public class Sample
{
	private uint _raw;

	internal Sample(uint raw)
	{
		_raw = raw;
	}

	internal uint Raw => _raw;

	/// <summary>
	///     Whether <c>D➡️</c> is held
	/// </summary>
	public bool DPadRight
	{
		get => BitHelper.HasFlag(_raw, 0);
		set => BitHelper.SetFlag(ref _raw, 0, value);
	}

	/// <summary>
	///     Whether <c>D⬅️</c> is held
	/// </summary>
	public bool DPadLeft
	{
		get => BitHelper.HasFlag(_raw, 1);
		set => BitHelper.SetFlag(ref _raw, 1, value);
	}

	/// <summary>
	///     Whether <c>D⬇️</c> is held
	/// </summary>
	public bool DPadDown
	{
		get => BitHelper.HasFlag(_raw, 2);
		set => BitHelper.SetFlag(ref _raw, 2, value);
	}

	/// <summary>
	///     Whether <c>D⬆️</c> is held
	/// </summary>
	public bool DPadUp
	{
		get => BitHelper.HasFlag(_raw, 3);
		set => BitHelper.SetFlag(ref _raw, 3, value);
	}

	/// <summary>
	///     Whether <c>Start</c> is held
	/// </summary>
	public bool Start
	{
		get => BitHelper.HasFlag(_raw, 4);
		set => BitHelper.SetFlag(ref _raw, 4, value);
	}

	/// <summary>
	///     Whether the <c>Z</c> trigger is held
	/// </summary>
	public bool Z
	{
		get => BitHelper.HasFlag(_raw, 5);
		set => BitHelper.SetFlag(ref _raw, 5, value);
	}

	/// <summary>
	///     Whether the <c>B</c> button is held
	/// </summary>
	public bool B
	{
		get => BitHelper.HasFlag(_raw, 6);
		set => BitHelper.SetFlag(ref _raw, 6, value);
	}


	/// <summary>
	///     Whether the <c>A</c> button is held
	/// </summary>
	public bool A
	{
		get => BitHelper.HasFlag(_raw, 7);
		set => BitHelper.SetFlag(ref _raw, 7, value);
	}

	/// <summary>
	///     Whether <c>C➡️</c> is held
	/// </summary>
	public bool CPadRight
	{
		get => BitHelper.HasFlag(_raw, 8);
		set => BitHelper.SetFlag(ref _raw, 8, value);
	}

	/// <summary>
	///     Whether <c>C⬅️</c> is held
	/// </summary>

	public bool CPadLeft
	{
		get => BitHelper.HasFlag(_raw, 9);
		set => BitHelper.SetFlag(ref _raw, 9, value);
	}

	/// <summary>
	///     Whether <c>C⬇️</c> is held
	/// </summary>
	public bool CPadDown
	{
		get => BitHelper.HasFlag(_raw, 10);
		set => BitHelper.SetFlag(ref _raw, 10, value);
	}

	/// <summary>
	///     Whether <c>C⬆️</c> is held
	/// </summary>
	public bool CPadUp
	{
		get => BitHelper.HasFlag(_raw, 11);
		set => BitHelper.SetFlag(ref _raw, 11, value);
	}

	/// <summary>
	///     Whether the <c>R</c> trigger is held
	/// </summary>
	public bool R
	{
		get => BitHelper.HasFlag(_raw, 12);
		set => BitHelper.SetFlag(ref _raw, 12, value);
	}

	/// <summary>
	///     Whether the <c>L</c> trigger is held
	/// </summary>
	public bool L
	{
		get => BitHelper.HasFlag(_raw, 13);
		set => BitHelper.SetFlag(ref _raw, 13, value);
	}

	/// <summary>
	///     The joystick's <c>X</c> axis' value
	/// </summary>
	public sbyte X
	{
		get => (sbyte)BitHelper.ExtractRange(_raw, 8 * 2, 8 * 3);
		set => _raw = SetSByte(_raw, value, 8 * 2);
	}

	/// <summary>
	///     The joystick's <c>Y</c> axis' value
	/// </summary>
	public sbyte Y
	{
		get => (sbyte)BitHelper.ExtractRange(_raw, 8 * 3, 8 * 4);
		set => _raw = SetSByte(_raw, value, 8 * 3);
	}

	private static uint SetSByte(uint value, sbyte flag, int index)
	{
		var bytes = BitConverter.GetBytes(value);
		bytes[index / 8] = (byte)flag;

		//value &= (uint)~(0xFF << (8 * n));
		//value |= (uint)(flag << (8 * n));
		return BitConverter.ToUInt32(bytes);
	}
}