namespace Postieri.Interfaces
{
    public interface IDashboard
    {
        double GetTotal();
        int AvailableCouriers();
        int OrdersInLastThreeMonths();
    }
}
