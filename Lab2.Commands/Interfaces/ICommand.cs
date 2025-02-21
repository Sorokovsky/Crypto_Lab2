namespace Lab2.Commands.Interfaces;

public interface ICommand
{
    public int Number { get; set; }

    public string Title { get; }

    public void Invoke(IExitable? exitable = null);
}