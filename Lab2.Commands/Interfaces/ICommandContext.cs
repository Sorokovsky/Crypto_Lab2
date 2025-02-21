namespace Lab2.Commands.Interfaces;

public interface ICommandContext : ICommand, IExitable
{
    public void Append(params List<ICommand> commands);
}