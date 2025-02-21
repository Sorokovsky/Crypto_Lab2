using Lab2.Commands.Interfaces;

namespace Lab2.Commands.Commands;

public abstract class BasicCommand : ICommand
{
    public int Number { get; set; }

    public abstract string Title { get; }

    public abstract void Invoke(IExitable? exitable);

    public override string ToString()
    {
        return $"{Number}-{Title}.";
    }
}