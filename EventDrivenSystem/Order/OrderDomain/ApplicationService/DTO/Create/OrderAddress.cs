namespace Rosered11.Order.Application.Service.DTO.Create
{
    public record OrderAddress(
        string Street,
        string PostalCode,
        string City
    );
}