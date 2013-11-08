using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.Api.Response.Abstract
{
	public class Response : ICloneable
	{
		public bool CollectionsAreEqual<T>(List<T> thisList, List<T> otherList)
		{
			if (thisList.Count != otherList.Count)
			{
				return false;
			}
			bool areEqual = true;
			for (int i = 0; i < thisList.Count; i++)
			{
				T i1 = thisList[i];
				T i2 = otherList[i];
				areEqual = areEqual && i1.Equals(i2);
				if (!areEqual)
				{
					break;
				}
			}
			return areEqual;
		}

		public override bool Equals(object obj)
		{
			throw new NotImplementedException(this.GetType().Name + ".Equals not implemented.");
		}

		public object Clone()
		{
			return null;
		}


		protected List<T> CloneCollection<T>(List<T> thisList) where T : ICloneable
		{
			List<T> cloned = new List<T>();
			foreach (T t in thisList)
			{
				cloned.Add((T) t.Clone());
			}
			return cloned;
		}
			 
	}
}
