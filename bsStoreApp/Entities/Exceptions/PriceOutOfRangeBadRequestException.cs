namespace Entities.Exceptions
{
    public class PriceOutOfRangeBadRequestException : BadRequestException
    {
        public PriceOutOfRangeBadRequestException() : base("En fazla 10.000 girebilirsiniz.")
        {
        }
    }
}
