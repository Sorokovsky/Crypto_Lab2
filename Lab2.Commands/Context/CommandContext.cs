using System.Text;
using Lab2.Commands.Chooser;
using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;

namespace Lab2.Commands.Context;

public class CommandContext : ICommandContext
{
    private readonly List<ICommand> _commands = [];
    private readonly Encoding _encoding;

    private bool _canExit;

    private int _currentNumber;
    private Encoding _prevInputEncoding;
    private Encoding _prevOutputEncoding;

    public CommandContext(string title, Encoding encoding)
    {
        _encoding = encoding;
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
        SetupEncoding();
        Loop();
        ClearEncoding();
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
    }

    private void SetupEncoding()
    {
        _prevInputEncoding = Console.InputEncoding;
        _prevOutputEncoding = Console.OutputEncoding;
        Console.InputEncoding = _encoding;
        Console.OutputEncoding = _encoding;
    }


    private void ClearEncoding()
    {
        Console.InputEncoding = _prevInputEncoding;
        Console.OutputEncoding = _prevOutputEncoding;
    }
}