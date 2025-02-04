namespace Mc2.CrudTest.AcceptanceTests.Mc2CrudTestPresentationServer.Extensions;

internal static class StringExtensions
{
    public static DateTime ToDateTime(this string value)
    {
        var splitedDate = value.Split("-").Select(s => int.Parse(s)).ToArray();
        return new DateTime(splitedDate[0], splitedDate[1], splitedDate[2]);
    }
}