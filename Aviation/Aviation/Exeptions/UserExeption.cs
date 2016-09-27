using System;

namespace Aviation.Exeptions
{
	public abstract class UserException : Exception
	{
		protected UserException(string mes) : base(mes){ }
	}

	public class NoTargetExeption : UserException
	{
		public NoTargetExeption(string mes) : base(mes) { }
	}

	public class PassPlaceException : UserException
	{
		public PassPlaceException(string mes) : base(mes) { }
	}
}