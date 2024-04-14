using TbotTinkoff.Classes;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var bot = new Bot("7104333258:AAE6n2a8kFA67-cBrUVt_Z-z6rbmieExmzM");

        bot.Start();
        await bot.GetInfo();
        Console.ReadLine();
        bot.Stop();
    }
}