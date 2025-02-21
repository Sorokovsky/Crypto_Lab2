using Lab2.Commands.Interfaces;

namespace Lab2.Commands.Commands;

public class ExitCommand : BasicCommand
{
    public override string Title { get; } = "Вихід";

    public override void Invoke(IExitable? exitable)
    {
        exitable?.Exit();
    }
}