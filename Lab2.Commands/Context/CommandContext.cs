using System.Text;
using Lab2.Commands.Chooser;
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
        Append(new ExitCommand());
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
        Loop();
    }

    public void Append(params List<ICommand> commands)
    {
        commands.ForEach(AddCommand);
    }

    private void AddCommand(ICommand command)
    {
        command.Number = _currentNumber++;
        _commands.Add(command);
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
        return Choosing.FromList(
            _commands,
            x => x.ToString() ?? string.Empty,
            int.Parse,
            (command, i) => command.Number == i,
            Title,
            false
        );
    }

    private static void WaitForContinue()
    {
        Console.WriteLine("Натисніть будь-яку кнопку для продовження...");
        Console.ReadKey();
        Console.Clear();
    }
}