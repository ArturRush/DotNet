using System;
using System.IO;
using Aviation.Exeptions;

namespace Aviation.Loggers
{
	/// <summary>
	/// Логгер для записи искючений
	/// </summary>
	public static class ExceptionLogger
	{
		/// <summary>
		/// Путь к файлу для записи
		/// </summary>
		public static string FilePath = "ExceptionsLog1.txt";

		/// <summary>
		/// Вести запись в файл? Иначе в консоль
		/// </summary>
		public static bool ToFile = true;

		/// <summary>
		/// Логгирование пользовательского исключения
		/// </summary>
		/// <param name="ex">Исключение</param>
		public static void LogUserException(UserException ex)
		{
			if (FilePath != null && ToFile)
			{
				if (!File.Exists(FilePath))
				{
					var fs = File.Create(FilePath);
					fs.Close();
				}
			}
			using (var writer = ToFile && FilePath != null ? File.AppendText(FilePath) : Console.Out)
			{
				writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ": " + ex.Message + " (Пользовательское исключение)");
			}
		}

		/// <summary>
		/// Логгирование системного исключения
		/// </summary>
		/// <param name="ex">Исключение</param>
		public static void LogSystemException(Exception ex)
		{
			if (FilePath != null && ToFile)
			{
				if (!File.Exists(FilePath))
				{
					var fs = File.Create(FilePath);
					fs.Close();
				}
			}
			using (var writer = ToFile && FilePath != null ? File.AppendText(FilePath) : Console.Out)
			{
				writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ": " + ex.Message + " (Системное исключение)");
			}
		}
	}
}
