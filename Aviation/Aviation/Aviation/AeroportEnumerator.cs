using System.Collections;
using System.Collections.Generic;
using Aviation.Engines;

namespace Aviation.Aviation
{
	public class AeroportEnumerator<T> : IEnumerator<T> where T : IPassengerAviation<IEngine>
	{
		private readonly IAeroport<T> _aeroport;
		private int _curIndex;
		private T _curAero;

		public AeroportEnumerator(IAeroport<T> aeroport)
        {
			_aeroport = aeroport;
			_curAero = default(T);
            _curIndex = -1;
        }

		public void Dispose()
		{
			
		}

		public bool MoveNext()
		{
			if (++_curIndex >= _aeroport.Count)
				return false;
			_curAero = _aeroport[_curIndex];
			return true;
		}

		public void Reset()
		{
			_curIndex = -1;
		}

		public T Current
		{
			get { return _curAero; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}
	}
}
