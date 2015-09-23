using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

namespace AssemblyCSharp
{
	public class Classic : ARules					
	{
		public static int MinAlive = 2;
		public static int MaxAlive = 3;

		public static class Range {
			public static int START = 3;
			public static int END = 3;
		}

		public Classic (Graph _g) : base (_g) {
			ruleset.Add ( new RuleSurvival());
			ruleset.Add ( new RuleUnderPop());
			ruleset.Add ( new RuleOverPop());
			ruleset.Add ( new RuleRePop());
		}
	}

	public class RuleSurvival : IRules {
		public bool Check (Node n)
		{
			int[] states = n.AdjacentStates;

			if (Node.ALIVE == n.State) {
				if(states[Node.ALIVE] >= Classic.MinAlive || states[Node.ALIVE] <= Classic.MaxAlive)
				{
					n.State = Node.ALIVE;
					return true;			//Exits Rule Check loop.
				}
			}

			return false;
		}
	}

	public class RuleUnderPop : IRules {
		public bool Check (Node n)
		{
			int[] states = n.AdjacentStates;
			
			if (Node.ALIVE == n.State) {
				if(states[Node.ALIVE] < Classic.MinAlive)
				{
					n.State = Node.DEAD;
					return true;			//Exits Rule Check loop.
				}
			}
			
			return false;
		}
	}

	public class RuleOverPop : IRules {
		public bool Check (Node n)
		{
			int[] states = n.AdjacentStates;
			
			if (Node.ALIVE == n.State) {
				if(states[Node.ALIVE] > Classic.MaxAlive)
				{
					n.State = Node.DEAD;
					return true;			//Exits Rule Check loop.
				}
			}
			
			return false;
		}
	}

	public class RuleRePop : IRules {
		public bool Check (Node n)
		{
			int[] states = n.AdjacentStates;
			
			if (Node.DEAD == n.State) {
				if(states[Node.ALIVE] <= Classic.Range.START || states[Node.ALIVE] >= Classic.Range.END)
				{
					n.State = Node.ALIVE;
					return true;			//Exits Rule Check loop.
				}
			}
			
			return false;
		}
	}
}

