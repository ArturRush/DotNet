using System;

namespace Aviation.Exeptions
{
	/// <summary>
	/// Пользовательские исключения
	/// </summary>
	public abstract class UserException : Exception
	{
		protected UserException(string mes) : base(mes){ }
	}
	/// <summary>
	/// Исключение отсутствия цели сообщения
	/// </summary>
	public class NoTargetExeption : UserException
	{
		public NoTargetExeption(string mes) : base(mes) { }
	}
	/// <summary>
	/// Исключение невалидных данных
	/// </summary>
	public class PassPlaceException : UserException
	{
		public PassPlaceException(string mes) : base(mes) { }
	}
}