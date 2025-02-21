using Lab2.Commands.Context;
using Lab2.Commands.Interfaces;

namespace Lab2.Application;

public static class Program
{
    public static void Main()
    {
        ICommandContext context = new CommandContext("Головне меню");
        context.Invoke();
    }
}