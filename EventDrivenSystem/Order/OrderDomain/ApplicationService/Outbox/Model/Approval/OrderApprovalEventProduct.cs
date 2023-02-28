namespace Rosered11.Order.Application.Service.Outbox.Model.Approval;

public record OrderApprovalEventProduct(
    string ID,
    int Quantity
);