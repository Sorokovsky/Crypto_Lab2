using System.Text;
using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;

namespace Lab2.Commands.Context;

public class CommandContext : ICommandContext
{
    private readonly List<ICommand> _commands = [];
    private bool _canExit;

    private int _currentNumber;

    public CommandContext(string title)
    {
        var encoding = Encoding.UTF8;
        Console.InputEncoding = encoding;
        Console.OutputEncoding = encoding;
        Title = title;
    }

    public int Number { get; set; }

    public string Title { get; }

    public void Exit()
    {
        _canExit = true;
    }

    public void Invoke(IExitable? exitable = null)
    {
        _canExit = false;
        Append(new ExitCommand());
        Loop();
    }

    public void Append(params List<ICommand> commands)
    {
        foreach (var command in commands)
        {
            command.Number = _currentNumber++;
            _commands.Add(command);
        }
    }

    private void Loop()
    {
        while (_canExit is false)
            try
            {
                ChooseCommand().Invoke(this);
                WaitForContinue();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Сталася помилка: \"{exception.Message}\"");
            }
    }

    private ICommand ChooseCommand()
    {
        Console.WriteLine($"{Title}.");
        Console.WriteLine("Виберіть операцію.");
        _commands.ForEach(Console.WriteLine);
        Console.Write(">> ");
        var number = int.Parse(Console.ReadLine() ?? string.Empty);
        var command = _commands.FirstOrDefault(x => x.Number == number);
        if (command is null) throw new Exception("Відповідь не розпізнано.");
        return command;
    }

    private void WaitForContinue()
    {
        Console.WriteLine("Натисніть будь-яку кнопку для продовження...");
        Console.Read();
    }
}