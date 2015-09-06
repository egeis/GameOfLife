using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public interface IWorld
	{
		void Destroy();
		void Create(GameSystemSettings gss);
	}
}

