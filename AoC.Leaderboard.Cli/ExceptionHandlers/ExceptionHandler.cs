using AoC.Leaderboard.Domain.Models;
using Spectre.Console;

namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public class ExceptionHandler
{
	private readonly IDictionary<Type, Type> _handlers;

	public ExceptionHandler()
	{
		_handlers = new Dictionary<Type, Type>();
	}

	internal void RegisterHandler<TException, THandler>()
		where THandler : IExceptionHandler<TException>
		where TException : Exception
	{
        _handlers.Add(typeof(TException), typeof(THandler));
    }

    internal int Handle(Exception e)
    {
		var type = e.GetType();

        if (_handlers.ContainsKey(type))
        {
            var handler = Activator.CreateInstance(_handlers[type]);
			var handleMethod = _handlers[type].GetMethod("Handle");

			handleMethod.Invoke(handler, new[] { e });
        }
		else
		{
			AnsiConsole.MarkupLine($"[red]Error:[/] {e.Message}");
        }

		if (GlobalContext.Debug)
		{
            AnsiConsole.WriteException(e);
        }

        return 1;
    }
}
