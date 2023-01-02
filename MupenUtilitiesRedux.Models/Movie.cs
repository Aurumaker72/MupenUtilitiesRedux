using MupenUtilitiesRedux.Models.Attributes;
using MupenUtilitiesRedux.Models.Enums;

namespace MupenUtilitiesRedux.Models;

/// <summary>
///     A <see langword="class" /> describing a Movie following the
///     <see href="https://tasvideos.org/EmulatorResources/Mupen/M64">M64 format</see>
/// </summary>
[MovieMetadata(EndOfHeader = 0x400)]
public sealed class Movie
{
	/// <summary>
	///     The maximum amount of controllers which can be present at once
	/// </summary>
	public const int MaxControllers = 4;

	internal Controller[] controllers;

	internal Movie()
	{
	}

	/// <summary>
	///     The magic cookie value
	///     <para>Expected value: <c>0x4D36341A</c></para>
	/// </summary>
	[MovieValueMetadata(Offset = 0)]
	public uint Magic { get; set; }

	/// <summary>
	///     The version value
	///     <para>Expected value: <c>3</c>, a lower value indicates an outdated movie</para>
	/// </summary>
	[MovieValueMetadata(Offset = 4)]
	public uint Version { get; set; }

	/// <summary>
	///     The unique identifier in Unix epoch format
	/// </summary>
	[MovieValueMetadata(Offset = 8)]
	public uint Uid { get; set; }

	/// <summary>
	///     The amount of visual interrupts
	/// </summary>
	[MovieValueMetadata(Offset = 0x00C)]
	public uint VisualInterrupts { get; set; }

	/// <summary>
	///     The amount of rerecords
	/// </summary>
	[MovieValueMetadata(Offset = 0x010)]
	public uint Rerecords { get; set; }

	/// <summary>
	///     The amount of frames per second the rom region specifies
	///     <para>
	///         Expected values:
	///         <list type="bullet">
	///             <item>30, for PAL region roms</item>
	///             <item>60, for NTSC/J region roms</item>
	///         </list>
	///         Any other value is invalid
	///     </para>
	/// </summary>
	[MovieValueMetadata(Offset = 0x014)]
	public byte FramesPerSecond { get; set; }

	/// <summary>
	///     The amount of connected controllers
	///     <para>
	///         Expected values:
	///         <c>1, 2, 3, 4</c>
	///     </para>
	///     Any other value is invalid
	/// </summary>
	[MovieValueMetadata(Offset = 0x15, IsDeserializable = false)]
	public byte ControllerCount => Controllers.Aggregate<Controller, byte>(0,
		(current, controller) => (byte)(current + (controller.IsPresent ? 1 : 0)));

	/// <summary>
	///     The amount of logical frames
	/// </summary>
	[MovieValueMetadata(Offset = 0x18)]
	public uint Frames { get; internal set; }

	/// <summary>
	///     The start type
	///     <para>
	///         Expected values:
	///         <list type="table">
	///             <listheader>
	///                 <term>Value</term>
	///                 <description>Start Type</description>
	///             </listheader>
	///             <item>
	///                 <term>1</term>
	///                 <description>Snapshot</description>
	///             </item>
	///             <item>
	///                 <term>2</term>
	///                 <description>Start</description>
	///             </item>
	///             <item>
	///                 <term>4</term>
	///                 <description>EEPROM</description>
	///             </item>
	///         </list>
	///         Any other value is invalid
	///     </para>
	/// </summary>
	[MovieValueMetadata(Offset = 0x1C)]
	public ushort StartType { get; set; }

	/// <summary>
	///     The controller flags
	/// </summary>
	[MovieValueMetadata(Offset = 0x020)]
	internal uint ControllerFlags { get; set; }

	/// <summary>
	///     The rom's name
	/// </summary>
	[MovieValueMetadata(Offset = 0xC4, Length = 32, StringEncoding = StringEncodings.Ascii)]
	public string? RomName { get; set; }

	/// <summary>
	///     The rom's crc32
	/// </summary>
	[MovieValueMetadata(Offset = 0xE4)]
	public uint RomCrc32 { get; set; }

	/// <summary>
	///     The rom's country code
	/// </summary>
	[MovieValueMetadata(Offset = 0xE8)]
	public ushort CountryCode { get; set; }

	/// <summary>
	///     The video plugin's name
	/// </summary>
	[MovieValueMetadata(Offset = 0x122, Length = 64, StringEncoding = StringEncodings.Ascii)]
	public string? VideoPluginName { get; set; }

	/// <summary>
	///     The audio plugin's name
	/// </summary>
	[MovieValueMetadata(Offset = 0x162, Length = 64, StringEncoding = StringEncodings.Ascii)]
	public string? AudioPluginName { get; set; }

	/// <summary>
	///     The input plugin's name
	/// </summary>
	[MovieValueMetadata(Offset = 0x1A2, Length = 64, StringEncoding = StringEncodings.Ascii)]
	public string? InputPluginName { get; set; }

	/// <summary>
	///     The rsp plugin's name
	/// </summary>
	[MovieValueMetadata(Offset = 0x1E2, Length = 64, StringEncoding = StringEncodings.Ascii)]
	public string? RspPluginName { get; set; }

	/// <summary>
	///     The author
	/// </summary>
	[MovieValueMetadata(Offset = 0x222, Length = 222, StringEncoding = StringEncodings.Utf8)]
	public string? Author { get; set; }

	/// <summary>
	///     The description
	/// </summary>
	[MovieValueMetadata(Offset = 0x300, Length = 256, StringEncoding = StringEncodings.Utf8)]
	public string? Description { get; set; }

	/// <summary>
	///     The logical controllers, as provided by the input plugin
	///     <para>
	///         This collection is guaranteed to contain <see cref="ControllerCount" /> elements
	///     </para>
	/// </summary>
	public IReadOnlyList<Controller> Controllers => controllers;
}