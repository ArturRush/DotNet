using System.Collections;
using System.Collections.Generic;
using Aviation.Engines;

namespace Aviation.Aviation
{
	class AeroportEnumerator : IEnumerator<IPassengerAviation<IEngine>>
	{
		private readonly IAeroport _aeroport;
		private int _curIndex;
		private IPassengerAviation<IEngine> _curAero;

		public AeroportEnumerator(IAeroport aeroport)
        {
			_aeroport = aeroport;
			_curAero = default(IPassengerAviation<IEngine>);
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

		public IPassengerAviation<IEngine> Current
		{
			get { return _curAero; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}
	}
}
