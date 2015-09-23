using System;
namespace AssemblyCSharp
{
	public interface INode
	{
		float X { get; }
		float Y { get; }
		float Z { get; }
		int State { get; set; }
		void TriggerNextGeneration();
	}
}