namespace API_Core.Interface
{
    public interface ISeat
    {
        public int CheckAvailableSeat(int id,int? toAvailQty, int? transId, bool? forUpdate);
    }
}
